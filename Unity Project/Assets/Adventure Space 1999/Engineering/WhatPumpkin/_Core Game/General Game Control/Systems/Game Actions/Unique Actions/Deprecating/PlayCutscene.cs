#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - January 21, 2015
#endregion

#region using
using UnityEngine;
using System.Collections;
using WhatPumpkin.Entities;
#endregion

namespace WhatPumpkin.Actions {

	/// <summary>
	/// Play cut scene.
	/// </summary>

	public class PlayCutScene : ActionType {

		#region methods

		/// <summary>
		/// has the cut scene ended.
		/// </summary>

		bool hasCutSceneEnded = false;

		/// <summary>
		/// Activate this instance.
		/// </summary>


		public PlayCutScene() {
			_name = "PlayCS";
		}

		public override IEnumerator BeginAction(params object[] parameters) {
				

			// Make sure there is a parameter
			if (parameters.Length > 0) {

				// Play the cut scene
				GameController.CutsceneManager.StartCutscene(parameters[0].ToString());

				CutScenes.CutScene.EndCutscene+=HandleCSEnd;

			}
			else {
				Debug.LogError("No cutscene is defined for the play cutscene command.");
				
				EndAction ();
				yield break;
			}

			// Don't end this action until the cutscene has ended
			while (!hasCutSceneEnded) {
				yield return null;
			}

			// Clear this
			hasCutSceneEnded = false;

			EndAction ();
			yield break;

		}

		void HandleCSEnd() {
		
			// The cut scene has ended
			hasCutSceneEnded = true;

		}

		#endregion

	}
}