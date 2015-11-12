#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - December 16, 2014
#endregion 

#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WhatPumpkin.ScriptingLanguage;
#endregion

namespace WhatPumpkin.Actions {

	/// <summary>
	/// Move to target.
	/// </summary>


	public class SetMaterial : ActionType {
	
		/// <summary>
		/// Activate this instance.
		/// </summary>
		
		public SetMaterial() {
			_name = "SetMat";
		}
		
		public override IEnumerator BeginAction(params object[] parameters) {

			List<string> arguments = Scripting.GetArguments (parameters [0].ToString ());

			// Make sure there is a parameter
			if (parameters.Length > 0) {

				// Get the arguments
				GameObject targetObject = null;
				Material material = null;
				int materialNumber = 0;

				// Find the target
				if(arguments[0] != null){targetObject =  GameObject.Find(arguments[0]);}

				// Get the material
				if(arguments[1] != null){material = GameController.ArtAssetController.GetMaterial(arguments[1]);}

				if(arguments[2] != null){
				
					int.TryParse(arguments[2], out materialNumber);

				}

				if(targetObject != null && material != null) {
					if(targetObject.gameObject.GetComponent<Renderer>().materials.Length > materialNumber) {
						targetObject.gameObject.GetComponent<Renderer>().materials[materialNumber] = material;
					}
					else {
						Debug.LogError ("Materials index is out of range");
					}
				}
				else {
					Debug.LogError("Object and/or Material. Please check for correct spellings and that the correct game object is in the scene named properly.");
				}
			}
			else {
			
			}

			// Let the game know that you can end the action
			EndAction ();
			yield break;

		}
	
	}
}
