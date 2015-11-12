#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - December 19, 2014
#endregion 
 

#region using
using UnityEngine;
using System.Collections;
using WhatPumpkin.Entities.Inventory;
#endregion

namespace WhatPumpkin.Actions {

	/// <summary>
	/// Disables a specified object when used in an action sequence.
	/// </summary>

	public class OpenContainer : ActionType {


		#region methods

		public OpenContainer() {
			_name = "OpenContainer";
		}

		public override IEnumerator BeginAction(params object[] parameters) {

			IContainer container = null;

			// Make sure there is a parameter
			if (parameters.Length > 0) {

				// TODO: Will need something that keeps track of the latest selected

				// If the parameter is 'this' then use the object in the selected inventory slot
				if(parameters[0].ToString() == "this") {
					Debug.Log(InventorySlot.SelectedSlot.ReferencedItem);
					container = (IContainer)InventorySlot.SelectedSlot.ReferencedItem;
				}


			
				// If a container was found then open it
				if(container != null) {
					container.Open();
				}
			}

			
			// End the action
			EndAction ();
			yield break;
		}


		#endregion

	}
}
