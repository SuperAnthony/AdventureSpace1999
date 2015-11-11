#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - December 19, 2014
#endregion 


#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#endregion

namespace WhatPumpkin.Actions {

	/// <summary>
	/// This action changes the state of the active PC
	/// </summary>

	public class ChangePCState : ActionType {

		/// <summary>
		/// The name.
		/// </summary>
		
		public const string NAME = "ChangePCState"; 


		#region methods

		public ChangePCState() {
			_name = NAME;
		}

		public override IEnumerator BeginAction(params object[] parameters) {

			// Make sure there is a parameter
			if (parameters.Length > 0) {

				// Make sure there is a parameter
				if (parameters.Length > 0) {
					
					// Get the names of each of the target objects
					List<string> arguments = ScriptingLanguage.Scripting.GetArguments(parameters[0].ToString());

					// There should be two arguments
					// Arg 1: The State Type
					// Arg 2: The State that's being changed to
				
					if(arguments != null && arguments.Count > 0) {

						// Change the active pc's state
						GameController.PartyManager.ActivePC.ChangeStateTo(arguments[0], arguments[1]);

					}
				}
				
			}

			
			// End the action
			EndAction ();
			yield break;
		}


		#endregion

	}
}
