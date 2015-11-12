#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - June 6, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;
#endregion

namespace WhatPumpkin {
	
	public class ArtAssetControls : MonoBehaviour {

		/// <summary>
		/// The materials referenced in the scene. TODO: This is not at all ideal, but quick fix for gdc build 
		/// </summary>
		
		[SerializeField] Material [] _materials;


		// Use this for initialization
		void Start () {
			SetUpAnimatedMaterials ();
		}
		
		// Update is called once per frame
		void Update () {

		}

		/// <summary>
		/// Gets a material by name.
		/// </summary>
		/// <returns>The material.</returns>
		/// <param name="name">Name.</param>

		public Material GetMaterial(string name) {
			
			// Gets a material by the material's name


			// Scene 
			if(GameData.SceneData != null) {

				foreach (Material material in GameData.SceneData.Materials) {
					
					if(material.name == name || material.name == material.name + " (Instnace)") {
						return material;
					}
					
				}
			}



			if(GameData.PersistentData != null) {

				// Gets a material by the material's name
				foreach (Material material in GameData.PersistentData.Materials) {
					
					if(material.name == name || material.name == material.name + " (Instnace)") {
						return material;
					}
					
				}
			}
			
			return null;
			
		}

		/// <summary>
		/// Sets up any animated materials that are found.
		/// </summary>
		
		public void SetUpAnimatedMaterials() {

			// Search through all objects to see if it should set up an animated material controller
			GameObject [] all_objects = GameObject.FindObjectsOfType<GameObject> ();
			
			foreach (GameObject go in all_objects) {

				// Check to see if this object is an animated controller
				if(AnimatingMaterialController.IsAnimatedMaterialController(go))
				{
					// If so, add an animated controller component
					AnimatingMaterialController aMatCont = go.AddComponent<AnimatingMaterialController>();
					
					// Initialize the controller
					aMatCont.Init();
				}
				
			}
			
		}

	}
}
