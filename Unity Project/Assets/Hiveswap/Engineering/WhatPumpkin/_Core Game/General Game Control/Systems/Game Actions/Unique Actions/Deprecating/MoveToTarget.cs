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


	public class MoveToTarget : ActionType {
	
		/// <summary>
		/// The name.
		/// </summary>
		
		public const string NAME = "MoveToTar"; 


		/// <summary>
		/// Activate this instance.
		/// </summary>
		
		public MoveToTarget() {
			_name = NAME;
		}
		
		public override IEnumerator BeginAction(params object[] parameters) {

			// Make sure there is a parameter
			if (parameters.Length > 0) {

				// Find the game object based on the name and move the transform to it
				GameObject targetObject =  GameController.SceneManager.FindSceneObject(parameters[0].ToString()).gameObject;

				if(targetObject != null) {
					// Move the move target to the target object
					//GameController.ActiveController.MoveTarget.transform.position = targetObject.transform.position;

					// Check to see if character reached the target
					// TODO: Other better solutions for this
					// Will need to turn this to an IEnumorator and run as a coroutine

					AIPath aiPathCharacter = GameController.PartyManager.ActivePC.GetComponent<AIPath>();

					//aiPathCharacter.target = targetObject.transform;

					GameController.Instance.MoveTarget.transform.position = targetObject.transform.position;
				

					// Make sure at least one second goes by before checking to see if the target has been reached
					yield return new WaitForSeconds(1);

					// Check to see if the target has been reached
					// before moving on to the next step
					while(aiPathCharacter.TargetReached == false) {
						// Return false because the target has not been reached
						yield return false;
					}

					// TODO: Other conditions will need to be created to determine if the character 
					// Has been blocked, and what to do in that situation
					// May have to invoke an action with a fail state

					// Change the character target back to default
					// aiPathCharacter.target = GameController.Instance.MoveTarget;



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
