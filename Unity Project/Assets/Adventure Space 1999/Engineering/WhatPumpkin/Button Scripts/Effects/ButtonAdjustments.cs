#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - March 12, 2015
#endregion 


#region using
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
#endregion

namespace WhatPumpkin.UI {

	/// <summary>
	/// ButtonFX - Adjustments that can be made to any unity button on the fly
	/// </summary>

	public class ButtonAdjustments {

		#region static methods

		/// <summary>
		/// Gets the button text.
		/// </summary>

		static public Text GetText(Button button) {
				
			// Search through the button's children to find the text
			// and return it

			foreach (Transform t in button.transform) {
			
				Text text = t.GetComponent<Text>();

				if(text != null) {
					return text;
				}
			
			}

			return null;
		
		}

		/// <summary>
		/// Gets the text of a game object with the specified name.
		/// </summary>
		/// <param name="name">Name.</param>

		static public Text GetText(Button button, string goName) {
		
			// TODO:

			return null;
		
		}


		/// <summary>
		/// Searches for a game object with a text component attached and then changes that text.
		/// </summary>
		/// <param name="button">Button.</param>
		/// <param name="textBody">Text body.</param>

		static public void ChangeText(Button button, string textBody) {
		
			GetText(button).text = textBody;
		
		}


		#endregion

	}
}