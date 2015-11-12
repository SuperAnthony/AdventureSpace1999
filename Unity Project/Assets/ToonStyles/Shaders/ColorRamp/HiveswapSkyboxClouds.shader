Shader "HiveSwap/SkyboxClouds"
{
	Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
	}

	SubShader {
	Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
	LOD 100
	Fog { Mode Off }
        Blend SrcAlpha OneMinusSrcAlpha 
	Lighting Off

	Pass { SetTexture [_MainTex] { combine texture } }

	}
}
