#region Copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - January 9, 2014
#endregion

#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#endregion

namespace WhatPumpkin.Entities.Inventory {


	/// <summary>
	/// Container. Primarily used for items with contents and the player container. 
	/// Perhaps a treasure chest object cou
	/// </summary>

	public abstract class Container : MonoBehaviour, IContainer {


		#region fields

		/// <summary>
		/// The entity's inventory.
		/// </summary>
		
		protected List<IItem> _items = new List<IItem> (); 

		/// <summary>
		/// Is this container open?
		/// </summary>
		
		protected bool _isOpen = false;

		/// <summary>
		/// Max number of items this object can hold
		/// </summary>
		
		[SerializeField] protected int _maxItems = 0; 

		// Properties

		/// <summary>
		/// Gets the items.
		/// </summary>
		/// <value>The items.</value>

		#endregion

		#region properties
		
		public virtual List<IItem> Items { get { return _items; }} 

		/// <summary>
		/// Gets the max inventory slots for this player character instance.
		/// </summary>
		/// <value>The max inventory slots.</value>
		
		public int MaxItems { get { return _maxItems; } }

		/// <summary>
		/// Gets a value indicating whether this container is open.
		/// </summary>
		/// <value>true</value>
		/// <c>false</c>
		
		public bool IsOpen { get { return _isOpen; } }

		// Abstract Methods

		/// <summary>
		/// Open this instance of the container.
		/// </summary>
		
		public abstract void Open ();

		/// <summary>
		/// Updates the container display.
		/// </summary>


		public abstract void UpdateContainerDisplay ();

		// Methods

		/// <summary>
		/// Close this instance.
		/// This method should be invoked whenever the player closes a container screen
		/// </summary>
		
		public void Close() {
			// This item is closed. 
			_isOpen = false;
		}



		/// <summary>
		/// Adds an item to the container slot.
		/// </summary>
		/// <param name="item">Item.</param>
		/// <param name="containerSlot">Container slot.</param>

		public void AddItem(IItem item, int containerSlot) {

			// Add Item
			try {

				_items [containerSlot] = item;
			}
			catch {
				Debug.LogError("Error - todo - fix this");
			}

//			Debug.Log ("Update Container Display");

			// Update Display
			UpdateContainerDisplay();

		}

		/// <summary>
		/// Adds the item to next empty container slot.
		/// </summary>
		/// <returns><c>true</c>, if item to next empty was added, <c>false</c> otherwise.</returns>
		/// <param name="item">Item.</param>
		
		public bool AddItemToNextEmpty(IItem item) {

			// TODO: Very minor optimization: 
			// I could keep track of which item is the next empty to prevent searching through items
			// This seems uncecessary though because I don't think there will be too many items to search
			// Through for any container and this will not be noticable, but it's fun to know that I can

			// The container slot that the item will be added to
			int slot = 0;

			// Search through the container's items
			foreach(Item i in _items) {

				//Debug.Log ("Item i: " + i.Key);

				// Check to see if the item is empty
				if(i.IsEmpty()) { 	
					//Debug.Log ("Item Added");
					// An empty slot was found, return true
					AddItem (item, slot);
					return true;
				}
				
				// Move to the next slot
				slot++;
			}

			// No empty slots detected
			return false;
		}

		/// <summary>
		/// Determines whether this instance has the items with the specified keys.
		/// </summary>
		/// <returns><c>true</c> if this instance has items the specified keys; otherwise, <c>false</c>.</returns>
		/// <param name="keys">Keys.</param>

		public bool HasItems(List<string> keys) {
		
			foreach (string key in keys) {
			
				// If any of these return false then the user does not have
				// all of the required items, therefore return false

				if(!HasItem(key)){return false;}


			}

			// If none of the items returned false then the user
			// has all of the items and we can return true

			return true;

		}

		/// <summary>
		/// Determines whether this instance has an item of the specified key.
		/// </summary>
		/// <returns><c>true</c> if this instance has item the specified key; otherwise, <c>false</c>.</returns>
		/// <param name="key">Key.</param>

		public bool HasItem(string key) {


			foreach(Item item in _items) {

				if(item.Key == key) {
					return true;
				}
			
			}

			return false;

		}

		/// <summary>
		/// Removes the specified item from the container.
		/// </summary>
		/// <param name="item">Item.</param>
		/// <param name="itemKey">Item key.</param>

		public void RemoveItem(string itemKey) {
		
			foreach (IItem item in _items) {
			
				if(item.Key == itemKey) {
					RemoveItem(item);
				}

			}
		
		}
	
		/// <summary>
		/// Removes the item from this container's inventory.
		/// </summary>
		/// <param name="item">Item.</param>

		public void RemoveItem(IItem item) {
		
			// Search for the item in the player's inventory and then remove it
			for (int i = 0; i < _items.Count; i++) {
			
				if(_items[i] == item) {
					_items[i] = InventoryManagement.Instance.GetEmptyItem();
				}
			
			}

			// Update the display
			UpdateContainerDisplay ();
		}

		#endregion


	}
}