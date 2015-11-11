using UnityEngine;
using System;
using System.Collections;
using UnityEditor;

/// <summary>
/// FBX scale fix. Makes certain objects scale to the right size.
/// </summary>

public class FBXScaleFix : AssetPostprocessor 
{ 
	public void OnPreprocessModel() { 

		ModelImporter modelImporter = (ModelImporter) assetImporter; 

		// 1 divided by file scale

		try {

			modelImporter.globalScale = 1 / modelImporter.fileScale;

		}
		catch (DivideByZeroException e) {
		
			Debug.Log ("Why would the scale ever be 0?");
			throw e;
		
		}
	} 
}
