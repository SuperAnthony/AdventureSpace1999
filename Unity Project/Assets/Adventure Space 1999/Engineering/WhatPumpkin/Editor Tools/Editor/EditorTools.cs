#region using
using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
#endregion

public class EditorTools : MonoBehaviour {
	
	[MenuItem("HiveSwap/Tools/Hook Up Textures") ]

	static public void HookupTextures() {
	
		// Clear the materials
		//materials.Clear ();

		GameObject [] gameObjects = FindObjectsOfType<GameObject> ();

		foreach (GameObject gameObject in gameObjects) {
		
			SyncTextures(gameObject);

		}
	
	}


	[MenuItem("HiveSwap/Tools/Hook Up Textures (Overwrite)") ]
	
	static public void HookupTexturesOverwrite() {
		
		// Clear the materials
		GameObject [] gameObjects = FindObjectsOfType<GameObject> ();
		
		foreach (GameObject gameObject in gameObjects) {
			
			SyncTextures(gameObject, true);
			
		}

	}

	/// <summary>
	/// Finds the materials texture.
	/// </summary>
	/// <returns>The materials texture.</returns>
	/// <param name="material">Material.</param>

	static Texture2D FindMaterialsTexture(Material material) {
	
		// Try to find the texture within it's folder
		string materialPath = AssetDatabase.GetAssetPath(material);
		string texturePath = materialPath.Replace("/Materials/", "/Textures/");
		texturePath = texturePath.Replace(".mat",".png");
		Texture2D texture = AssetDatabase.LoadAssetAtPath(texturePath, typeof(Texture2D)) as Texture2D;

		// If texture is not found then look in the shared folder
		if (texture == null) {
		
			texturePath = "Assets/Hiveswap/Game Assets/Shared Textures/";
			string textureName = material.name + ".png";
			texturePath+=textureName;
			texture = AssetDatabase.LoadAssetAtPath(texturePath, typeof(Texture2D)) as Texture2D;	
		}

		return texture;
	
	}

	/// <summary>
	/// Syncs the textures.
	/// </summary>
	/// <param name="gameObject">Game object.</param>
	/// <param name="overwrite">If set to <c>true</c> overwrite.</param>

	static void SyncTextures(GameObject gameObject, bool overwrite = false) {
	
		Renderer renderer = gameObject.GetComponent<Renderer> ();

		if (renderer != null && renderer.sharedMaterials != null) {
		
			foreach(Material sharedMaterial in renderer.sharedMaterials) {
			
				// Find the material's texture
				Texture2D texture = FindMaterialsTexture(sharedMaterial);

				// If no texture was found then exit the loop
				if(texture == null){break;}

				// Attach the texture
				if(sharedMaterial.mainTexture == null || overwrite == true) {
					sharedMaterial.mainTexture = texture;
				}

			}
		
		}

	}




}
