#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 4, 2015
#endregion 


#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#endregion

namespace WhatPumpkin.Actions {

	/// <summary>
	/// Show a message prompt with multiple options.
	/// </summary>

	public class ShowPrompt : ActionType {

		/// <summary>
		/// The name.
		/// </summary>
		
		public const string NAME = "ShowPrompt"; 


		#region methods

		public ShowPrompt() {
			_name = NAME;
		}

		public override IEnumerator BeginAction(params object[] parameters) {

			//Debug.Log ("Enable: " + parameters);

			// Make sure there is a parameter
			if (parameters.Length > 0) {

				// Make sure there is a parameter
				if (parameters.Length > 0) {
					
					// Get the names of each of the target objects
					List<string> keys = ScriptingLanguage.Scripting.GetArguments(parameters[0].ToString());

					// Key 1: The message key
					// Key 2: Button Option 1
					// Key 3: Button Option 2
					// Key 4: Button Option 3


					string [] optionKeys = new string[3];

					for(int i = 0; i < optionKeys.Length; i++) {optionKeys[i] = keys[i + 1];}

					MessageManager.Instance.OpenMultiOptionMessage(keys[0], optionKeys);

				}
				
			}

			
			// End the action
			EndAction ();
			yield break;
		}


		#endregion

	}
}
