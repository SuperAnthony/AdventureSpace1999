#region Copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - November 21, 2014
#endregion

#region using
using UnityEngine;
using System.Collections;
#endregion

namespace WhatPumpkin {

	public class Switch : MonoBehaviour {


		#region fields

		/// <summary>
		/// The activatable object that is affect.
		/// TODO: Get a list of activateable object.
		/// </summary>

		ISwitchable _activatableObject;


		/// <summary>
		/// Is this instance active at the start
		/// </summary>

		[SerializeField] bool _isActiveOnStart = true;

		/// <summary>
		/// Is this instance active.
		/// </summary>

		bool _isActive;

		// Use this for initialization
		void Awake () {

		
			_activatableObject = this.GetComponent(typeof(ISwitchable)) as ISwitchable;

			// Activate is we are activating on start
			// Deactivate if it is not supposed to be active on start
			if (_isActiveOnStart) {
				Activate();
			}
			else {
				Deactivate();
			}

		}


		/// <summary>
		/// Activate this instance.
		/// </summary>

		public void Activate() {

			// Does this object have it's own Activatable behaviour
			if (_activatableObject != null) {
				// If so, then use it to activate
				_activatableObject.Activate();
				_isActive = _activatableObject.IsActive;
			}
			else {
				// If not, then just disable the game object
				this.gameObject.SetActive(true);
				_isActive = true;
			}

		
		
		}

		/// <summary>
		/// Deactivate this instance.
		/// </summary>

		public void Deactivate() {

			// Does this object have it's own Activatable behaviour
			if (_activatableObject != null) {
				// If so, then use it to activate
				_activatableObject.Deactivate();
				_isActive = _activatableObject.IsActive;
			}
			else {
				// If not, then just disable the game object
				this.gameObject.SetActive(false);
				_isActive = false;
			}
			


		}

		/// <summary>
		/// Switchs the state of the active.
		/// </summary>

		public void SwitchActiveState() {

			if (_activatableObject != null) {
				_isActive = _activatableObject.IsActive;			
			}

			if(_isActive) {
				Deactivate();
			}
			else {
				Activate();
			}

		}

		#endregion

	
	}

}
