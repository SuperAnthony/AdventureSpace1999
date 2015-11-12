using UnityEngine;
using System.Collections;

using UnityStandardAssets.ImageEffects;

public class Mosaic : PostEffectsBase {

	// Use this for initialization
	void Start () {
	
	}
	
	[ImageEffectOpaque]
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{

		source.filterMode = FilterMode.Point;

		Graphics.Blit (source, destination);
	}
}
