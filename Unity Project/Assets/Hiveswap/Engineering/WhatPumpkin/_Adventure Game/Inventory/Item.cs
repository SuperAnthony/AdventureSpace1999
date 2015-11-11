#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - October, 2014
#endregion 

/// <summary>
/// Item. Items will be used in container inventory
/// </summary>

#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WhatPumpkin.Localization;
using WhatPumpkin.Actions.Sequences; 
using WhatPumpkin.Entities;
#endregion


// TODO: Remove this from the entities namespace

namespace WhatPumpkin.Entities.Inventory {


	[System.Serializable]

	public class Item : Keyed, IItem, IContainer, IEntityDescriptionText, IEntity, IOpenVerbCoin {

		#region fields

		/// <summary>
		/// The identifier
		/// </summary>

		[SerializeField] int _id; // This is an artifact

		/// <summary>
		/// The key name.
		/// </summary>

		//[SerializeField] string _key;

		/// <summary>
		/// The name of the item. 
		/// </summary>
		[SerializeField] string _name;

		/// <summary>
		/// The description. 
		/// </summary>
		
		[SerializeField] string _description;

		/// <summary>
		/// The icon key.
		/// </summary>

		[SerializeField] string _iconKey; 

		/// <summary>
		/// The max inventory slots for this entity.
		/// 0 if it is not a container
		/// Anything greater than 0 makes this entity a container of objects
		/// </summary>


		[SerializeField] int _containerMax = 0; 
		
		/// <summary>
		/// The max amount
		/// </summary>
		
		[SerializeField] int _maxAmount = 1;

		/// <summary>
		/// The amount.
		/// </summary>

		[SerializeField] int _amount = 1;


		/// <summary>
		/// The verb action sequence.
		/// </summary>

		//[SerializeField] VerbActionSequence [] _verbActionSequence = new VerbActionSequence[6]; 
		[SerializeField] List<VerbActionSequence> _verbActionSequence = new List<VerbActionSequence>(); 

	
		/// <summary>
		/// is this container open.
		/// </summary>

		protected bool _isOpen = false;

		/// <summary>
		/// The items.
		/// </summary>

		protected List<IItem> _items = new List<IItem> (); 



		//public event System.EventHandler Destroyed; // TODO: Needed for IEntity implementation but can be removed once this inherets from Keyed, not ready to do that yet

		#endregion

		#region properties

		/// <summary>
		/// Gets the items stored in this container.
		/// </summary>
		/// <value>The items.</value>

		public List<IItem> Items { get { return _items; } }


		/// <summary>
		/// Gets a value indicating whether this instance is container.
		/// </summary>
		/// <value><c>true</c> if this instance is container; otherwise, <c>false</c>.</value>
		
		public bool IsContainer { get { return _containerMax > 0; } }
	
		// Properties
				
		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>
		
		public override string Key { get 
			{ 
				if(_key == null) {
					return "";
				}
				return _key; 
			} 
		}

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>


		public string Name {
			get
			{ 

				if(_name == null) {
					return "";
				}
				return _name; 
			} 
		} 
	

		/// <summary>
		/// Gets the identifier.
		/// </summary>
		/// <value>The identifier.</value>

		public int Id { 
			get { 
				return _id; 
			} 
		}

		/// <summary>
		/// Gets the description of the item.
		/// </summary>
		/// <value>The description.</value>

		public string Description { 
			get 
			{ 
				if(_description == null) {
					return "";
				}
				return _description; 
			} 
		} 

		/// <summary>
		/// Gets the number of remaining uses.
		/// </summary>
		/// <value>The uses.</value>

		public int MaxAmount { get { return _maxAmount; } }

		/// <summary>
		/// Gets the amount.
		/// </summary>
		/// <value>The amount.</value>

		public int Amount 
		{ 
			get 
			{ 
				if(_amount > _maxAmount){_amount = _maxAmount;}
				return _amount; 
			} 

			set {
				_amount = value;
				if(_amount > _maxAmount) { _amount = _maxAmount;}
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is open.
		/// </summary>
		/// <value>true</value>
		/// <c>false</c>

		public bool IsOpen { get { return _isOpen; } }

		/// <summary>
		/// Gets the icon.
		/// </summary>
		/// <value>The icon.</value>

		public Sprite Icon { get { return GameController.InventoryManager.GetIcon(_iconKey);}}


		/// <summary>
		/// Gets the verb sequences.
		/// </summary>
		/// <value>The verb sequences.</value>

		public List<VerbActionSequence> VerbSequences { get { return _verbActionSequence; } } 

		/// <summary>
		/// Gets the verb action sequence. This wrapper property implements the IOpenVerbCoin interface. 
		/// </summary>
		/// <value>The verb action sequence.</value>

		public VerbActionSequence [] VerbActionSequences { get 
			{ 
				VerbActionSequence [] verbActionSequences = new VerbActionSequence[VerbSequences.Count];

				for(int i = 0; i < VerbSequences.Count; i++) {
					verbActionSequences[i] = VerbSequences[i];
				}


				return verbActionSequences;
			
			}
		}

		#endregion

		#region methods

		// Methods
		public Item() {
			// TODO: For some reason this seems to get invoked twice for each item type that is created
			// What's up with that Unity?
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WhatPumpkin.HiveSwap.Inventory.Item"/> class.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="maxSlots">Max slots.</param>
		/// <param name="desc">Description.</param>

		public Item(Item itemType) {
//			Debug.Log ("Item(itemType)");

			// Initialize the item properties
			_name = itemType._name;
			_key = itemType._key;
			_description = itemType._description;
			_containerMax = itemType._containerMax;
			_maxAmount = itemType._maxAmount;		
			_amount = itemType._amount;
			_iconKey = itemType._iconKey;
			_verbActionSequence = itemType._verbActionSequence;

			// Fill items with none items
			EmptyContainer ();

			ReceiveActionSequences ();
		}

		/// <summary>
		/// Receives the verb action sequences from the action controller.
		/// </summary>

		void ReceiveActionSequences() {
		
			//TODO: Fix this

//			Debug.Log ("Receive Action Sequences Invoked");

			for(int i = 0; i < _verbActionSequence.Count; i++) {
			
				// Locate the sequence by key

				// TODO: In particular, fix this so that it searches all keyed objects
				//VerbActionSequence vAS = GameController.SceneManager.FindKeyedObject<VerbActionSequence>(_verbActionSequence[i].Key); 

				// TODO: Temp:
				// Search Persistent Data
				VerbActionSequence vAS = Entity.FindObjectByKey<VerbActionSequence>(_verbActionSequence[i].Key, GameController.PersisentGameData.VerbActionSequences);
				// Search Scene Data
				if(vAS == null && GameData.SceneData != null && GameData.SceneData.VerbActionSequences != null) {
					vAS = Entity.FindObjectByKey<VerbActionSequence>(_verbActionSequence[i].Key, GameData.SceneData.VerbActionSequences);
					}


				// If a sequence is found then set the reference
				if(vAS != null) {
					_verbActionSequence[i] = vAS;
				}
			}

		}

		/// <summary>
		/// Select this instance.
		/// </summary>

		public void Select() {
			GameController.Instance.SelectedEntity = this;
		}


		// FOR CONTAINERS

		/// <summary>
		/// Determines whether this instance is empty.
		/// </summary>
		/// <returns><c>true</c> if this instance is empty; otherwise, <c>false</c>.</returns>

		public bool IsEmpty() {
		
			// TODO: Temp Solution for ECCC
			return this == GameController.InventoryManager.GetEmptyItem () || this.Key == "ITM_NONE";

		}
	
		/// <summary>
		/// Empties the container. Generally used when initializing the container.
		/// </summary>

		void EmptyContainer() {

			for (int i = 0; i < _containerMax; i++) {
				Debug.Log ("Add none item: " + i);
				// Add a none item
				_items.Add(GameController.InventoryManager.GetEmptyItem());
				
			}
		}

		/// <summary>
		/// Open this instance of the container.
		/// </summary>

		public void Open() {
			// Tell the container screen what item it is refering to
			GameController.InventoryManager.SelectedItemContainerScreen.Container = (IContainer)this;
			
			// Open the container screen
			GameController.InventoryManager.SelectedItemContainerScreen.Activate();

			// This item is open
			_isOpen = true;
		}

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
			_items [containerSlot] = item;
			GameController.InventoryManager.SelectedItemContainerScreen.UpdateDisplay ();
			
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
					_items[i] = GameController.InventoryManager.GetEmptyItem();
				}
				
			}
			
			// Update the display
			GameController.InventoryManager.SelectedPlayerContainerScreen.UpdateDisplay ();
		}

		/// <summary>
		/// Adds the item to next empty container slot.
		/// </summary>
		/// <returns><c>true</c>, if item to next empty was added, <c>false</c> otherwise.</returns>
		/// <param name="item">Item.</param>
		
		public bool AddItemToNextEmpty(IItem item) {
			
			// The container slot that the item will be added to
			int slot = 0;
			
			// Search through the container's items
			foreach(Item i in _items) {
				
				// Check to see if the item is empty
				if(i.IsEmpty()) { 	
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

		#endregion

#if UNITY_EDITOR

		/// <summary>
		/// Gets the icon key.
		/// </summary>
		/// <value>The icon key.</value>

		public string IconKey { 
			get { 

				if(_iconKey == null) {
					return "";
				}

				return _iconKey; 
			} 
		}

		/// <summary>
		/// Gets the container max.
		/// </summary>
		/// <value>The container max.</value>

		public int ContainerMax { get { return _containerMax; } }

		[ExecuteInEditMode]

		/// <summary>
		/// Sets the properties.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="name">Name.</param>
		/// <param name="desc">Desc.</param>
		/// <param name="iconKey">Icon key.</param>
		/// <param name="containerMax">Container max.</param>
		/// <param name="maxAmount">Max amount.</param>

		public void SetProperties(string key, string name, string desc, string iconKey, int containerMax, int maxAmount) {

			_key = key;
			
			_name = name;
			
			_description = desc;
			
			_iconKey = iconKey; 
			
			_containerMax = 0; 
			
			_maxAmount = 1;

		}


#endif
	}
}