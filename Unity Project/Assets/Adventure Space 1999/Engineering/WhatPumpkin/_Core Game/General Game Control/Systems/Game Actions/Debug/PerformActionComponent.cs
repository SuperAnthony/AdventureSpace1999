#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - August 5th, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;
#endregion

namespace WhatPumpkin.Debuging {

	/// <summary>
	/// Perform Action
	/// This monobehaviour uses the debug console tech to quickly perform an action
	/// I'm using this for events with cutscenes
	/// Allthough it is in the debug namespace it will be used in game since I can quickly leverage the tech
	/// </summary>

	public class PerformActionComponent : MonoBehaviour {

		/// <summary>
		/// Performs a specified action followed by it's arguments
		/// Implementation: ActionName,Argument 1, Argument 2, etc.
		/// Example: SetNar,NAR_EAT_COOKIE
		/// Example: PlayBark,joey,BARK_DRANK_MILK
		/// </summary>
		/// <param name="_actionAndArgs">_action and arguments.</param>

		public void PerformAction(string _actionAndArgs) {
		
			// Split into multiple functions

			string [] functions = _actionAndArgs.Split (";".ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);

			foreach (string function in functions) {
//				Debug.Log ("Function: " + function);
				DebugConsole.ReceiveCommand (function, true);
			}
		}


	}
}
