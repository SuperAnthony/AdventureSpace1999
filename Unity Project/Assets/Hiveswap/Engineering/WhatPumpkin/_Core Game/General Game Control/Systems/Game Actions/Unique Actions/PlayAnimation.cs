#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - December 30, 2014
#endregion 

#region using
using UnityEngine;
using System.Collections;
using WhatPumpkin.Sgrid.Characters;
using WhatPumpkin.ScriptingLanguage;
using WhatPumpkin.Entities;
#endregion

namespace WhatPumpkin.Actions {

	public class PlayAnimation : ActionType {

		/// <summary>
		/// Activate this instance.
		/// </summary>

		public PlayAnimation() {
			_name = "PlayAnim";
		}

		public override IEnumerator BeginAction(params object[] parameters) {
			
			// Make sure there is a parameter
			if (parameters.Length > 0) {

//				Debug.Log("Play Animation: " + parameters[0].ToString());

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

				// TODO: Method that searches for an entity and returns it's game object
				//Entity entity = GameController.SceneManager.FindEntity(animatingObjectName);
				GameObject animatingObject = GameObject.Find(animatingObjectName);

				/*
				if(entity != null) {
					animatingObject = entity.gameObject;
				}*/

				// If the object exists, then check to see if an Animator is attached
				if(animatingObject != null) {
					
					// If an animator is attached then play the correct animation
					Animator animator = animatingObject.GetComponent<Animator>();
					if(animator != null) 
					{
						try{
							animator.Play(animationClip);
						}
						catch {
							Debug.Log ("Could not find animation");
							EndAction ();
						}
					}



					// This gives the animation time to start playing
					for(int i = 0; i < 10; i++) {
						yield return null;
					}

					while(animator != null && animator.GetCurrentAnimatorStateInfo(0).normalizedTime  < .98) {

						yield return null;

						
					}

					//Debug.Log (animator.GetCurrentAnimatorStateInfo(0).IsName(animationClip));

				}
				else {
					Debug.LogError("Could not locate the game object " + animatingObject + "for the PlayAnimation action.");
				}
				
				
				
			}
			else {
				Debug.LogError("No parameters found for the PlayAnimation action.");
			}
			
			// End the action
			EndAction ();
			yield break;
		}

	}
}