// Date: November 26th, 2014

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using WhatPumpkin.Entities.Inventory;

namespace WhatPumpkin.Screens { // TODO: Proper folder | and different namespace

	/// <summary>
	/// Container.
	/// </summary>

	public class ContainerScreen : GameScreen {

		// Instance Fields

		/// <summary>
		/// The inventory slots associated with this container.
		/// </summary>

		[SerializeField] List<InventorySlot> _inventorySlots = new List<InventorySlot>(); 


		/// <summary>
		/// The container that this screen is refering to (can be an object or character).
		/// </summary>

		IContainer _container; 


		/// <summary>
		/// The slot offset.
		/// </summary>

		[SerializeField] int _slotOffset = 0;


		// Instance Properties

		void Awake() {
		
			// Register to the PC Activated event. When a PC is activated set the slot offset back to 0

			WhatPumpkin.Sgrid.Characters.PlayerCharacter.PCActivated += HandlePCActivated;


		}

		void HandlePCActivated (object sender, EventArgs e)
		{
			// When a pc is activated set the slot offset back to 0
			_slotOffset = 0;
		}

		/// <summary>
		/// Gets the container.
		/// </summary>
		/// <value>The container.</value>

		public IContainer Container { get { return _container; } 
			set { 
				// Switch the container
				_container = value; 
				// Update the display
				UpdateDisplay();
			} 
		
		}

		/// <summary>
		/// Gets the slot offset.
		/// </summary>
		/// <value>The slot offset.</value>

		public int SlotOffset { get { return _slotOffset; } }


		// Methods

		public override void Close() {
		
			// Close the referenced container
			_container.Close ();

			// Close this screen by deactivating the gameobject
			this.gameObject.SetActive (false);
		}

		/// <summary>
		/// Updates the display.
		/// </summary>
		/// 
		public void UpdateDisplay(){

	//		Debug.Log ("Container Screen - UpdateDisplay()" + _container.ToString ());

			// Populate all of the inventory slots
			DisplayContainerItems(_container);
		}

		/// <summary>
		/// Adds the child inventory slots.
		/// </summary>

		void AddChildInventorySlots() {
			// Get slots from the child objects at the start
			foreach (Transform child in this.transform) {
				
				// Does this child contain an inventory slot
				InventorySlot inventorySlot = child.gameObject.GetComponent<InventorySlot>();
				if(inventorySlot != null) {
					// Add this inventory slot to the list
					_inventorySlots.Add(inventorySlot);
				}
				
				
			}
		
		}


		/// <summary>
		/// Shows the inventory in next available slot.
		/// </summary>
		/// <param name="inv">Inv.</param>

		public void ShowInventoryInNextSlot(IItem inv) {
			// Find the next empty inventory slot and add it
			foreach(InventorySlot iSlot in _inventorySlots) {
				// If this slot is empty then add the inventory and break out of this loop
				if(iSlot.IsEmpty()) {
					// Add inventory
					iSlot.ShowInventoryInfo(inv);
					// Break
					break;
				}
			}
		}


		/// <summary>
		/// Switchs the cointainer.
		/// </summary>
		/// <param name="container">Container.</param>
		
		public void SwitchCointainer(IContainer container) {
			// Change the container
			_container = container;
			// Update the display
			UpdateDisplay ();
		}

		/// <summary>
		/// Displays the inventory from container.
		/// </summary>
		/// <param name="container">Container.</param>

		void DisplayContainerItems(IContainer container) {

	//		Debug.Log ("Display Container Items");

			int userItemIndex;
			int slotItemIndex = 0;

//			Debug.Log ("Container: " + container);

			// Search through each item starting at the offset
			for(userItemIndex = _slotOffset; userItemIndex < container.Items.Count; userItemIndex++) {
			
				try {

					//Debug.Log ("Try");

					//Debug.Log ("Count: " + _inventorySlots.Count);
					//Debug.Log ("Slot item index: " + " num: " + slotItemIndex + " slot: " + _inventorySlots[slotItemIndex]);
					// Show info
					try {
						_inventorySlots[slotItemIndex].ShowInventoryInfo(container.Items[userItemIndex]);
					
					}
					catch {
					
					//	Debug.Log("Argument Out of Range?");

					}
					// Activate slot
					try {
					_inventorySlots[slotItemIndex].Activate();
					}
					catch {
					//	Debug.Log("Argument Out of Range?");
					}

				}
				catch (Exception e) {


					Debug.LogException(e);

				}
			
				// Move to the next slot
				slotItemIndex++;
				// Item Info

			}

			// Deactivate Remaining Slots that are not actually available in the container
			// Debug.Log ("Remaining Items: " + userItemIndex);
			for(userItemIndex = slotItemIndex; userItemIndex < _inventorySlots.Count; userItemIndex++) {
				// Deactivate any nodes that the player's don't have
				_inventorySlots[userItemIndex].Deactivate();
			}


		}

		/// <summary>
		/// Moves to next item.
		/// </summary>
		/// <param name="numItems">Number of items.</param>

		public void MoveToNextItem(int numItems) {

			if (_slotOffset == GameController.PartyManager.ActivePC.Items.Count - 1) {
				return;			
			}

			// Increase the offset
			_slotOffset += numItems;

			UpdateDisplay ();
		
		}

		/// <summary>
		/// Moves to previous item.
		/// </summary>
		/// <param name="numItems">Number of items.</param>

		public void MoveToPrevItem(int numItems) {
			// Decrease the offset
			_slotOffset -= numItems;
			// Make sure the offset isn't trying to select an element that doesn't exist 
			// by being less than zero
			if(_slotOffset < 0){_slotOffset = 0;}

			UpdateDisplay ();

		}

		/// <summary>
		/// Gets the next empty slot.	
		/// </summary>
		/// <returns>The next empty slot.</returns>

		private InventorySlot GetNextEmptySlot() {
			// TODO: This can be optimized | Though there isn't too much to search through
			foreach (InventorySlot iSlot in _inventorySlots) {
				
				if(iSlot.IsEmpty()){return iSlot;}
			}
			
			return null; // No Empty slots 
		}


	}
}
