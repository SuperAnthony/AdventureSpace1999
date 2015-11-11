#region Copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - October, 2014
#endregion


#region using
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using WhatPumpkin.Entities.Inventory;
using WhatPumpkin.CameraManagement;
#endregion

namespace WhatPumpkin.Sgrid.Characters {

	[RequireComponent(typeof(PlayerInventory))]

	/// <summary>
	/// PlayerCharacter
	/// </summary>

	public class PlayerCharacter : Character, ICharacterSaveData_Act1, ISwitchable, IMoveable, ITriggersCamSwitch, IKeyed, IPartyMember {


		// TODO: IPlayerCharacter, ICharacter, INonPlayerCharacter

		#region static events

		/// <summary>
		/// Occurs when PC created.
		/// </summary>

		static public event EventHandler PCCreated; // TODO: Remove? This should have all been one PC event

		/// <summary>
		/// Occurs when a PC is activated.
		/// </summary>

		static public event EventHandler PCActivated;

		/// <summary>
		/// Occurs when activated.
		/// </summary>
		
		public override event EventHandler Activated;


		#endregion

		#region fields

		/// <summary>
		/// The active character.
		/// </summary>

		static PlayerCharacter _active; 

		/// <summary>
		/// The color associated with this character, will generally be used for UI.
		/// </summary>

		[SerializeField] Color _color;
	
		/// <summary>
		/// The the abilities this player has.
		/// </summary>

		//[SerializeField] List<string> _abilities = new List<string>();

		/// <summary>
		/// The unity scene that the player is in.
		/// </summary>

		[SerializeField] string _scene; 

		/// <summary>
		/// The latest camera node since active.
		/// </summary>

		[SerializeField] string _lastCamSinceActive;

		/// <summary>
		/// The room key that the character is in.
		/// </summary>

		[SerializeField] string _room;


		#endregion

		#region static properties

		/// <summary>
		/// Gets or sets the active player character.
		/// </summary>
		/// <value>The active.</value>


		static public PlayerCharacter Active { 
			get { return _active; } 
			private set {


				if(_active != null)
				{
					// Deactivate the currently active instance
					_active.Deactivate();

				}

				// Set the new active character
				_active = value;
			
		
			

			}
		} 
	
	
		/// <summary>
		/// Gets the items.
		/// </summary>
		/// <value>The items.</value>

		public List<IItem> Items { 
		
			get { return Inventory.Items;}

		} 

		#endregion

		#region member properties

		/// <summary>
		/// Gets the current speed.
		/// </summary>
		/// <value>The current speed.</value>

		public override float CurrentSpeed { 
			get { 

				// TODO: Temp - this does not determine the actual speed
				AIPath aiPath = this.GetComponent<AIPath>();
				if(aiPath != null) {

					// Add the value of all three axis to get the current velocity of the character
					// Make sure to use an absolute value otherwise the game may return a negative speed
					// TODO: Triangulate to get an accurate exact speed
					return Mathf.Abs (aiPath.CurrentVelocity.x +
									aiPath.CurrentVelocity.y +
					                  aiPath.CurrentVelocity.z);
				}

				return 0F; 
			} 
		}

		/// <summary>
		/// Gets a value indicating whether this instance is active.
		/// </summary>
		/// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>

		public bool IsActive { get { return this == _active; } }

		/// <summary>
		/// Gets the color.
		/// </summary>
		/// <value>The color.</value>

		public Color Color { get { return _color; } }

		/// <summary>
		/// Gets a value indicating whether this instance can trigger switch.
		/// </summary>
		/// <value>true</value>
		/// <c>false</c>

		public bool CanTriggerSwitch { get { return IsActive; } }

		/// <summary>
		/// Gets the scene name the player is in.
		/// </summary>
		/// <value>The room.</value>

		public string Scene { get { return _scene; } }

		/// <summary>
		/// Gets the inventory.
		/// </summary>
		/// <value>The inventory.</value>

		public PlayerInventory Inventory {
				get {
					// Get the player inventory component
					PlayerInventory inventory = this.GetComponent<PlayerInventory>();
					// If it doesn't exist then add it
					if(inventory == null){inventory = this.gameObject.AddComponent<PlayerInventory>();}
					// Return
					return inventory;
				}
		}

		/// <summary>
		/// Gets the last cam since active.
		/// </summary>
		/// <value>The last cam since active.</value>

		public string LastCamSinceActive { get { return _lastCamSinceActive; } }

		/// <summary>
		/// Gets the serializable transform data.
		/// </summary>
		/// <value>The transform data.</value>

		SerializableTransform _transformData = new SerializableTransform();
		public SerializableTransform TransformData { 
			get { 
				_transformData = new SerializableTransform(this.transform);
				return _transformData; 
			} 
		}

	

		#endregion
	
		#region methods

		/// <summary>
		/// On Update.
		/// </summary>

		protected override void Update() {

			base.Update ();


			// For some reason this  is necessary TODO: Ideally I wouldn't have to do this each frame

			if (this != _active && this.GetComponent<CharacterController> ().enabled == true) {
								this.GetComponent<CharacterController> ().enabled = false;
						}

		}


		/// <summary>
		/// Adds the ability.
		/// </summary>
		/// <param name="abilityKey">Ability key.</param>
		/*
		public void AddAbility(string abilityKey) {
			_abilities.Add (abilityKey);
		}*/

		/// <summary>
		/// Determines whether this instance has ability the specified abilityKey.
		/// </summary>
		/// <returns><c>true</c> if this instance has ability the specified abilityKey; otherwise, <c>false</c>.</returns>
		/// <param name="abilityKey">Ability key.</param>

		public bool HasAbility(string abilityKey) {
		
			// TODO: I'm not a big fan of this living in Player Character, could use something more abstract


			// Check to see if the character has the component Character Abilities, if it does not then return false
			WhatPumpkin.Hiveswap.Abilities.CharacterAbilities characterAbility = this.GetComponent<WhatPumpkin.Hiveswap.Abilities.CharacterAbilities>();
			if (characterAbility == null) {
				return false;
			}


			return characterAbility.HasAbility (abilityKey);

		}




		/// <summary>
		/// Start this instance.
		/// </summary>

		protected override void Start() {

			// Add PC to list of characters in the game
			GameController.PartyManager.AddPCToList (this);

			// Set the player's target to the game controllers move target
			SetTarget (GameController.Instance.MoveTarget);

			// Invoke the base start method 
			base.Start ();

			// This instance was created
			// TODO: Do I need this?
			OnCreated ();

			DisablePlayerControl ();
	

			// Track Camera Node Switches
			GameController.CamManager.SwitchCameraNode+= HandleSwitchCameraNode;

			// Handle Room Actiation
			WhatPumpkin.Sgrid.Environment.Room.RoomActivated += HandleRoomActivated;

			GameController.InputManager.TargetChangeEvent += HandleTargetChange;
		
		}

		/// <summary>
		/// Handles a target change
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>

		void HandleTargetChange (object sender, TargetEventArgs e)
		{
			// I prefered to do this in the party manager but for some reason it's not working and I don't have time to fight with unity
			if (e.EventType == TargetEventType.Moved && _active == this) {
						AllowMovement ();
				}
		}

		/// <summary>
		/// Handles room activation
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>

		void HandleRoomActivated (object sender, EventArgs e)
		{
			if (this == _active) {

				WhatPumpkin.Sgrid.Environment.Room room = (WhatPumpkin.Sgrid.Environment.Room)sender;

				if(room != null) {
					_room = room.Key;
				}
			
			}
		}

		/// <summary>
		/// Handles camera node switch
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>

		void HandleSwitchCameraNode (object sender, CamSwitchArgs e)
		{

			// If this character is active then track the latest camera
			if (IsActive) {
				_lastCamSinceActive = e.NewActiveCamera.Key;
			}
		}

		void OnDestroy() {
		
		
			// Unregister from events
			GameController.CamManager.SwitchCameraNode-= HandleSwitchCameraNode;
			WhatPumpkin.Sgrid.Environment.Room.RoomActivated -= HandleRoomActivated;
			GameController.InputManager.TargetChangeEvent -= HandleTargetChange;

		}

		static public void UpdateActiveCharacterScene() {
		
			if (_active != null) {
								_active.UpdateScene ();
						}
		
		}

		void UpdateScene() {

			_scene = Application.loadedLevelName;
		
		}

		/// <summary>
		/// Broadcasts the created PC.
		/// </summary>

		void OnCreated() {
		
			if(PCCreated != null){PCCreated.Invoke(this, null);}

		}

		

		/// <summary>
		/// Deactivate this instance.
		/// </summary>
		
		public override void Deactivate() {

			if (_active = this) {
								_active = null;
						}

//			Debug.Log ("Attempt To Deactivate Character Controller");

			// Disable the Character Controller so that the PC cannot fall infinitely 
			CharacterController charCont = this.GetComponent<CharacterController> ();
			if(charCont != null){
				charCont.enabled = false;
			}
			else {
				Debug.LogError("The character controller on your Player Character " + this.Key + " is not attached. Please attach");
			}

			// Hide the instance
			// Hide ();
			DisablePlayerControl ();
		}


		void CompleteActivationAfterSceneLoad (object sender, EventArgs e)
		{
			Debug.Log ("Complete Scene Activation After Scene Load");
			GameController.SceneManager.SceneLoadEnd -= CompleteActivationAfterSceneLoad;
			Activate ();
		}
		
		/// <summary>
		/// Activate this instance.
		/// </summary>

		public override void Activate() {

			// Is this in the correct unity scene or do we need to load a new one
			// Wait for scene to load before continuing
			if (!IsInCurrentScene ()) {
//				Debug.Log ("Subscribe to scene load end");

				// Deactivate the currently active character for now
				Deactivate();

				GameController.SceneManager.SceneLoadEnd += CompleteActivationAfterSceneLoad;
				GameController.SceneManager.LoadScene(_scene);
				return;
			}

			// Set the active instance
			Active = this;
			
			// If this object is activated then for now I will say that it's implied that it is also an available party member
			_isAvailablePartyMember = true;

			Container container = _active.GetComponent<Container>();
			

			GameController.InventoryManager.SelectedPlayerContainerScreen.SwitchCointainer(container);
			//Debug.Log ("container: " + container);
			container.UpdateContainerDisplay();




			// Update the controls
			if (TargetMover.Instance != null) {
				// Re set the move target 
				TargetMover.Instance.SetTargetPosition (this.gameObject.transform.position);
			}

			// Enable the character controller
			// Disable the Character Controller so that the PC cannot fall infinitely 

			CharacterController charCont = this.GetComponent<CharacterController> ();
			if(charCont != null){
				charCont.enabled = true;
			}
			else {
				Debug.LogError("The character controller on your Player Character " + this.Key + " is not attached. Please attach");
			}
			
			EnablePlayerControl ();

			// Activate room
			if (_room != null && _room != "") {

				//	GameController.SceneManager.FindEntity (_room).Activate ();

				IActivatable activatable = GameController.SceneManager.FindKeyedObject<IActivatable>(_room);

				if(activatable != null) { activatable.Activate();}

			}

			// Activate new camera if necessary

//			Debug.Log ("Activate PC: " + this.Key);

			if (_lastCamSinceActive != null && _lastCamSinceActive != "" 
			    && (GameController.CamManager.ActiveCameraNode == null || _lastCamSinceActive != GameController.CamManager.ActiveCameraNode.Key)) {

	//			Debug.Log ("Attempt To Activate Camera Node");

				CameraNode camNode = GameController.SceneManager.FindKeyedObject<CameraNode>(_lastCamSinceActive);

				if(camNode != null) {

		//			Debug.Log ("Activating Camera Node");
					// Camera node found, activate it
					camNode.Activate();
				}

			}

			// Raise PCActivated
			if (PCActivated != null) {PCActivated (this, null);} // TODO: Do I need this and "Activated"?

			// Raise Activated Event
			if(Activated != null) { Activated(this, null);}


		}

		/// <summary>
		/// Allow Player Movement
		/// </summary>

		void AllowMovement() {

			this.GetComponent<AIPath> ().speed = _speed;
		}

		/// <summary>
		/// Stops moving.
		/// </summary>
		
		public override void StopMoving() {

			// Update the controls
			if (TargetMover.Instance != null) {
				// Re set the move target 
				TargetMover.Instance.SetTargetPosition (this.gameObject.transform.position);
			}

			this.GetComponent<AIPath> ().speed = 0;

			
		}

	

		void EnablePlayerControl() {

			// Reenable Path Finding
			AIPath aiPath = this.GetComponent<AIPath> ();
			aiPath.Enable();
		
		}

		void DisablePlayerControl() {

				AIPath aiPath = this.GetComponent<AIPath> ();
				aiPath.Disable();
		
		}

		/// <summary>
		/// Determines whether this instance is in current scene.
		/// </summary>
		/// <returns><c>true</c> if this instance is in current scene; otherwise, <c>false</c>.</returns>

		bool IsInCurrentScene() {

			// If blank then we will say that the PC is in the current scene
			if (_scene == "" || _scene == null) {return true;}

			// Otherwise, check to see that hte PC is in the current scene
			return _scene == Application.loadedLevelName;
		}

		/// <summary>
		/// Changes the scene.
		/// </summary>
		/// <param name="sceneName">Scene name.</param>

		public void ChangeScene(string sceneName) {
			_scene = sceneName;
		}

	
		/// <summary>
		/// Hides this instance by deactivating the game object.
		/// </summary>
		
		void Hide() {
			
			this.gameObject.SetActive (false);
			
		}


		/// <summary>
		/// Shows this instance by activating the game object.
		/// </summary>

		void Show() {

			this.gameObject.SetActive(true);
		}

		/// <summary>
		/// Determines whether this instance is moving.
		/// </summary>
		/// <returns><c>true</c> if this instance is moving; otherwise, <c>false</c>.</returns>

		public bool IsMoving() {
			return CurrentSpeed > 0F;
		}

		#endregion

		#region implement IPartyMember

		/// <summary>
		/// Occurs when joined party.
		/// </summary>

		public event Action JoinedParty; 

		/// <summary>
		/// Occurs when made available to party.
		/// </summary>

		public event Action MadeAvailableToParty;

		/// <summary>
		/// Occurs when made unavailable from party.
		/// </summary>

		public event Action MadeUnavailableFromParty;


		/// <summary>
		/// Is this character an available party member.
		/// </summary>
		
		[SerializeField] bool _isAvailablePartyMember = false;

		/// <summary>
		/// Gets a value indicating whether this instance is available party member.
		/// </summary>
		/// <value><c>true</c> if this instance is available party member; otherwise, <c>false</c>.</value>

		public bool IsAvailablePartyMember { get { return _isAvailablePartyMember; } }


		/// <summary>
		/// Joins the party.
		/// </summary>
		
		public void JoinParty() {

			// Raise the joined party event
			if (JoinedParty != null) {JoinedParty();}

			MakeAvailable ();
		}

		/// <summary>
		/// Makes this character available to the party.
		/// </summary>

		public void MakeAvailable() {

			_isAvailablePartyMember = true;
			// Invoke the made available to party event
			if (MadeAvailableToParty != null) {MadeAvailableToParty();}
		}

		/// <summary>
		/// Make ths character unavailable.
		/// </summary>

		public void MakeUnavailable() {
			_isAvailablePartyMember = false;
			// Invoke the made unavailable from party event
			if (MadeUnavailableFromParty != null) {MadeUnavailableFromParty();}
		}


		#endregion

		#region for loading data
	
		/// <summary>
		/// Receives the character data from loading a file then parses that data
		/// </summary>
		/// <param name="data">Data.</param>

		public void ReceiveData(ICharacterSaveData_Act1 data) {
			
			// Send that data back to the character

			// _name = data.Name;

			// Receive Scene Data
			_scene = data.Scene;
			// Receive Cam Data
			_lastCamSinceActive = data.LastCamSinceActive;
			// Receive serializable transform data
			_transformData = data.TransformData;

			// After getting the transform data, set the actual transform
			transform.position = new Vector3 (_transformData.Position.X, _transformData.Position.Y, _transformData.Position.Z);
			transform.localScale = new Vector3 (_transformData.LocalScale.X, _transformData.LocalScale.Y, _transformData.LocalScale.Z);
			transform.rotation = new Quaternion (_transformData.Rotation.X, _transformData.Rotation.Y, _transformData.Rotation.Z, _transformData.Rotation.W);


			// Retreive Inventory Data from the character data
			
			// Search through all inventory data and add it to the inventory
			foreach (Item item in data.Items) {
				Inventory.AddItemToNextEmpty(item);
			}
			
			// Receive Room Data from the data
			
		}

		#endregion
	}
}