#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - March 1, 2015
#endregion 


#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using WhatPumpkin.Sgrid.Characters;
using WhatPumpkin.Sgrid.Markers;
#endregion



namespace WhatPumpkin.EditorTesting {

	/// <summary>
	/// Hive swap scene edit.
	/// </summary>

	public class HiveSwapSceneEdit : MonoBehaviour {

		#region static members

		static HiveSwapSceneEdit _instance;

		static bool _editorActivated = false;


		#endregion

		#region fields

		/// <summary>
		/// The name of the _scene load.
		/// </summary>

		[SerializeField] string _sceneLoadName = "Persistent Data"; 

		/// <summary>
		/// The active PC key name.
		/// </summary>

		[SerializeField] string _activePCKey = "PC_JOEY"; 

		/// <summary>
		/// Set PC scene to.
		/// </summary>

		[SerializeField] string _setPCSceneTo; 

		/// <summary>
		/// The _active PC.
		/// </summary>

		PlayerCharacter _activePC;

		/// <summary>
		/// Spawn point.
		/// </summary>

		[SerializeField] SpawnPoint _spawnPoint;

		#endregion

#if UNITY_EDITOR

		[UnityEditor.MenuItem("HiveSwap/Create/Hiveswap Scene Editor Mode")]

		static public void Create() {
		
			GameObject go = new GameObject ("Hiveswap Scene Editor Mode");
			go.AddComponent<HiveSwapSceneEdit> ();


		}

#endif


		// Use this for initialization
		void Awake () {


			// This should only get activated once per game
			if (_editorActivated == true) {DestroyObject(this.gameObject);}

			// If there are no required scene objects then load them with the load level additive method

			if (RequiredSceneObjects.Instance == null) {

						// Check to see if required scene objects are in the scene or not before loading the persistent data level
						Application.LoadLevelAdditive (_sceneLoadName);
				}
		
		}


		void Start() {


			if (_instance == null) {

				_editorActivated = true;

				// Set the singleton instance to this
					_instance = this;
			
					// Get the active pc
					foreach (PlayerCharacter pc in GameObject.FindObjectsOfType<PlayerCharacter>()) {
				
							if (pc.Key == _activePCKey) {
				
									if(_setPCSceneTo != null && _setPCSceneTo != "") {
										pc.ChangeScene(_setPCSceneTo);
									}
								
									// Set the active character
									GameController.PartyManager.SetPlayerCharacter (pc);
									_activePC = pc;
					
					
							}
				
					}
			
			
			
			
			
					// Activate the PC | Do I need this?
					ActivatePC ();
			
			
					// Activate the spawn point
					ActivateSpawn ();

				}

		}



		/// <summary>
		/// Activates the spawn point.
		/// </summary>
		/// <returns><c>true</c>, if spawn was activated, <c>false</c> otherwise.</returns>
		
		bool ActivateSpawn() {
			
			if(_spawnPoint == null){return false;}
			
			// Activate
			_spawnPoint.Activate();
			return true;
			
		}
		
		/// <summary>
		/// Activates the Player Character.
		/// </summary>
		
		bool ActivatePC() {
			
			// Return false if there is no active PC to activate
			if(_activePC == null){return false;}
			
			_activePC.Activate ();
			return true;
			
			
		}

	}
}


