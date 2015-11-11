#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - December 16, 2014
#endregion 

#region using
using UnityEngine;
using System.Collections;
#endregion

namespace WhatPumpkin.Actions {

	/// <summary>
	/// Move to target.
	/// </summary>


	public class SetTarget : ActionType {
	
		/// <summary>
		/// Activate this instance.
		/// </summary>
		
		public SetTarget() {
			_name = "SetTar";
		}
		
		public override IEnumerator BeginAction(params object[] parameters) {

			Debug.Log ("Set Tar");

			// Make sure there is a parameter
			if (parameters.Length > 0) {

				// Find the game object based on the name and move the transform to it
				GameObject targetObject =  GameController.SceneManager.FindEntity(parameters[0].ToString()).gameObject;

				if(targetObject != null) {
	


					Debug.Log (GameController.PartyManager.ActivePC.transform.position);

					GameController.PartyManager.ActivePC.transform.position = targetObject.transform.position;
					GameController.Instance.MoveTarget.transform.position = targetObject.transform.position;

				
				}
				else {
					Debug.LogError("Target not found in scene. Please check for correct spellings and that the correct game object is in the scene named properly.");
				}
			}
			else {
				Debug.LogError("No target is defined for the MoveToTarget command.");
			}

			// Let the game know that you can end the action
			EndAction ();
			yield break;

		}
	
	}
}
