#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 6, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using WhatPumpkin.ScriptingLanguage;
using WhatPumpkin.Actions;
#endregion


namespace WhatPumpkin.Debuging {

	/// <summary>
	/// Debug console. Allows developer to perform actions
	/// </summary>

	public class DebugConsole {

		#region properties

		/// <summary>
		/// Is the debug console active
		/// </summary>

		static public bool IsActive { get; internal set; }

		#endregion

		#region methods

		/// <summary>
		/// Switch between active state or deactive state.
		/// </summary>

		static internal void Switch() {
		
			IsActive = !IsActive;

		}


		/// <summary>
		/// Receives the command.
		/// </summary>
		/// <param name="command">Command.</param>

		static internal void ReceiveCommand(string command, bool overrideActiveRequirement = false) {

			if(IsActive || overrideActiveRequirement) {

				// Get all the arguments from the command
				List<string> arguments = Scripting.GetArguments (command);

				// Temp - for now I'll just pass one parameter for testing
				string actionType = arguments [0];
				//string parameters = arguments [1];

				// TODO: Temp - this can definitely be done better
				string parameters = command.Replace(actionType+",",""); 

//				Debug.Log ("Attempt to play action");

				// Play the action
				PlayAction (actionType, parameters);
			}
		
		}

		/// <summary>
		/// Plays an action.
		/// </summary>
		/// <param name="actionType">Action type.</param>
		/// <param name="parameters">Parameters.</param>

		static void PlayAction(string actionType, string parameters) {


			if (GameController.ActionControl != null) {
								GameController.ActionControl.PerformAction (ActionTypeCollection.Instance.GetActionType (actionType), parameters);
						}
		
		}

		#endregion

	}
}