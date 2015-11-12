#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - TODO
#endregion 

#region using
using UnityEngine;
using System.Collections;
#endregion


namespace WhatPumpkin.Actions {

	/// <summary>
	/// Use state.
	/// </summary>

	public class Use : ActionType {

		#region methods

		public Use() {
			_name = "Use";
		}

		public override IEnumerator BeginAction(params object[] parameters) {

			Debug.Log ("Begin Use Action");

			// Make sure there is a parameter
			if (parameters.Length > 0) {

				// If the parameter is "this" then use the most recently selected key name
				string usingObject = parameters [0].ToString();

				Debug.Log ("Using Object: " + usingObject);

				if(usingObject == "this" || usingObject == "") {

					usingObject = GameController.Instance.SelectedEntity.Key;
				}


				// TODO: The game state should be changed to a use state
				// This is a temp quick way for me to test


				GameController.CombineItemController.StartCombineMode(usingObject);
				
			}
			else {
				Debug.LogError("No parameters found for the use action.");
			}
			
			// End the action
			EndAction ();
			yield break;
		}

		#endregion

	}
}
