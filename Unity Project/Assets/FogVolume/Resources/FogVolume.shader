
Shader "Hidden/FogVolume"
{
	
    CGINCLUDE  
    #include "UnityCG.cginc" 
    sampler2D _CameraDepthTexture, _Gradient;
	#if SHADER_API_D3D11 || SHADER_API_OPENGL
	sampler3D _NoiseVolume;
	#endif

	#ifdef SHADER_API_D3D9
	#define MAX_STEPS 256
	#else
	#define MAX_STEPS 512
	#endif

    float4 _Color, _InscatteringColor, _BoxMin, _BoxMax, Speed=1, Stretch;
    float3 L = float3(0, 0, 1);
    float _InscatteringIntensity=1, InscatteringShape, _Visibility, InscatteringStartDistance = 100, InscatteringTransitionWideness = 500, _3DNoiseScale, _3DNoiseStepSize, gain=1, threshold=0;
	float FogMinHeight = 0, FogMaxHeight=-20;

    //http://www.cs.cornell.edu/courses/CS4620/2013fa/lectures/03raytracing1.pdf
	//http://www.clockworkcoders.com/oglsl/rt/gpurt1.htm
    //http://webcache.googleusercontent.com/search?q=cache:9r5sCd1f2hsJ:www.clockworkcoders.com/oglsl/rt/gpurt1.htm+&cd=1&hl=es&ct=clnk&gl=es
 //float hitbox(Ray r, float3 m1, float3 m2, out float tmin, out float tmax) 
 float hitbox (float3 startpoint, float3 direction, float3 m1, float3 m2, inout float tmin, inout float tmax)
 {
        float tymin, tymax, tzmin, tzmax;
        float flag = 1.0;
        if (direction.x > 0) 
    {
            tmin = (m1.x - startpoint.x) / direction.x;
            tmax = (m2.x - startpoint.x) / direction.x;
        }


    else 
    {
            tmin = (m2.x - startpoint.x) / direction.x;
            tmax = (m1.x - startpoint.x) / direction.x;
        }


    if (direction.y > 0) 
    {
            tymin = (m1.y - startpoint.y) / direction.y;
            tymax = (m2.y - startpoint.y) / direction.y;
        }


    else 
    {
            tymin = (m2.y - startpoint.y) / direction.y;
            tymax = (m1.y - startpoint.y) / direction.y;
        }


     
    if ((tmin > tymax) || (tymin > tmax)) flag = -1.0;
        if (tymin > tmin) tmin = tymin;
        if (tymax < tmax) tmax = tymax;
        if (direction.z > 0) 
    {
            tzmin = (m1.z - startpoint.z) / direction.z;
            tzmax = (m2.z - startpoint.z) / direction.z;
        }


    else 
    {
            tzmin = (m2.z - startpoint.z) / direction.z;
            tzmax = (m1.z - startpoint.z) / direction.z;
        }


    if ((tmin > tzmax) || (tzmin > tmax)) flag = -1.0;
        if (tzmin > tmin) tmin = tzmin;
        if (tzmax < tmax) tmax = tzmax;
        return (flag > 0.0);
    }

	//http://zurich.disneyresearch.com/~wjarosz/publications/dissertation/chapter4.pdf
	float Henyey(float3 E, float3 L, float mieDirectionalG)
	{
		
		float theta=saturate(dot(E, L));
		float pi=3.1416;
		return clamp((1.0 / (4.0*pi)) * ((1.0 - pow(mieDirectionalG, 2.0)) / pow(1.0 - 2.0*mieDirectionalG*theta + pow(mieDirectionalG, 2.0), 1.5)), 0, 10) ;
	}
    struct v2f
    {
        float4 SampleCoordinates         : SV_POSITION;
        float3 Wpos        : TEXCOORD0;
        float4 ScreenUVs   : TEXCOORD1;
        float3 LocalPos    : TEXCOORD2;
        float3 ViewPos     : TEXCOORD3;
        float3 LocalEyePos : TEXCOORD4;	
    };

	half Threshold(float noise)
	{
		
		float Guy = noise * gain;
		float thresh =  Guy - threshold;
		return max(0.0f,(lerp(0.0f ,Guy , thresh )));		  
	}

	uniform float4x4 _ViewProjInv;

	float4 GetWorldPositionFromDepth( float2 uv_depth )
	{  
			float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv_depth);
			float4 H = float4(uv_depth.x*2.0-1.0, (uv_depth.y)*2.0-1.0, depth, 1.0);
			float4 D = mul(_ViewProjInv,H);
			return D/D.w;
	}

    v2f vert (appdata_full i)
    {
        v2f o;
        o.SampleCoordinates = mul(UNITY_MATRIX_MVP, i.vertex);
        o.Wpos.xyz = mul((float4x4)_Object2World, float4(i.vertex.xyz, 1)).xyz;
        o.ScreenUVs = ComputeScreenPos(o.SampleCoordinates);
        o.ViewPos = mul((float4x4)UNITY_MATRIX_MV, float4(i.vertex.xyz, 1)).xyz;
        o.LocalPos = i.vertex.xyz;
        o.LocalEyePos = mul((float4x4)_World2Object, (float4(_WorldSpaceCameraPos, 1))).xyz;
		
        return o;
    }

            float4 frag (v2f i) : COLOR
            {

				float3 direction = normalize(i.LocalPos - i.LocalEyePos);
				float tmin, tmax;
				float Volume = hitbox(i.LocalEyePos, direction, _BoxMin.xyz, _BoxMax.xyz, tmin, tmax);
				// tmin must be 0 when inside the volume
				int Inside[3] = {0, 0, 0}, bOutside;
				Inside[0] = step(0, abs(i.LocalEyePos.x) - _BoxMax.x);
				Inside[1] = step(0, abs(i.LocalEyePos.y) - _BoxMax.y);
				Inside[2] = step(0, abs(i.LocalEyePos.z) - _BoxMax.z);
				bOutside  = min(1,(float)(Inside[0] + Inside[1] + Inside[2]));
				tmin*=bOutside;
		
				float2 ScreenUVs = i.ScreenUVs.xy/i.ScreenUVs.w;
				float3 WorldPosition = GetWorldPositionFromDepth(ScreenUVs).xyz;
				float Depth =  length(DECODE_EYEDEPTH(tex2D(_CameraDepthTexture, ScreenUVs).r )/normalize(i.ViewPos).z);								
					
				float MinMax[2] = {max(tmin, tmax), min(tmin, tmax)};
		
				float thickness = min(MinMax[0], Depth) - min(MinMax[1], Depth);	
				
				float Fog = thickness / _Visibility;
				
				Fog = 1-exp2(-Fog);
				Fog *= Volume;
				
				float4 Final=0;
				float3 Normalized_CameraWSdir = normalize(i.Wpos - _WorldSpaceCameraPos);
				float3 Normalized_CameraLocalSdir = normalize(i.LocalPos - i.LocalEyePos);
				
				half DistanceClamp = saturate( Depth/InscatteringTransitionWideness- InscatteringStartDistance);
								
				//High end machines only
				#if SHADER_API_D3D11 || SHADER_API_OPENGL
				//tweak quality here
				#define NoiseSamples  50
				#define GradientSamples 10

				float invNoiseSamples = 1.0f/(float)NoiseSamples, invGradientSamples = 1.0f/(float)GradientSamples;                
				float NoiseSamplesStep = thickness * invNoiseSamples, GradientSamplesStep = thickness * invGradientSamples;
				float4 Noise=0;
				float4 FogGradientSample=0, FogGradient = 0;
				float3 rayStart = _WorldSpaceCameraPos + Normalized_CameraWSdir * tmin;
				float3 rayStop = _WorldSpaceCameraPos + Normalized_CameraWSdir * tmax;
				float3 SampleDirection = rayStop - rayStart;
				float3 step = normalize(SampleDirection) * _3DNoiseStepSize;
				float RayCollision = distance(rayStop, rayStart);
				
				float3 SampleCoordinates = rayStart;
				Speed *=_Time.x;											
				
				////////////////////////////////////
				#if _FOG_GRADIENT && !_FOG_VOLUME_NOISE
			
				//(WIP)
				for(int x=0; x<GradientSamples; x++)
				{
					float Step = MinMax[1] + (GradientSamplesStep * (float)x);
					float LocalCoords = (Normalized_CameraLocalSdir.y * Step) + i.LocalEyePos.y;
					float WorldCoords = (Normalized_CameraWSdir.y * Step) + _WorldSpaceCameraPos.y;
					half2 GradientCoords = half2(0, 1-LocalCoords / (_BoxMin.y - _BoxMax.y) + 0.5);					
					//half2 GradientCoords =  half2(0, 1-WorldCoords / (_BoxMin.y - _BoxMax.y));

					FogGradientSample = tex2D(_Gradient, GradientCoords);
					FogGradient += FogGradientSample * invGradientSamples;
				}

				#else
					FogGradientSample = 1;
				#endif

				#ifdef _FOG_VOLUME_NOISE
				for(int s=0; s<NoiseSamples && RayCollision > 0.0; s++, SampleCoordinates += step, RayCollision -= _3DNoiseStepSize)
                {     					            											
					Noise += Threshold(tex3D(_NoiseVolume, SampleCoordinates * _3DNoiseScale * Stretch.rgb + Speed.rgb).r) *
					Threshold(tex3D(_NoiseVolume, SampleCoordinates * _3DNoiseScale * Stretch.rgb * 0.5h + Speed.rgb * 0.5).r) * invNoiseSamples;											
				}
				#endif				
				//return FogGradient.a;
				Noise.a = 1-exp(-Noise.a);	
				
				#if _FOG_GRADIENT && !_FOG_VOLUME_NOISE  
				_Color.a *= FogGradient.a;//only valid for certain situations. Needs more research.
				_Color.rgb *= FogGradient.rgb;
				#endif
				#if _FOG_VOLUME_NOISE
				_Color.a *= Noise.a;
				_InscatteringColor.rgb *= Noise.rgb; 
				#endif
				
				
				#endif									
				
				#ifdef _FOG_VOLUME_INSCATTERING
				
				//Inscattering						
				//float Inscattering = pow(max(0, dot(L, Normalized_CameraWSdir)), _InscateringExponent);//Deprecated
				float Inscattering = Henyey(Normalized_CameraWSdir, L, InscatteringShape);
				//clamp by distance:
				Inscattering *= DistanceClamp * Fog;
				Final = float4( _Color.rgb +  _Color.rgb * _InscatteringColor.rgb * _InscatteringIntensity * Inscattering, _Color.a);
				
				#else
				Final = _Color;
				#endif																

				
				Final.a *=(Fog * _Color.a); 				
		
				return Final;
			}
            
            ENDCG
            SubShader
			{

        Tags {
            "Queue"="Overlay" "IgnoreProjector"="True" "RenderType"="Transparent" }

        Blend SrcAlpha OneMinusSrcAlpha   

        Cull Front  
		Lighting Off 
		ZWrite Off  
		ZTest Always

        Pass
        {
		Fog { Mode Off }
            CGPROGRAM
            #pragma multi_compile _ _FOG_VOLUME_INSCATTERING  
			#pragma multi_compile _ _FOG_VOLUME_NOISE 
			#pragma multi_compile _ _FOG_GRADIENT
            #pragma vertex vert
			#pragma glsl
            #pragma fragment frag
            #pragma target 3.0
            ENDCG
        }
      
	} 
	Fallback off
}
