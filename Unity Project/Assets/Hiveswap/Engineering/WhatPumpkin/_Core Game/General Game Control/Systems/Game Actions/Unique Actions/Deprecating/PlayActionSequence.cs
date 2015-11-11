#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - February 26, 2015
#endregion 


#region using
using UnityEngine;
using System.Collections;
using WhatPumpkin.Actions.Sequences;
#endregion

namespace WhatPumpkin.Actions {



	/// <summary>
	/// Plays a specified action sequence
	/// </summary>

	public class PlayActionSequence : ActionType {

		#region methods

		bool _finishedPlaying = false;

		public PlayActionSequence() {
			_name = "PlayAS";
			ActionSequence.ActionSequencePlayed += HandleActionSequencePlayed;
		}

		
		~PlayActionSequence() {
			ActionSequence.ActionSequencePlayed -= HandleActionSequencePlayed;
		}

		void HandleActionSequencePlayed (ActionSequence actionSequence)
		{
//			Debug.Log ("Sequence Played: " + actionSequence.Key);
			_finishedPlaying = true;
		}

		public override IEnumerator BeginAction(params object[] parameters) {

			// Make sure there is a parameter
			if (parameters.Length > 0) {

				// There should only be one parameter
				string key = parameters[0].ToString();


				// Play the action sequence of a given key
				IPerform perormingObject = GameController.SceneManager.FindKeyedObject<IPerform>(key);
				if(perormingObject != null) {
					perormingObject.Play();
					_finishedPlaying = false;
				}
				else {
					_finishedPlaying = true;
				}
				
			}


			while (!_finishedPlaying) {
				// Wait here until finished playing 
				yield return false;
				
			}
			
			// End the action
			EndAction ();
			yield break;
		}


		#endregion

	}
}
