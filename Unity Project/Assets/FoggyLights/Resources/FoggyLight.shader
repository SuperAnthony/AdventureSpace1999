Shader "Hidden/FoggyLight"
{
    SubShader
    {
        Tags { "Queue"="Overlay" "IgnoreProjector"="True" "RenderType"="Transparent" }
 
		Lighting Off 
		ZWrite Off 
		ZTest Always
		Fog { Mode Off }

       Pass
        {
            Blend one one
            
			CGINCLUDE      
            #pragma vertex PointLightVert
            #pragma fragment PointLight
            #pragma target 3.0		
			#pragma multi_compile _ _FOG_CONTAINER  	
            sampler2D _CameraDepthTexture;
            #include "UnityCG.cginc"
			float PointLightIntensity;
            float4 PointLightPosition;
            float4 PointLightColor;
            float PointLightExponent,  Offset, _Visibility;
			ENDCG

			CGPROGRAM     
			struct PointLightv2f
            {
                float4 pos         : SV_POSITION;
                float3 Wpos        : TEXCOORD0;                
                float3 ViewPos     : TEXCOORD1;
				float4 ScreenUVs   : TEXCOORD2;
            };
            void PointLightVert (appdata_full i, out PointLightv2f o)
            {
               
				UNITY_INITIALIZE_OUTPUT(PointLightv2f, o);
                o.pos = mul(UNITY_MATRIX_MVP, i.vertex);
                o.Wpos.xyz = mul((float4x4)_Object2World, float4(i.vertex.xyz, 1)).xyz;
                float4 ScreenPos = ComputeScreenPos(o.pos);
				o.ScreenUVs.xy = ScreenPos.xy / ScreenPos.w;
				o.ScreenUVs.w = ScreenPos.w;
                o.ViewPos = mul((float4x4)UNITY_MATRIX_MV, float4(i.vertex.xyz, 1)).xyz;  				                  
            }

			

            float4 PointLight(PointLightv2f i) : COLOR
            {
                float3  Wpos = i.Wpos, q;
                float4 PointInscattering;
                float  c, s, b;
                float3 dir = (Wpos - _WorldSpaceCameraPos);
                float l = length(dir);
                dir /=l;
                q = _WorldSpaceCameraPos - PointLightPosition.rgb ;				
                b = dot(dir, q );
                c = dot(q , q );
				
                // evaluate integral
				s = 1.0f / sqrt(c - b *b );
                PointInscattering = min(max(0, s * (atan( (l + b ) * s ) - atan( b *s ))), 100);
		
                //PointInscattering = 1-exp2(-PointInscattering );//filmic style
				PointInscattering /=(1+PointInscattering );//reinhard style
                PointInscattering = pow(PointInscattering , PointLightExponent )* PointLightColor * PointLightIntensity * 0.5;
                	
				float2 ScreenUVs = i.ScreenUVs.xy;
                float Depth =  length(DECODE_EYEDEPTH(tex2D(_CameraDepthTexture, ScreenUVs).r )/normalize(i.ViewPos).z);																				
				
                //Soft interesection & offset:
				float InscatteringClamp = saturate( Depth - length(q) - Offset);
                PointInscattering.rgb *= InscatteringClamp;
				
				#ifdef _FOG_CONTAINER
				half FogVolumeAtten = exp(-i.ScreenUVs.w/_Visibility);
				PointInscattering.rgb *= FogVolumeAtten;
				#endif

				PointInscattering.a = PointLightColor.a;
				
                return PointInscattering;
            }

              ENDCG
        }

	} 
	Fallback off
}