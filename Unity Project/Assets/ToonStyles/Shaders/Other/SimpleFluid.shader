Shader "ToonStyles/Other/SimpleFluid"
{
	Properties
	{
      	_BumpMap ("Bump", 2D) = "bump" {}
		_WaveSpeedX ("Wave Speed X", Float) = 0.2
		_WaveSpeedY ("Wave Speed Y", Float) = 0.2
		_WaveScale("Wave Scale", Float) = 1
		_ColorControl ("Reflective color (RGB) fresnel (A) ", 2D) = "" { }
		_Horizon ("Horizon Color", Color) = (1,1,1,1)
	}
	SubShader
	{
	    Tags { "RenderType"="Opaque" }
		
	  	CGPROGRAM
		#include "UnityCG.cginc"
	  	#include "../toonstyles.cginc"
		#pragma surface surf Custom approxview halfasview nodirlightmap 
		
		sampler2D _BumpMap;
		sampler2D _ColorControl;
	    half _WaveSpeedX;
	    half _WaveSpeedY;
	    half _WaveScale;
	    fixed4 _Horizon;
	    	
	  	struct Input
	  	{
	    	fixed2 uv_BumpMap;
	    	half3 viewDir;
	  	};
		
		fixed4 LightingCustom (SurfaceOutput o, half3 lightDir, half atten)
		{
			return SimpleLambertLight(o, lightDir, atten);
		}

	  	void surf (Input IN, inout SurfaceOutput o) 
	  	{
			fluidSurf(o, _BumpMap, _ColorControl, IN.uv_BumpMap, _WaveScale, half2(_WaveSpeedX, _WaveSpeedY), IN.viewDir, _Horizon);
	  	}
		ENDCG
	} 
	FallBack "Diffuse"
}

