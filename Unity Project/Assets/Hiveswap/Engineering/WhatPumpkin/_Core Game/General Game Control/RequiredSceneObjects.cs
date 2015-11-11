#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - December 31, 2014
#endregion 



#region using
using UnityEngine;
using System.Collections;
#endregion

/// <summary>
/// The objects required for each scene. Should originate in a Persistent Data scene
/// </summary>

namespace WhatPumpkin.EditorTesting {

	/// <summary>
	/// The required scene objects for the game.
	/// </summary>

	public class RequiredSceneObjects : MonoBehaviour {

		/// <summary>
		/// The singleton instnace
		/// </summary>

		static RequiredSceneObjects _instance; 

		/// <summary>
		/// Gets the singleton instance.
		/// </summary>
		/// <value>The instance.</value>

		static public RequiredSceneObjects Instance { get { return _instance; } } 

		// Use this for initialization
		void Awake () {
		
			// If there is no instance of the required scene objects then make this the sigleton instance
			if (_instance == null) {
			
				_instance = this;
			
			}
			else { // Otherwise, destroy this game object

				// This shouldn't happen

				Debug.LogError("Duplicate Required Scene Object is being created. Destroying this object, but this should not happen.");

				if(_instance != this) {
					Destroy(this.gameObject);
				}
			
			}

		

		}

		void Start() {
		

		}
		


	}
}
