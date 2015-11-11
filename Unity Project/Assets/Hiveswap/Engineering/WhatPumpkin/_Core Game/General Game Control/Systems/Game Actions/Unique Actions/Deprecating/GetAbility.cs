// TODO: February 26, 2015

#region using
using System;
using UnityEngine;
using System.Collections;
#endregion

namespace WhatPumpkin.Actions {

	[Obsolete("Get Ability is Obsolete, use Activate instead")]

	public class GetAbility : ActionType {

		#region methods

		/// <summary>
		/// Deactivate this instance.
		/// </summary>

		public GetAbility() {
			_name = "GetAb";
		}

		public override IEnumerator BeginAction(params object[] parameters) {
			/*
			// Make sure there is a parameter
			if (parameters.Length > 0) {
				
				// Add the ability to the active player character
				if(GameController.PartyManager.ActivePC != null) {
					GameController.PartyManager.ActivePC.AddAbility(parameters[0].ToString());
				}
			}
			else {
				Debug.LogError("No parameters found for the get ability action.");
			}*/

			EndAction ();
			yield break;

		}

		#endregion

	}
}