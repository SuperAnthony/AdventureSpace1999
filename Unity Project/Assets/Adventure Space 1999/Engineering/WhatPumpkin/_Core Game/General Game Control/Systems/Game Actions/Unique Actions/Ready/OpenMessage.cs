#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - July 20, 2015
#endregion 


#region using
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using WhatPumpkin.Screens;
#endregion

namespace WhatPumpkin.Actions {

	/// <summary>
	/// Disables a specified object when used in an action sequence.
	/// </summary>

	public class OpenMessage : ActionType {

		/// <summary>
		/// The name.
		/// </summary>
		
		public const string NAME = "Message"; 

		
		#region argument receiver

		
		Type [] _validTypes = new Type[] {typeof(IGameScreen)};
		
		
		protected override Type[] ValidTypes {
			get {
				return _validTypes;
			}
		}
		
		List<IKeyed> _validArguments = new List<IKeyed> ();
		
		
		protected override List<IKeyed> ValidArguments {
			get {
				return _validArguments;
			}
		}
		
		
		#endregion


		#region methods

		public OpenMessage() {
			_name = NAME;
		}

		public override IEnumerator BeginAction(params object[] parameters) {



			// Make sure there is a parameter
			if (parameters.Length > 0) {

				// Get all of the arguments
				List<string> arguments = GetArguments(parameters[0].ToString());
				string message = arguments[0];

				// Get the options from those arguments
				// Remove the message key
				arguments.RemoveAt(0);
				// Get all the remaining options keys
				string [] optionKeys = new string[arguments.Count];
				for(int i = 0; i < arguments.Count; i++) {
					optionKeys[i] = arguments[i];
				}


				GameController.MessageManager.OpenMultiOptionMessage(message, optionKeys);

			}

			
			// End the action
			EndAction ();
			yield break;
		}

		#endregion

	}
}
