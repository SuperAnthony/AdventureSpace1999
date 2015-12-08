#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - August 25, 2015
#endregion 

using UnityEngine;
using System.Collections;

namespace WhatPumpkin {

	/// <summary>
	/// Props
	/// </summary>

	public class Prop : Sgrid.Entity, ISwitchable {
		

		public enum Hand {Left, Right};

		#region fields

		/// <summary>
		/// The hand this object is in
		/// </summary>

		[SerializeField] Hand _inHand = Hand.Left;

		/// <summary>
		/// The left hand node that this object will follow locally.
		/// </summary>

		[SerializeField] Transform _leftHand;

		/// <summary>
		/// The right hand node that this object will follow locally.
		/// </summary>
		
		[SerializeField] Transform _rightHand;

		/// <summary>
		/// Is this object active?
		/// </summary>

		[SerializeField] bool _isActive = false;

		[SerializeField] Transform _propHandle;

		#endregion

		#region properties

		/// <summary>
		/// Gets a value indicating whether this instance is active.
		/// </summary>
		/// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>

		public override bool IsActive { get { return _isActive; } }

		#endregion

		#region methods

		void Awake() {
		
			// Set this object
			Set ();

			// If this object is active then show it
			// If not, then hide it

			if (_isActive) {
				Show ();		
			}
			else {
				Hide ();
			}

		}


		/// <summary>
		/// Set this instance.
		/// </summary>

		void Set() {
		
			if (_inHand == Hand.Left && _leftHand != null) {this.gameObject.transform.SetParent (_leftHand);}
			else if (_inHand == Hand.Right  && _rightHand != null) {this.gameObject.transform.SetParent (_rightHand);}

			// Set the local position to 0
			this.transform.localPosition = new Vector3 (0F, 0F, 0F);
			this.transform.localRotation = new Quaternion (0F, 0F, 0F, 0F);
		}

		/// <summary>
		/// Switches the object to the specified hand
		/// </summary>
		/// <param name="toHand">To hand.</param>

		public void SwitchHand(Hand toHand) {_inHand = toHand; Set ();}

		/// <summary>
		/// Switches the object to the opposite hand
		/// </summary>

		public void SwitchHand() {


			if (_inHand == Hand.Left) {
				_inHand = Hand.Right;		
			}
			else {
				_inHand = Hand.Left;
			}

			Set ();
		}

		/// <summary>
		/// Activate this instance.
		/// </summary>

		public override void Activate() {
		
			Set ();
			_isActive = true;
			Show ();

		}

		/// <summary>
		/// Deactivate this instance.
		/// </summary>

		public override void Deactivate() {

			_isActive = false;
			Hide ();

		}

		/// <summary>
		/// Show this instance.
		/// </summary>

		void Show() {
		
			if (_propHandle != null) {_propHandle.gameObject.SetActive(true);}


		}

		/// <summary>
		/// Hide this instance.
		/// </summary>

		void Hide() {
		
			if (_propHandle != null) {_propHandle.gameObject.SetActive(false);}

		}


		#endregion 

	}
}
