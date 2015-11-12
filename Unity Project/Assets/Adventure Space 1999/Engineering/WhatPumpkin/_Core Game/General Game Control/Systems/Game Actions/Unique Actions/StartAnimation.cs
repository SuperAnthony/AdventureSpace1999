using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using WhatPumpkin.ScriptingLanguage;

namespace WhatPumpkin.Actions {

	public class StartAnimation : ActionType {

		public StartAnimation() {
			_name = "StartAnim";
		}

		public override IEnumerator BeginAction(params object[] parameters) {

			// Make sure there is a parameter
			if (parameters.Length > 0) {

				// Get the variable name and the value
				string script = parameters[0].ToString(); // The full script
				string animatingObjectName; // The animating object

				// Remove spaces
				//script = script.Replace(" ", "");

				// Get the variable name 
				animatingObjectName=Scripting.GetVariableNameFromAssignment(script);
				// Get the animation clip to be played
				string animationClip = Scripting.GetValueFromAssignment(script);

				// Locate a game object with the correct name
				GameObject animatingObject = GameController.SceneManager.FindSceneObject(animatingObjectName);
				// If the object exists, then check to see if an Animator is attached
				if(animatingObject != null) {
				
					// If an animator is attached then play the correct animation
					Animator animator = animatingObject.GetComponent<Animator>();

					if(animator != null) 
					{
						animator.Play(animationClip);
					}


				}
				else {
					Debug.LogError("Could not locate the game object " + animatingObject + "for the StartAnimation action.");
				}

			}
			else {
				Debug.LogError("No parameters found for the StartAnimation action.");
			}

			// End the action
			EndAction ();
			yield break;
		}


	}
}
