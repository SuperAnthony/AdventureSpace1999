#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - November 17, 2014
#endregion 

#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#endregion

namespace WhatPumpkin {



	/// <summary>
	/// Animating Material Controller - animating this node will swap textures in a material.
	/// The material that is affected is determined the the name of this node
	/// A texture is selected based on the location, rotation or scale of one axis which can be dynamically selected
	/// </summary>


	public class AnimatingMaterialController : MonoBehaviour {

		/// <summary>
		/// The animating textures path.
		/// </summary>
		
		static string _animatingTexturesPath = "AnimatingTextures";
		
	
		/// <summary>
		/// The animation material controller signifier.
		/// This is the string used to determine if a node is an animation material controller
		/// </summary>
		
		static string _animMatConSignifier = "amatcon_";


		// Static Fields

		const int IndexOffset = -360;


		// Instance Fields

		/// <summary>
		/// The name of the Animating Material.
		/// All textures associated with this animated material must lead with this name followed by an underscore
		/// </summary>

		string _animMaterialName; 


		/// <summary>
		/// The animating material.
		/// </summary>
		[SerializeField] Material _animatingMaterial;

		/// <summary>
		/// The textures that can be selected.
		/// </summary>

		[SerializeField] List<Texture2D> _textures = new List<Texture2D>();


		// Instance Properties

		/// <summary>
		/// Gets the name of the Animating Material.
		/// </summary>
		/// <value>The name.</value>

		public string AnimatingMaterialName { get { return _animMaterialName; } }




		/// <summary>
		/// The max number of animating textures.
		/// Should never be reached, but will be used to break out of a while loop just in case
		/// </summary>

		int _maxAnimatingTextures = 300;

		//Material mat;


		bool isLoaded = false;

		// Type Methods

		/// <summary>
		/// Setups the amat cons.
		/// </summary>
		
		static public void SetupAmatCons() {
			
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

		/// <summary>
		/// Determines if an animated material controller belongs to the specified gObject.
		/// </summary>
		/// <returns><c>true</c> if is animated material controller the specified gObject; otherwise, <c>false</c>.</returns>
		/// <param name="gObject">G object.</param>

		static public bool IsAnimatedMaterialController(GameObject gObject) {

			return gObject.name.StartsWith (_animMatConSignifier);
		
		}

		/*
		bool IsApproximatelyWholeNumber(float f) {
		
			f = f - Mathf.FloorToInt (f); 
		
			UnityEngine.Mathf.Approximately(f
		}


		int GetWholeNumber(float f) {

			return Mathf.RoundToInt(f:);
		
		}*/ 

		// Instance Methods


		/// <summary>
		/// Initialize this instance. The controller will determine when this will happen.
		/// </summary>
		public void Init() {
					
			//Debug.Log ("Init");
			// Get the name of the material that is being animated
			_animMaterialName = GetMaterialName ();

			// Get the material 

			_animatingMaterial = FindMaterialInParentAndChildren (_animMaterialName, this.transform.parent);
	

			if (_animatingMaterial == null) {
				// If no animation was found
				//Debug.LogError ("Could not find the animating material. Are the joints and material names properly named?");
			}

			// Load textures on start 
			LoadTextures ();
			//StartCoroutine (LoadTextures());
			// ECCC Build - we preloaded the textures
			isLoaded = true;

		}

		string GetMaterialName() {

			// Return the name of the object minus the prefixed node tag
			return this.gameObject.name.Replace (_animMatConSignifier, "");
		}



		// Use this for initialization
		void Start () {

			//_instance = this;
		
			//Init ();
			//SetupAmatCons ();
		}
		
		// Update is called once per frame
		void Update () {


			// Check to see if we've loaded the textures and that there is an animating material
		
			int index = 0;

			if(isLoaded && _animatingMaterial != null) {
			

				float y = (float)this.transform.localPosition.y;

		
				if((y) % 1 == 0) {


					index = (int)(this.transform.localPosition.y);
					ChangeTexture(index);

				}
				else {

					index = (int)Mathf.RoundToInt(y);

					float difference = (float)Mathf.Abs(index - y);

					if(difference < .01) {
						ChangeTexture(index);
					}

				}


			}


		}

		/// <summary>
		/// Loads the textures.
		/// </summary>
		/// <returns>The number of textures loaded.</returns>
		/*
		IEnumerator LoadTextures() {

			Debug.Log ("Loading Textures");

			int index = 1;

			ResourceRequest request;


			// Load all textures associated with this material
			do  {
			
				// Get request to load a texture. If null, then it did not find the texture
				// Use the animating texture path from the developer settings
				// Use the name from this component's material name property
				// Append the index as a string to the material name in order to get the indexted texture


				request = LoadTexture (_animatingTexturesPath, AnimatingMaterialName.Replace("_1","") + "_" + index.ToString());
				yield return request; // Return request when it is loaded or determined to be null

				// Add the texture if the request is not null
				if(request.asset != null) {
					_textures.Add(request.asset as Texture2D);
				}
				else {
					// Otherwise break
					// The reqeust is null so there should be no more textures to load
					break;
				}
				// Move to the next index
				index++;
			
			} while (request.asset != null || index <= _maxAnimatingTextures); // Break if we get beyond max allowed textures - we should never reach this limit, this is to prevent the game from getting stuck in this loop

			// Let this object know that it is loaded | TODO: May be a temporary solution to handling this
			isLoaded = true;

			//yield return null;

		}*/

		void LoadTextures() {
		
			int index = 1;
			
			ResourceRequest request;
			
			
			// Load all textures associated with this material
			do  {
				
				// Get request to load a texture. If null, then it did not find the texture
				// Use the animating texture path from the developer settings
				// Use the name from this component's material name property
				// Append the index as a string to the material name in order to get the indexted texture
				
				
				request = LoadTexture (_animatingTexturesPath, AnimatingMaterialName.Replace("_1","") + "_" + index.ToString());

				// Add the texture if the request is not null
				if(request.asset != null) {
					_textures.Add(request.asset as Texture2D);
				}
				else {
					// Otherwise break
					// The reqeust is null so there should be no more textures to load
					break;
				}
				// Move to the next index
				index++;
				
			} while (request.asset != null || index <= _maxAnimatingTextures); // Break if we get beyond max allowed textures - we should never reach this limit, this is to prevent the game from getting stuck in this loop
			
			// Let this object know that it is loaded
			isLoaded = true;
			

		}


		/// <summary>
		/// Loads the texture.
		/// </summary>
		/// <returns>The texture.</returns>
		/// <param name="path">Path.</param>
		/// <param name="textureName">Texture name.</param>

		ResourceRequest LoadTexture(string path, string textureName) {

			ResourceRequest request = Resources.LoadAsync<Texture2D>(path + "/" + textureName);
			return request;
		}


		/// <summary>
		/// Changes the texture.
		/// </summary>
		/// <param name="frame">Frame.</param>

		void ChangeTexture(int frame) {

			if(frame < 0){return;}

			// Check to make sure we're not attempting to access an element that does not exist
			if(frame  < _textures.Count) {
				// Set the texture
				_animatingMaterial.mainTexture = _textures [frame];
			}

		}

		// Useful functions. Place these in a new data type?


		/// <summary>
		/// Finds the material in parent and children.
		/// </summary>
		/// <returns>The material in parent and children.</returns>
		/// <param name="mat_name">Mat_name.</param>
		/// <param name="parent">Parent.</param>

		Material FindMaterialInParentAndChildren(string mat_name, Transform parent, string objectNameMustContain = "") {

			// Search through parents
			foreach (Transform child in parent.transform) {

				// Try to get the material in this child object

				// Check to see if the object contains the name that is required
				// Before searching through materials
				//if(objectNameMustContain == "" || child.name.Contains(objectNameMustContain)) {
					//Material mat = null;
					
					Material mat = FindMaterialInTransform(mat_name, child); 
					//If the material exists then return the material
					if(mat != null) {
						return mat;
					}
				//}
			}

			//  Could not find the material
			return null;

		}

		/// <summary>
		/// Finds the material in transform.
		/// </summary>
		/// <returns>The material in transform.</returns>
		/// <param name="mat_name">Mat_name.</param>
		/// <param name="trans">Trans.</param>

		Material FindMaterialInTransform(string mat_name, Transform trans) {


			// Search through each material in the object
		
			if (trans.GetComponent<Renderer>() != null) { // Make sure a renderer is attached

				foreach(Material mat in trans.GetComponent<Renderer>().materials) {

					// Must append " (Instance)" to the search because
					// Unity automatically adds this in quotes whenever a new instnace is created
					// Just in case however, we will also search for the name
					//Debug.Log(mat.name + " | " + mat_name);
					if(mat.name == (mat_name + " (Instance)")  || mat.name == mat_name) {
						return mat;
					}
				}
			}

			return null;
		}



	}
}