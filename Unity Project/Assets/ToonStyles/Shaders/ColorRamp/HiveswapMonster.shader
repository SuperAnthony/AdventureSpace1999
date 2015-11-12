Shader "HiveSwap/Monster"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
      		_Ramp ("Coloring Ramp", 2D) = "white" {}
      		_Highlight ("Shine", Range(0, 64)) = 0
   	 	_RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
    		_RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
	}
	SubShader
	{
	    Tags { "RenderType" = "Opaque" "Queue" = "Geometry" }  
	  	
	  	CGPROGRAM
	  	#include "UnityCG.cginc"
	  	#include "../toonstyles.cginc"
	  	#pragma surface surf Custom approxview nodirlightmap Lambert
	  	
	  	sampler2D _MainTex;
		sampler2D _Ramp;
		half _Highlight;
      		float4 _RimColor;
      		float _RimPower;
		
		struct Input
	  	{
          		float2 uv_MainTex;
          		float3 viewDir;
	  	};
		
		void surf (Input IN, inout SurfaceOutput o) 
		{
		 	fixed4 color = tex2D (_MainTex, IN.uv_MainTex);
		 	o.Albedo = color.rgb;
		 	o.Alpha = color.a;
			half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
          		o.Emission = _RimColor.rgb * pow (rim, _RimPower);
		}
		
		fixed4 LightingCustom (SurfaceOutput o, half3 lightDir, half3 viewDir, half atten)
		{
			return ColorRampLight(o, lightDir, viewDir, atten, _Highlight, _Ramp, 0.25 * (64 - _Highlight));
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
