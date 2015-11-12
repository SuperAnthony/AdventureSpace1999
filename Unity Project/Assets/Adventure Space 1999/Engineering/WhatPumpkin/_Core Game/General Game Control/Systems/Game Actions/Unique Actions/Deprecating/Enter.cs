#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - April 17, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;
using WhatPumpkin.FX;
using WhatPumpkin.Sgrid.Markers;
#endregion


namespace WhatPumpkin.Actions {

	/// <summary>
	/// Enter a room.
	/// </summary>

	// TODO: Deprecate

	public class Enter : ActionType {

		#region methods

		static string spawnPointName;

		public Enter() {
			_name = "Enter";
		}

		static public void OnFadeComplete() {

			//Debug.Log ("Enter's OnFadeComplete Invoked");

			//SpwanPlayer ();

			//CameraTransitions.StartFadeFromBlack ();

			//GameController.ActivePlayerCharacter.GetComponent<AIPath> ().Enable ();

			// MovePlayerToPosition(Positionz);
			/*
			// Find the game object based on the name and move the transform to it
			GameObject targetObject =  GameController.SceneManager.FindSceneObject(targetObjectName);
			
			if(targetObject != null) {
				
				Debug.Log (GameController.ActivePlayerCharacter.transform.position);
				
				GameController.ActivePlayerCharacter.transform.position = targetObject.transform.position;
				GameController.Instance.MoveTarget.transform.position = targetObject.transform.position;
				
				
			}
			else {
				Debug.LogError("Target not found in scene. Please check for correct spellings and that the correct game object is in the scene named properly.");
			}
			*/	
			
		}

		static public void SpwanPlayer() {
		
			// Get the spawn point
			SpawnPoint spawnPoint = SpawnPoint.FindSpawnPoint(spawnPointName);

			if(spawnPoint != null) {
				
				// Activate Spawn Point
				SpawnPoint.ActivateSpawnPoint(spawnPoint);

				// Get the PC's AI Path and disable it
				//GameController.ActivePlayerCharacter.GetComponent<AIPath>().Disable();
			}

		}

				
		public override IEnumerator BeginAction(params object[] parameters) {

			Debug.Log ("Begin Use Action");

			//CameraTransitions.fadeToBlackComplete += OnFadeComplete;

			// Make sure there is a parameter
			if (parameters.Length > 0) {

				// Pseudo Code

					// Get the name of the target object
				spawnPointName = parameters[0].ToString();

				
				// Make Camera Camera Fade To Black
				//CameraTransitions.StartFadeToBlack();

				OnFadeComplete();

			


			}
			else {
				Debug.LogError("No parameters found for the '" + _name + "' action.");
			}
			
			// End the action
			EndAction ();
			yield break;
		}

		#endregion

	}
}
