#region Copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - January 9, 2015
#endregion


#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#endregion

namespace WhatPumpkin.Entities.Inventory {

	/// <summary>
	/// Player inventory.
	/// </summary>

	public class PlayerInventory : Container {


		#region methods

		void Start() {

			InitItems ();


			// Exit method if this is not the active player character
			// TODO: Yes, this is pretty hackey
			if (!this.GetComponent<WhatPumpkin.Sgrid.Characters.PlayerCharacter> ().IsActive) {return;}

			InventoryManagement.Instance.SelectedPlayerContainerScreen.SwitchCointainer ((IContainer)this);
			InventoryManagement.Instance.SelectedPlayerContainerScreen.UpdateDisplay ();

		
		}

		/// <summary>
		/// Initializes the items.
		/// </summary>
		
		void InitItems() {
			
			// Fill items with none items
			for (int i = 0; i < _maxItems; i++) {
				// Add a none item
				_items.Add(InventoryManagement.Instance.GetEmptyItem());
				
			}
			
			// Now update the display
			UpdateContainerDisplay ();
			
		}

		/// <summary>
		/// Empties the inventory. This was built for the ECCC demo, I'd be skeptical to keep this public
		/// </summary>

		public void EmptyInventory() {
		
			_items.Clear ();
			InitItems ();

		}

		/// <summary>
		/// Updates the container display.
		/// </summary>

		public override void UpdateContainerDisplay() {

//			Debug.Log ("Update Container Display");
			// Update Display
			// TODO: I don't like this - fix
			GameController.InventoryManager.SelectedPlayerContainerScreen.UpdateDisplay ();


		}

		/// <summary>
		/// Open this instance of the container.
		/// </summary>
		
		public override void Open() {
			// Tell the container screen what item it is refering to
			InventoryManagement.Instance.SelectedPlayerContainerScreen.Container = (IContainer)this;
			
			// Open the container screen
			InventoryManagement.Instance.SelectedPlayerContainerScreen.Activate();

			// The container is open
			_isOpen = true;
		}

		#endregion

	}
}