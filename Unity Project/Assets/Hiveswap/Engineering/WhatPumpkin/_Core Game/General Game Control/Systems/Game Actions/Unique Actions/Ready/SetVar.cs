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

	public class SetVar : ActionType {

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
		
		public const string NAME = "SetVar"; 


		#region methods

		public SetVar() {
			_name = NAME;
		}

		public override IEnumerator BeginAction(params object[] parameters) {

			// Make sure there is a parameter
			if (parameters.Length > 0) {

				/*
				string script = parameters[0].ToString();
				string variableName;

				//float value = 0;
				
				// Remove spaces
				script = script.Replace(" ", "");
				
				// Get the variable name 
				variableName=Scripting.GetVariableNameFromAssignment(script);
				// Get the assignment value as a string 
				string s_value = Scripting.GetValueFromAssignment(script);


				// Attempt to get the game variable and assign it
				IGameVariable gameVariable = (IGameVariable)Keyed.KeyedDictionary[variableName];
				if(gameVariable != null) {gameVariable.Assign(s_value);}
				*/
				/*
				// Try to convert the string into a number if possible
				if(float.TryParse(s_value, out value))
				{
					// If the parse is successfull then handle the number variable
					// Set the variable as a number value
					DialogueLua.SetVariable(variableName, value);
				}
				else{
					
					// If the parse was not successfull then handle as a string value
					// Set the variable as a string value
					DialogueLua.SetVariable(variableName, s_value);
					
					
				}*/

				// Get the variable name and the value

				string script = parameters[0].ToString();
				string variableName;
				float value = 0;

				// Remove spaces
				script = script.Replace(" ", "");

				// Get the variable name 
				variableName=Scripting.GetVariableNameFromAssignment(script);
				// Get the assignment value as a string 
				string s_value = Scripting.GetValueFromAssignment(script);

				// Try to convert the string into a number if possible
				if(float.TryParse(s_value, out value))
				{
					// If the parse is successfull then handle the number variable
					// Set the variable as a number value
					DialogueLua.SetVariable(variableName, value);
				}
				else{

					// If the parse was not successfull then handle as a string value
					// Set the variable as a string value
					DialogueLua.SetVariable(variableName, s_value);


				}

				// Let the game controller know a variable was set
				GameController.GameVariableController.OnVariableSet(variableName, s_value);
				GameController.GameVariableController.AssignVariable(variableName, s_value);

			}
			else {
				Debug.LogError("No parameters found for the SetVar action.");
			}

			// End the action
			EndAction ();
			yield break;
		}

		#endregion


	}
}
