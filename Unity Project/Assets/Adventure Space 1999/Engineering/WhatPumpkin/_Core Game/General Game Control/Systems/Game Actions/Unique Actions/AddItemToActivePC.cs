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


	public class AddItemToActivePC : ActionType {
	
		/// <summary>
		/// Activate this instance.
		/// </summary>
		
		public AddItemToActivePC() {
			_name = "Get";
		}
		
		public override IEnumerator BeginAction(params object[] parameters) {

			// TODO: Make sure I'm creating new items when necessary
			// And only passing around items when not necessary
			// If I'm getting an item by the item type from the inventory manager
			// Then this means I need to CREATE an item
			// If I'm locating an item through other means then I should only pass that item around


			// Make sure there is a parameter
			if (parameters.Length > 0) {

				//Debug.Log("Get");

				// Find the game object based on the name and move the transform to it
			

				List<string> arguments = Scripting.GetArguments(parameters[0].ToString());

				// TODO: Abstract this data

				if(arguments.Count > 0) {


					PlayerCharacter pc = GameController.PartyManager.ActivePC;
					IContainer container = pc.GetComponent(typeof(IContainer)) as IContainer;

					// TODO: Temp for ECCC Build

					for(int i = 0; i < arguments.Count; i++) {

						Item itemType = GameController.InventoryManager.FindItemTypeByKey(arguments[i]);
						if(itemType != null) {
							container.AddItemToNextEmpty(new Item(itemType) );
							// Amount
							//int amount = 0;
							//int.TryParse(arguments[0], out amount);
							itemType.Amount = 1;
						}
					}

					//Debug.Log ("Item Key: " + arguments[0]);
			
					/*
					// Go through the remaining arguments and add the item
					for(int i = 0; i < arguments.Count; i++) {

						Debug.Log ("Item: " + arguments[i]);

						Item itemType = GameController.InventoryManager.FindItemTypeByKey(arguments[i]);

						Debug.Log ("Item Type: " + itemType);

						if(itemType != null) {
							container.AddItemToNextEmpty(new Item(itemType) );
							// Amount
							int amount = 0;
							int.TryParse(arguments[i], out amount);
							itemType.Amount = amount;
						}
					}*/

					/*
					// Get Quantity
					if(arguments.Count > 0) {
					PlayerCharacter pc = GameController.ActivePlayerCharacter;
					IContainer container = pc.GetComponent(typeof(IContainer)) as IContainer;

					//Debug.Log ("Item Key: " + arguments[0]);

					Item itemType = GameController.InventoryManager.FindItemTypeByKey(arguments[0]);

					if(arguments.Count > 1) {

						// Amount
						int amount = 0;
						int.TryParse(arguments[1], out amount);
						itemType.Amount = amount;
					}
					else {
						if(itemType != null){itemType.Amount = 1;}
					}

					if(itemType != null) {
						// Create a new item based on the item type 
						Debug.Log ("Attempt to add item");
						container.AddItemToNextEmpty(new Item(itemType) );
					}
					else {
						Debug.LogError("Could not find the item type.");
					}
				}
					}*/
				}
			}
		

			// Let the game know that you can end the action
			EndAction ();
			yield break;

		}
	
	}
}
