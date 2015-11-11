#region using
using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using WhatPumpkin.ScriptingLanguage;
#endregion

namespace WhatPumpkin.Actions {

	public class AddVar : ActionType {

		#region methods

		public AddVar() {
			_name = "AddVar";
		}

		public override IEnumerator BeginAction(params object[] parameters) {

			// TODO: Use Scripting/Get Arguments

			Debug.Log ("Begin Set Var Action");

			// Make sure there is a parameter
			if (parameters.Length > 0) {

				// Get the variable name and the value
				string script = parameters[0].ToString();
				string variableName;
				float incrementValue = 0; // The amount that will be incremented by

				// Get the variable name 
				variableName=Scripting.GetVariableNameFromAssignment(script);
				// Get the assignment value as a string 
				string s_value = Scripting.GetValueFromAssignment(script);

				// Try to convert the string into a number if possible
				if(float.TryParse(s_value, out incrementValue))
				{

					// Add
					float originalValue = 0;
					originalValue = DialogueLua.GetVariable(variableName).AsInt;

					// Add value
					DialogueLua.SetVariable(variableName, originalValue + incrementValue);
				}
				else{

					Debug.LogError("Could not convert the assignment to a number value");

				}

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
