#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - November 3, 2014
#endregion 

#region summary
/// <summary>
/// InventoryManagement - One instance to manage our inventory syladex
/// </summary>
#endregion

#region using
using System.Xml;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using WhatPumpkin.Screens;
using WhatPumpkin.Data.XML;
#endregion

namespace WhatPumpkin.Entities.Inventory { // TODO: I don't want inventory to derive from the entities namespace

	// TODO: Add this to the game controller

	public class InventoryManagement : MonoBehaviour, IXMLDataReceiver, ICombineItemController {

		#region static fields

		/// <summary>
		/// The active inventory manager.
		/// </summary>
	
		static InventoryManagement _instance; // One Singleton instance of the inventory manager


		#endregion

		#region fields

		/// <summary>
		/// The item icons.
		/// </summary>

		Dictionary<string, Sprite> _itemIcons = new Dictionary<string, Sprite> ();

		/// <summary>
		/// The icon groups representing the icons which can be serialized and exposed to the inspector unlike dictionaries.
		/// </summary>

		[SerializeField] IconGroup [] _iconGroups;

		//[SerializeField] List<Sprite> _itemIcons = new List<Sprite> ();

		/// <summary>
		/// The collection of possible items in the game.
		/// </summary>

		// Items will be created instantiated from this item type array
		[SerializeField] Item [] _itemTypes;

		/// <summary>
		/// The button that will be used for the syladex slots
		/// </summary>

		[SerializeField] GameObject _slotButton;  

		/// <summary>
		/// The item description text.
		/// </summary>

		[SerializeField] Text _itemDescriptionText;

		/// <summary>
		/// The default combine item sequence.
		/// </summary>

		[SerializeField] string _defaultCombineItemSequence = "AS_COMBINE_DEFAULT";

		/// <summary>
		/// The selected player container.
		/// </summary>

		[SerializeField] ContainerScreen _selectedPlayerContainerScreen;

		/// <summary>
		/// The selected item container.
		/// </summary>

		[SerializeField] ContainerScreen _selectedItemContainerScreen; 


		/// <summary>
		/// The earth inventory slot.
		/// </summary>

		[SerializeField] InventorySlot _earthInventorySlot;
	

		#endregion

		#region Combine Controller


		public event Combine Combine;

		public bool IsUseMode { get { return _combiningItemKey != null && _combiningItemKey != ""; } }
		
		// The hotspot controller may also handle using/combining items
		string _combiningItemKey = "";
		
		
		/// <summary>
		/// Starts the combine item mode.
		/// </summary>
		/// <param name="itemKey">Item key.</param>
		
		public void StartCombineMode(string itemKey) {
			_combiningItemKey = itemKey;

			// Change the default cursors
			GameController.CursorControl.ChangeDefaultCursor (InputManager.TARGET_CURSOR, InputManager.TARGET_ROLLOVER_CURSOR);
			GameController.CursorControl.UseDefaultCursor ();


		}
		
		/// <summary>
		/// Ends the combine mode.
		/// </summary>
		
		public void EndCombineMode() {
			_combiningItemKey = "";
			GameController.CursorControl.ChangeDefaultCursor (InputManager.DEFAULT_CURSOR, InputManager.DEFAULT_ROLLOVER_CURSOR);
			GameController.CursorControl.UseDefaultCursor ();
		}


		
		void AttemptCombine(string itemKey1, string itemKey2, string defaultActionSequence) {

			foreach (WhatPumpkin.Actions.Sequences.CombineActionSequence combineActionSequence in GameData.SceneData.CombineActionSequences) {
				
				if(combineActionSequence.HandleCombine(itemKey1, itemKey2)) {

					EndCombineMode ();
					return;
				}
				
			}

			foreach (WhatPumpkin.Actions.Sequences.CombineActionSequence combineActionSequence in GameData.PersistentData.CombineActionSequences) {
				
				if(combineActionSequence.HandleCombine(itemKey1, itemKey2)) {
					EndCombineMode ();
					return;
				}
				
			}

			// Could not find any combination, using default sequence
			try {
				GameController.SceneManager.FindKeyedObject<IPerform>(defaultActionSequence).Play();
			}
			catch {
				Debug.Log ("Unable to perform the default action sequence '" + defaultActionSequence + "'. Please check to see if one exists in the persistent data.");
			}


			// When a combine is attempted we end combine mode
			EndCombineMode ();

			
		}

		
		void HandleSendItem (string itemKey)
		{


			// Try to combine the items
			if (IsUseMode) {AttemptCombine (_combiningItemKey, itemKey, _defaultCombineItemSequence);}
		}



		void InitializeCombineItemControl() {


			//Debug.Log ("Init Combine Item Control Count: " + GameData.PersistentData.CombineActionSequences.Length);

			// Search through all of the combine action sequences and assign them to this controller
			/*
			foreach (WhatPumpkin.Actions.Sequences.CombineActionSequence combineActionSequence in WhatPumpkin.Actions.Sequences.CombineActionSequence.Instances) {
			
				combineActionSequence.SubscribeToController(this);
			
			}*/

			// TODO: Temp?
			// Search through all of the combine action sequences and assign them to this controller
			/*
			foreach (WhatPumpkin.Actions.Sequences.CombineActionSequence combineActionSequence in GameData.PersistentData.CombineActionSequences) {
				
				combineActionSequence.SubscribeToController(this);
				
			}

			foreach (WhatPumpkin.Actions.Sequences.CombineActionSequence combineActionSequence in GameData.SceneData.CombineActionSequences) {
				
				combineActionSequence.SubscribeToController(this);
				
			}*/
			
		}

		public void AddToCombineList(ICombineable combineable) {

			combineable.SendItem += HandleSendItem;


		}

		public void RemoveFromCombineList(ICombineable combineable) {

			combineable.SendItem -= HandleSendItem;
		
		}

		#endregion


		#region properties
		/// <summary>
		/// Gets the active inventory manager.
		/// </summary>
		/// <value>The active inventory manager.</value>
		
		public static InventoryManagement Instance { get { return _instance; } }

		// Instance Properites

		/// <summary>
		/// Gets the earth inventory slot.
		/// </summary>
		/// <value>The earth inventory slot.</value>

		public InventorySlot EarthInventorySlot { get { return _earthInventorySlot; } }

		/// <summary>
		/// Gets the selected player container.
		/// </summary>
		/// <value>The selected player container.</value>

		public ContainerScreen SelectedPlayerContainerScreen { get { return 

				_instance._selectedPlayerContainerScreen; } }

		/// <summary>
		/// Gets the selected item container.
		/// </summary>
		/// <value>The selected item container.</value>

		public ContainerScreen SelectedItemContainerScreen { get { return _instance._selectedItemContainerScreen; } }

		/// <summary>
		/// Swaps the items.
		/// </summary>
		/// <returns><c>true</c>, if items were swaped, <c>false</c> otherwise.</returns>
		/// <param name="slotA">Slot a.</param>
		/// <param name="slotB">Slot b.</param>

		public bool SwapItems(InventorySlot slotA, InventorySlot slotB) {

			// This is to make sure we have an item referencing the B slot item even after we change the item the B slot is referencing
			IItem itemB = slotB.Item;

			// Recipient Slot - Send Item A to the Recipient Slot (Slot B)
			slotB.Container.AddItem(slotA.Item, slotB.ItemContainerPosition);

			// Giver slot - Send Item B to the Giver Slot (Slot A)
			slotA.Container.AddItem(itemB, slotA.ItemContainerPosition);

			// Update the displays
			UpdateContainerDisplays ();

			return true;
		}

		/// <summary>
		/// Updates the container displays.
		/// </summary>

		public void UpdateContainerDisplays() {

			_selectedItemContainerScreen.UpdateDisplay ();
			_selectedPlayerContainerScreen.UpdateDisplay ();
		}

		/// <summary>
		/// Gets the icon by a given key.
		/// </summary>
		/// <returns>The icon.</returns>
		/// <param name="key">Key.</param>

		public Sprite GetIcon(string key) {
		
			Sprite icon = null;
			if (key != null) {
						_itemIcons.TryGetValue (key, out icon);
				}
			return icon;

		}

		#endregion

		#region methods

		// Use this for initialization
		void Start () {

			// Update dictionary
			foreach (IconGroup iconGroup in _iconGroups) {
			
				_itemIcons.Add(iconGroup.Key, iconGroup.Icon);

			}

			// Load Persistent data
			//XMLLoader.Load<InventoryManagement> (Application.dataPath + "/Resources/Game Data/Items.xml", GameController.InventoryManager);

			InitializeCombineItemControl ();

			// Subscribe to input events
			GameController.InputManager.LeftMouseClick += HandleLeftMouseClick;
			GameController.InputManager.RightMouseClick += HandleRightMouseClick;
			
		}

		void HandleRightMouseClick ()
		{
			EndCombineMode ();
		}

		void HandleLeftMouseClick ()
		{
			// If we're in combine mode then we end it the moment there is a click
			// TODO: This may prove to be more of a pain than anticipated
			//EndCombineMode ();
		}

		void Awake() {
			// Set the singleton to this
			_instance = this;

		}


		/// <summary>
		/// Updates the item description text.
		/// </summary>
		/// <param name="iSlot">I slot.</param>

		public void UpdateItemDescriptionText(InventorySlot iSlot) {
		
			// TODO:
			// Temporary
			_itemDescriptionText.text = "Item Description: " + iSlot.ItemDescription;
		
		}

		/// <summary>
		/// Updates the item description text.
		/// </summary>
		/// <param name="desc">Desc.</param>

		public void UpdateItemDescriptionText(string desc) {
			
			// TODO:
			// Temporary
			_itemDescriptionText.text = desc;
			
		}

		/// <summary>
		/// Gets the empty item. 
		/// The empty item will be the first item in the item types list.
		/// </summary>
		/// <returns>The empty item.</returns>

		public Item GetEmptyItem() {
		
			if (_itemTypes.Length > 0) {
				return _itemTypes[0];
			}
		
			return null;
		}


		/// <summary>
		/// Creates and returns an item based on a given item type.
		/// </summary>
		/// <returns>The item.</returns>
		/// <param name="itemType">Item type.</param>
		
		public Item CreateItem(string itemKey) {
			
			return new Item (FindItemTypeByKey(itemKey));
			
		}

		/// <summary>
		/// Finds the item by key.
		/// </summary>
		/// <returns>The item of a given key.</returns>
		/// <param name="itemKey">Item key.</param>

		public Item FindItemTypeByKey(string itemKey) {
		
			foreach (Item item in _itemTypes) {

				if(item.Key == itemKey) {
					return item;
				}
				
			}

			return null;
		}

		/// <summary>
		/// Finds the item by identifier.
		/// </summary>
		/// <returns>The item of a given identifier.</returns>
		/// <param name="id">Identifier.</param>

		public Item FindItemTypeById (int id) {
		
			foreach (Item item in _itemTypes) {
				
				if(item.Id == id) {
					return item;
				}
				
			}
			return null;
		}

		/// <summary>
		/// Parses the XML data.
		/// </summary>
		/// <returns><c>true</c>, if XML data was parsed, <c>false</c> otherwise.</returns>
		/// <param name="xmlData">Xml data.</param>
		
		public bool ParseXMLData(XmlDocument xmlData, params object [] parameters) {

			// Get the parent node
			XmlNode parentNode = xmlData.ChildNodes [0];		

			// Parse the verb action sequence data
			GoogleDocToXMLFormatParser.Parse<Item>(out _itemTypes, parentNode);

			return true;
		}

		/// <summary>
		/// Gets the icon of a given key.
		/// </summary>
		/// <returns>The icon.</returns>
		/// <param name="key">Key.</param>
		/*
		public Sprite GetIcon(string key) {

			// TODO: Tempoarary
			return _itemIcons [0];

			return null;
			
		}*/

		#endregion

#if UNITY_EDITOR

		/// <summary>
		/// Gets the item types.
		/// </summary>
		/// <value>The item types.</value>

		public Item [] ItemTypes { get { return _itemTypes; } }

		/// <summary>
		/// Adds an item type
		/// </summary>

		public void AddItemType() {

			_itemTypes = DataUtilities.AddArrayElement(_itemTypes, new Item());
		}

		public void RemoveItem(Item item) {
		
			_itemTypes = DataUtilities.RemoveArrayElement (_itemTypes, item);

		
		}

		public void AddIcon() {
		


		}




#endif
	}
}