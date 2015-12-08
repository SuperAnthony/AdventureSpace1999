#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created -  Novemeber 7, 2014
#endregion 

#region using
using UnityEngine;
using System;
using System.Collections;
#endregion

namespace WhatPumpkin.Screens {

	/// <summary>
	/// GameScreen - all of our screens will have this component. 
	/// Any features we want our UI Screens to have will be added here
	/// </summary>

	#region required components
	[RequireComponent(typeof(Switch))]
	#endregion
	
	public class GameScreen : MonoBehaviour, IGameScreen, ISwitchable {

		static public event EventHandler<GameScreenEventArgs> ScreenEvent;

		#region member fields

		[SerializeField] bool _canEscapeFrom = false;

		/// <summary>
		/// Switch component used for switching between active and inactive states 
		/// </summary>
		Switch _switch;
		#endregion

		#region member properties

		/// <summary>
		/// Is this screen active
		/// </summary>
		/// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>

		public bool IsActive { get { return gameObject.activeSelf; } }


		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get { return this.name; } }

		#endregion

		// Use this for initialization
		#region methods
		void Start () {


			// Get the switch

			// Get the switch component
			_switch = this.gameObject.GetComponent<Switch> ();


			// If a switch doesn't exist, add the component but send an error message
			if (_switch == null) {
				Debug.LogError ("Could not find a switch on the " + this.gameObject.name + " game screen. Adding a switch but this could cause issues, please attach the switch component to it in your scene.");
				_switch = this.gameObject.AddComponent<Switch>();
			}

		}

	

		/// <summary>
		/// Activate this instance.
		/// </summary>

		public virtual void Activate() {

			Open ();
		}

		/// <summary>
		/// Deactivate this instance.
		/// </summary>

		public virtual void Deactivate() {

			Close ();
		}

		/// <summary>
		/// Gets the name of the key.
		/// </summary>
		/// <returns>The key name.</returns>

		string GetKeyName() {
		
			// This is because I did not require the game screens to inherit from entity. 
			// Which is something I would like to do but do not want to risk doing at this moment
			string key = this.gameObject.name;
			Sgrid.EntityInfo entityInfo = this.GetComponent<Sgrid.EntityInfo> ();

			if(entityInfo != null) {
//				Debug.Log ("Entity Info Found: " + entityInfo.Key);
				key = entityInfo.Key;
			
			}

			return key;
			
		}

		/// <summary>
		/// Close this screen.
		/// </summary>
		public virtual void Close() {

			// Close this screen by deactivating the gameobject
			this.gameObject.SetActive (false);

			// Raise screen event
			if (ScreenEvent != null) {ScreenEvent(this, new GameScreenEventArgs(GetKeyName(),ScreenEventType.Close));}

		}

		/// <summary>
		/// Open this screen.
		/// </summary>

		public virtual void Open () {

			// If the user can exit this screen with the escape key then add the deactivation to the scree managers escape key delegate
			if (_canEscapeFrom) {GameController.ScreenManager.ReceiveEscapeDelegate (Deactivate);}
			// Open this screen by activating the gameobject
			this.gameObject.SetActive (true);
			// Raise screen event
			if (ScreenEvent != null) {ScreenEvent(this, new GameScreenEventArgs(GetKeyName(),ScreenEventType.Open));}
		}

		/// <summary>
		/// Switch this instance.
		/// </summary>

		public void SwitchActiveState() {
			_switch.SwitchActiveState ();
		}


		// Update is called once per frame
		void Update () {
		
		}
		#endregion
	}


	public enum ScreenEventType { Open, Close }
	
	public class GameScreenEventArgs : EventArgs {
	
		// The key of the screen being affected
		string _key; 

		// The type of event (open, close, etc)
		ScreenEventType _eventType;

		public ScreenEventType ScreenEvent { get { return _eventType; } } 

		public string Key { get { return _key; } }

		public GameScreenEventArgs(string screen_key, ScreenEventType eventType) {
			_key = screen_key;
			_eventType = eventType;
		}
	
	}


}
