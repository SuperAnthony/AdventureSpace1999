#region Copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - January 29, 2015
#endregion

#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WhatPumpkin.ScriptingLanguage;
#endregion

namespace WhatPumpkin.Actions {

	/// <summary>
	/// Closeup action.
	/// </summary>

	public class Closeup : ActionType {
	
		#region methods

		/// <summary>
		/// Activate this instance.
		/// </summary>
		
		public Closeup() {
			_name = "Closeup";
		}
		
		public override IEnumerator BeginAction(params object[] parameters) {

			// Make sure there is a parameter
			if (parameters.Length > 0) {

				// Get the arguments
				List<string> arguments = Scripting.GetArguments(parameters[0].ToString());

				if(arguments[0] !=null) {

					//Debug.Log ("Closeup Cam Name: " + arguments[0]);

					GameController.CamManager.StartCloseup(arguments[0]);
				}
				else
				{
					Debug.LogWarning("Could not find the object '" + arguments[0] + "' when attempting to invoke a closeup");
				}

				/*
				if(arguments.Count > 1) {

					// Set the back button to play this action sequence
				

				}*/
					
			}
			else {
				Debug.LogError("No paramaters defined for closupe command.");
			}



			EndAction ();

			return null;
		}

		#endregion
	
	}
}
