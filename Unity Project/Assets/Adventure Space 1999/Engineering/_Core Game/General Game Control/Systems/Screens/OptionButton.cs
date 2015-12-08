#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created -  May 5, 2015
#endregion 

#region using
using UnityEngine;
using UnityEngine.UI;
using System;
#endregion


namespace WhatPumpkin.Screens {

	/// <summary>
	/// The button that stores an IOption
	/// </summary>


	public class OptionButton : MonoBehaviour {

		#region events
		public event EventHandler Selected;
		#endregion

		#region fields

		/// <summary>
		/// /The front facing unity UI text of this option
		/// </summary>

		[SerializeField] Text _text;

		/// <summary>
		/// The option this button is assocaited with.
		/// </summary>

		IOption _option;

		#endregion

		#region methods

		/// <summary>
		/// Receives the option associated with this Option Button.
		/// </summary>
		/// <param name="option">Option.</param>

		internal void ReceiveOption(IOption option) {
			_option = option;
			_text.text = _option.Text;
		}

		/// <summary>
		/// Receives the option associated with this Option Button.
		/// </summary>
		/// <param name="option">Option.</param>
		/// <param name="text">Text.</param>

		internal void ReceiveOption(IOption option, string text) {
			_option = option;
			_text.text = text;
		}

		/// <summary>
		/// Select this instance. Should be set up to occur when the user presses the button associated with this.
		/// </summary>

		public void Select() {

			// Raise the selected event
			if (Selected != null) {Selected(this, null);}
		
			// Select the option
			if (_option != null) {_option.Select ();}
		
		}

		#endregion

	}
}
