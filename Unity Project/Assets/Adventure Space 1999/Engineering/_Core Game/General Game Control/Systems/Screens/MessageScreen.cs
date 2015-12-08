#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created -  January 13, 2015
#endregion 

#region using
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
#endregion

namespace WhatPumpkin.Screens {

	/// <summary>
	/// MessageScreen - a game screen with one message associated with it
	/// Used for things such as Narrator Text and Barks
	/// </summary>

	public class MessageScreen : GameScreen, IMessageScreen, IMessageOptionsScreen {

		#region fields

		/// <summary>
		/// The UI Text message associated with the screen
		/// This should be set up in the inspector
		/// </summary>

		[SerializeField] Text _message;

		// [SerializeField] OptionButton

		/// <summary>
		/// The options this message screen may have.
		/// </summary>

		[SerializeField] OptionButton [] _optionButtons;

		#endregion

		#region methods

		public void Start() {
		
			// Handle button select
			foreach (OptionButton optionButton in _optionButtons) {
				optionButton.Selected += HandleSelected;
			}

		}

		/// <summary>
		/// Handles the selected option button.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>

		void HandleSelected (object sender, System.EventArgs e)
		{
			// Close this when an option is selected
			Close ();
		}

		public void OnDestroy() {

			foreach (OptionButton optionButton in _optionButtons) {
				if(optionButton != null) {
					optionButton.Selected -= HandleSelected;
				}
			}
		
		}

		/// <summary>
		/// Open the specified message.
		/// </summary>
		/// <param name="message">Message.</param>

		public void Open(string message, bool isGraphicMessage = false) {
		
			// Set the new message 
			if (_message != null) {
				_message.text = message;
			}

			// Open the screen
			Open ();
		}

		/// <summary>
		/// Open a message screen, specifies a message and it's options.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="options">Options.</param>

		public void Open(string message, IOption [] options) {
		
			// Display the screen
			Open ();

			// Set the new message 
			if (_message != null) {_message.text = message;}

			// Receive the options
			ReceieveOptions (options); 
		}

		/// <summary>
		/// Clears the options.
		/// </summary>
		/*
		void ClearOptions() {
		/*
			for (int i = 0; i < _optionButtons.Length; i++) {
				_optionButtons[i] = null;
			}
			*/
		
		//}

		/// <summary>
		/// Receieves the options.
		/// </summary>
		/// <param name="options">Options.</param>

		void ReceieveOptions(IOption [] options) {

			// Clear the options
			//ClearOptions ();

			// Pass the option along to the button
			for (int i = 0; i < _optionButtons.Length; i++) {
				Debug.Log (options[i]);
				Debug.Log (_optionButtons[i]);
				_optionButtons[i].ReceiveOption(options[i]);
			}
		}

		#endregion

	}

}
