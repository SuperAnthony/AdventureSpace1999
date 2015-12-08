using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WhatPumpkin.ScriptingLanguage;

namespace WhatPumpkin.Actions {

	public class ChangeScene : ActionType {
	
		/// <summary>
		/// Activate this instance.
		/// </summary>
		
		public ChangeScene() {
			_name = "ChangeScene";
		}

		List<string> arguments;
		
		public override IEnumerator BeginAction(params object[] parameters) {

			// Make sure there is a parameter
			if (parameters.Length > 0) {

				// Get the arguments
				arguments = Scripting.GetArguments(parameters[0].ToString());

                // Load the scene
                if (arguments.Count > 2)
                {
                    GameController.SceneManager.LoadScene(arguments[0], arguments[1], arguments[2]); // Choose a load scene
                }
                else if (arguments.Count > 1)
                {
                    GameController.SceneManager.LoadScene(arguments[0], arguments[1]); // Default (no load screne)
                }
                else
                {
                    Debug.LogError("The Change Scene action requires at least 2 parameters (Scene Name, Spawn Point)");
                }
                
			}
			else {
				Debug.LogError("No parameters defined for Change Scene action");
			}

			EndAction ();

			return null;
		}
        
	}
}
