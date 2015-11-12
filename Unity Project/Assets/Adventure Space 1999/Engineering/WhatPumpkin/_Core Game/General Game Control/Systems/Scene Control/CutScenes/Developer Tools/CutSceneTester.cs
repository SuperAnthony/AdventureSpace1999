// Copyright (c) 2015 What Pumpkin Studios
// Author - Sergio Nizama
// Date Created - March 3rd, 2015


using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace WhatPumpkin.CutScenes {

	/// <summary>
	/// CutSceneTester - Utility for testing out cutscenes.
	/// </summary>

	public class CutSceneTester : MonoBehaviour {

        #region Data Members (Fields)

        /// <summary>
		/// Cutscene key to load cutscene.
		/// </summary>
		//public string keyOfCutScene;
		//public int key;

		/// <summary>
		/// List of cutscenes.
		/// </summary>
		public List<CutScene> cutScenes = new List<CutScene>();

		/// <summary>
		/// The _cs loading image.
		/// </summary>

		public Image _csLoadingImage;


		Camera csCamera;

        #endregion

        // TODO: ECCC Demo Temp Code
		public string LoadSceneOnComplete = "FirstFloor";

        #region Member Functions (Methods)

        void Start (){


			// Set up the animated materials
			SetUpAnimatedMaterials();


			GetCutScene (0).Test ();
			//csCamera = gameObject.GetComponent<Camera>();

			csCamera = GameObject.FindWithTag ("MainCamera").GetComponent<Camera> ();


			//csCamera = GameObject.FindObjectOfType<Camera> ().GetComponent<Camera> ();

			Debug.Log ("camera? " + csCamera);
		}
		/// <summary>
		/// Takes in key and returns from the list of CS, the CS associated with the key.
		/// </summary>
		CutScene GetCutScene(string key){
			foreach (CutScene element in cutScenes) {
				if (key == element.Key){
					return element;
				}

			}
			return null;
		}

		CutScene GetCutScene(int index){
			if (cutScenes.Count == 0) {
				return null;
			} else {
				return cutScenes[index];	
			}
		}

		void Update (){


			CutScene.Active.Update ();


			// Update camera position
			csCamera.transform.position = cutScenes[0].PrincipalActor.CameraGO.position;
			csCamera.transform.rotation = cutScenes[0].PrincipalActor.CameraGO.rotation;

		}

		void PlayNext() {
		
			if (CutScene.Active.HasNextCutScene) {

				StartCutscene (CutScene.Active.NextCutScene);

			} 
			else {
			
				if(LoadSceneOnComplete != null || LoadSceneOnComplete != "") {

					// Show loading image
					_csLoadingImage.gameObject.SetActive(true);

					// Load
					Application.LoadLevel(LoadSceneOnComplete);
				}

			}

		
		}

		/// <summary>
		/// Starts the cutscene.
		/// </summary>
		/// <param name="cutsceneKey">Cutscene key.</param>

		public void StartCutscene(string cutsceneKey) {
			
			//try{
			// Find cutscene and play it
			CutScene cutscene = GetCutScene(cutsceneKey);
			
			if(cutscene!=null) {
				
				// Play Cut Scene
				cutscene.Play();
				
			}
		
		}

		public void StartCutscene(int cutsceneIndex) {
			

			//try{
			// Find cutscene and play it
			CutScene cutscene = GetCutScene(cutsceneIndex);
			
			if(cutscene!=null) {
				
				// Play Cut Scene
				cutscene.Play();
				
			}
			
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
					//AnimatingMaterialController aMatCont = go.AddComponent<AnimatingMaterialController>();


					AnimatingMaterialController aMatCont = go.GetComponent<AnimatingMaterialController>();


					// Initialize the controller
					if(aMatCont != null) {
						aMatCont.Init();
					}
				}
				
			}

        }
        #endregion
    }
}