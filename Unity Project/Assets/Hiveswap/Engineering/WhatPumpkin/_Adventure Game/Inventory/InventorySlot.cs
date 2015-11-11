#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - November 3, 2014
#endregion 

#region using
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using WhatPumpkin.Actions;
using WhatPumpkin.Screens;
#endregion


namespace WhatPumpkin.Entities.Inventory {

	// Require Components
	[RequireComponent(typeof(Button))]

	/// <summary>
	/// Inventory slot.
	/// </summary>

	public class InventorySlot : MonoBehaviour, ICombineable {


		#region static fields

		/// <summary>
		/// The selected slot.
		/// </summary>

		static InventorySlot _selectedSlot;

		#endregion

		#region events
		
		public event ItemKeyEvent SendItem;
		
		#endregion

		#region member fields

		/// <summary>
		/// Th identifier.
		/// </summary>
		[SerializeField] int _id;

		/// <summary>
		/// The container screen that this slot is a part of.
		/// </summary>
		[SerializeField] ContainerScreen _containerScreen;

		/// <summary>
		/// The color of the slot when selected.
		/// </summary>

		[SerializeField] Color _selectedColor;

		/// <summary>
		/// The inventory that this slot is pointing to .
		/// </summary>

		IItem _item; 

		/// <summary>
		/// The button text.
		/// Provide easy access to the button's text
		/// </summary>

		[SerializeField] Text _buttonText; 

		/// <summary>
		/// The _item amount.
		/// </summary>

		[SerializeField] Text _itemAmount;

		/// <summary>
		/// The icon.
		/// </summary>

		[SerializeField] Image _icon;

		/// <summary>
		/// The add width offset.
		/// </summary>

		[SerializeField] bool _addWidthOffset = true;

	
		/// <summary>
		/// The original position.
		/// </summary>

		Vector3 _origPosition;

		/// <summary>
		/// The collided inventory slot.
		/// </summary>

		InventorySlot _collidedInventorySlot;

		/// <summary>
		/// is this object dragging.
		/// </summary>

		bool _isDragging = false;

		#endregion


		#region member properties

		/// <summary>
		/// Gets the selected slot.
		/// </summary>
		/// <value>The selected slot.</value>

		public static InventorySlot SelectedSlot { get { return _selectedSlot; } }

		/// <summary>
		/// Gets the referenced item.
		/// </summary>
		/// <value>The referenced item.</value>

		public IItem ReferencedItem { get { return _item; } }

		// Instance Properties

		// TODO: Add description to the inventory data and get that data

		/// <summary>
		/// Gets the item description.
		/// </summary>
		/// <value>The item description.</value>

		public string ItemDescription {

						get { 

							// Check to see if there is an inventory item, if so return the item's description
							if(_item != null)
							{
								return _item.Description;
							}

							// Otherwise, return a default message
							return "You shouldn't see this! What did you do?!?!";
						}
				} 

		/// <summary>
		/// Gets a value indicating whether this instance has item actions.
		/// </summary>
		/// <value><c>true</c> if this instance has item actions; otherwise, <c>false</c>.</value>

		public bool HasItemActions {
		
			get { return _item.VerbSequences.Count > 0;} 

		}

		/// <summary>
		/// Gets the container that this slot is currently referencing.
		/// </summary>
		/// <value>The container.</value>

		public IContainer Container { get { return _containerScreen.Container; } }


		/// <summary>
		/// Gets the item this slot is currently referencing.
		/// </summary>
		/// <value>The item.</value>

		public IItem Item { get { return _item; } }

		/// <summary>
		/// Gets the item container position.
		/// </summary>
		/// <value>The item container position.</value>

		public int ItemContainerPosition {get { return _containerScreen.SlotOffset + _id;}}

		#endregion

		#region methods

		/// <summary>
		/// Clears the selection.
		/// </summary>

		static void ClearSelection() {
		
			_selectedSlot.Deselect (); 

		}



		// Methods

		/// <summary>
		/// Opens the inventory container.
		/// </summary>
		/// <returns><c>true</c>, if inventory container was opened, <c>false</c> otherwise.</returns>

		public void OpenInventoryContainer() {

			if (GameController.CombineItemController.IsUseMode) {
				if(SendItem != null){SendItem(Item.Key);}
			}
			else if  (!_isDragging) {
				// If there's a verb coin panel then open it
				SelectInventorySlot ();
			}

		}


		/// <summary>
		/// Invoked when the drag ends.
		/// </summary>

		public void EndDrag() {


			// Let the object know that we are no longer dragging
			_isDragging = false;

			// Set the object back to it's original position
			ReturnToOrigin ();

			// Transfer contents from one slot to another if necessary
			if (_collidedInventorySlot != null) {
				// Swap the items in the referenced inventory slots
				InventoryManagement.Instance.SwapItems(this, _collidedInventorySlot);

			}

		}

		/// <summary>
		/// Returns this object to it's origin.
		/// </summary>

		private void ReturnToOrigin() {
			this.transform.position = _origPosition;
		
		}

		/// <summary>
		/// Activate this instance.
		/// </summary>

		public void Activate() {
		//	Debug.Log ("Inventory Slot: " + this);
		//	Debug.Log ("Inventory Slot: " + this.name);
			this.gameObject.SetActive (true);
		}

		/// <summary>
		/// Deactivate this instance.
		/// </summary>

		public void Deactivate() {
			this.gameObject.SetActive (false);
		}

		/// <summary>
		/// Shows the inventory info of a particular item.
		/// </summary>
		/// <param name="inv">Inv.</param>

		public void ShowInventoryInfo(IItem item) {

//			Debug.Log ("Show Inventory Info");

			// Add the new inventory
			_item = item;
			// Update the text data 
			/*
			Debug.Log (_buttonText);
			Debug.Log (item.Key);
			Debug.Log (_buttonText.text);
			*/

			// Display the correct name
			if(_buttonText){_buttonText.text =  MessageManager.Instance.GetMessage(item.Name);}

			// Display the correct item amount
			if (_itemAmount) {


				if(item.Amount <= 0) { // Prevent the item amount from displaying a number less than 0
					_itemAmount.text = "";
				}

				else { // Otherwise display the correct item amount 

					// TODO: ECCC Temp build fix
					//_itemAmount.text = item.Amount.ToString ();
					_itemAmount.text = "";
				}
			}

			// Display the correct icon
			if (_icon != null && item.Icon != null) {_icon.sprite = item.Icon;}  
		}

		/// <summary>
		/// Determines whether this instance is empty.
		/// </summary>
		/// <returns><c>true</c> if this instance is empty; otherwise, <c>false</c>.</returns>

		public bool IsEmpty() {return _item == null || _item.IsEmpty();}


		// Use this for initialization
		void Awake () {

			// Keep track of this object's original position
			_origPosition = new Vector3 (this.transform.position.x, 
			                            this.transform.position.y);

		}

		/// <summary>
		/// Selects the inventory slot.
		/// </summary>

		void SelectInventorySlot() {

			Debug.Log ("Select Inventory Slot");
		
			// Check to see if this object has actions
			if(HasItemActions){

				Debug.Log ("Has Item Actions");

				// First, deselect the currently selected slot if it's not null
				if(_selectedSlot != null) {
					_selectedSlot.Deselect ();
				}

				// Let the game know that this is the active inventory slot
				_selectedSlot = this;

				// Let the item know that it's selected
				this.Item.Select();

				// Set the button color 
				Button button = this.GetComponent<Button> ();
				button.image.color = _selectedColor;

				// The old verb coin offset
				Vector3 oldVerbCoinOffset = new Vector3(ActionController.Instance.VerbCoinPanel.VerbCoinOffset.x,
				                                        ActionController.Instance.VerbCoinPanel.VerbCoinOffset.y,
				                                        ActionController.Instance.VerbCoinPanel.VerbCoinOffset.z);
				// Add any offeset to the verb coin
				if(_addWidthOffset) {
					/*
					ActionController.Instance.VerbCoinPanel.VerbCoinOffset = new Vector3 (oldVerbCoinOffset.x + this.GetComponent<RectTransform>().rect.width,
					                                                                      oldVerbCoinOffset.y,
					                                                                      oldVerbCoinOffset.z);*/

					ActionController.Instance.VerbCoinPanel.VerbCoinOffset = new Vector3 (92,
					                                                                      oldVerbCoinOffset.y,
					                                                                      oldVerbCoinOffset.z);
				}

				Debug.Log ("Open Panel");
				// Open the verb coin panel using the verb sequences associated with this item
				ActionController.Instance.OpenVerbCoinPanel(_item.VerbSequences, this.transform);
				// Return the offset
				ActionController.Instance.VerbCoinPanel.VerbCoinOffset = new Vector3(oldVerbCoinOffset.x,
				                                                                     oldVerbCoinOffset.y,
				                                                                     oldVerbCoinOffset.z);

			}

		}

		/// <summary>
		/// Deselect this instance.
		/// </summary>

		void Deselect() {
		
			// Return this object's color to normal
			Button button = this.GetComponent<Button> ();
			button.image.color = Color.white;

			// Set the static reference to null
			_selectedSlot = null;


		
		}

		/// <summary>
		/// Begins the drag.
		/// </summary>

		public void BeginDrag() {
			// Let the object know that it's being dragged 
			// Only do so if the referenced item is not open item referenced in the inventory slot is not "none" (empty)
			IContainer container = (IContainer)_item;
			if(!_item.IsEmpty() && !container.IsOpen){
				_isDragging = true;
			}
		}

		// Update is called once per frame
		void Update () {

			// If this object is being dragged the let it follow the mouse pointer
			if (_isDragging) {
				FollowMousePointer();
			}
		}

		/// <summary>
		/// Follows the mouse pointer.
		/// </summary>

		void FollowMousePointer() {
			this.transform.position = Input.mousePosition;
		}

		/// <summary>
		/// Raises the trigger enter2d event.
		/// </summary>
		/// <param name="col">Col.</param>

		void OnTriggerEnter2D(Collider2D col) {
		
			// Upon entry, track any inventory slots that have been entered
			// Check to see if the object is colliding with an inventory slot
			InventorySlot iSlot = col.GetComponent<InventorySlot> ();

			if (iSlot != null) {
				// Track the inventory slot
				_collidedInventorySlot = iSlot;
			}


		}

		/// <summary>
		/// Raises the trigger exit2 d event.
		/// </summary>
		/// <param name="col">Col.</param>

		void OnTriggerExit2D(Collider2D col) {

			//Debug.Log ("Trigger Exit");

			//  Remove an inventory slot that it is tracking if we exited this inventory slot
			InventorySlot iSlot;
			iSlot = col.GetComponent<InventorySlot>();

			// Make sure the islot is not null and that it is the same as the one currently being tracked
			if(iSlot != null && iSlot == _collidedInventorySlot)
			{
				// If so, then clear the collided slot
				_collidedInventorySlot = null;
			}

		}

		/// <summary>
		/// Gets the icon.
		/// </summary>
		/// <returns>The icon.</returns>
		/// <param name="key">Key.</param>

		Sprite GetIcon(string key) {
		
			return null;

		}

		/// <summary>
		/// Raises the destroy event.
		/// </summary>
		/*
		void OnDestroy() {

			GameController.CombineItemController.RemoveFromCombineList (this);
			SendItem = null;
			
		}*/

		void Start() {
			DontDestroyOnLoad (this);
			GameController.CombineItemController.AddToCombineList (this);

		}


		#endregion

	}

}
