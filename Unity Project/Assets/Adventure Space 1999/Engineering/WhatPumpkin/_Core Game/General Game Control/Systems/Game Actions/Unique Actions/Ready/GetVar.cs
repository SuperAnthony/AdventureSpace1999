#region copyright (c) 2015 What Pumpkin Studio
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
#endregion 

#region using
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using WhatPumpkin.ScriptingLanguage;
#endregion

namespace WhatPumpkin.Actions {

	public class GetVar : ActionType {

		#region argument receiver
		
		Type [] _validTypes = new Type[] {typeof(IGameVariable)};
		

		protected override Type[] ValidTypes {
			get {
				return _validTypes;
			}
		}
		
		List<IKeyed> _validArguments = new List<IKeyed> ();
		
		
		/// <summary>
		/// Gets the valid arguments.
		/// </summary>
		/// <value>The valid arguments.</value>
		
		protected override List<IKeyed> ValidArguments {
			get {

				return _validArguments;
			}
		}
		

		#endregion

		/// <summary>
		/// The name.
		/// </summary>
		
		public const string NAME = "GetVar"; 


		#region methods

		public GetVar() {
			_name = NAME;
		}

		public override IEnumerator BeginAction(params object[] parameters) {

			// Make sure there is a parameter
			if (parameters.Length > 0) {

				// Get the variable name and the value
				string variableName = parameters[0].ToString();
				Debug.Log (variableName + ": " + DialogueLua.GetVariable(variableName).AsString);


			}
			else {
				Debug.LogError("No parameters found for the GetVar action.");
			}

			// End the action
			EndAction ();
			yield break;
		}

		#endregion


	}
}
