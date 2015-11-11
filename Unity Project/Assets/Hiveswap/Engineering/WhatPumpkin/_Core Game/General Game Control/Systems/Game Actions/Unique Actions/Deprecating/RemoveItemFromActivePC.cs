#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - December 16, 2014
#endregion 

#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WhatPumpkin.Entities.Inventory;
using WhatPumpkin.Sgrid.Characters;
using WhatPumpkin.ScriptingLanguage;
#endregion

namespace WhatPumpkin.Actions {

	/// <summary>
	/// Move to target.
	/// </summary>


	public class RemoveItemFromActivePC : ActionType {
	
		/// <summary>
		/// Activate this instance.
		/// </summary>
		
		public RemoveItemFromActivePC() {
			_name = "Lose";
		}
		
		public override IEnumerator BeginAction(params object[] parameters) {

			// Make sure there is a parameter
			if (parameters.Length > 0) {

				// Find the game object based on the name and move the transform to it
			

				List<string> arguments = Scripting.GetArguments(parameters[0].ToString());

				// TODO: Abstract this data

				if(arguments.Count > 0) {
					PlayerCharacter pc = GameController.PartyManager.ActivePC;
					IContainer container = pc.GetComponent(typeof(IContainer)) as IContainer;
			

					// Go through the remaining arguments and add the item
					for(int i = 0; i < arguments.Count; i++) {

						container.RemoveItem(arguments[i]);

					}

			
				}
			}
		

			// Let the game know that you can end the action
			EndAction ();
			yield break;

		}
	
	}
}
