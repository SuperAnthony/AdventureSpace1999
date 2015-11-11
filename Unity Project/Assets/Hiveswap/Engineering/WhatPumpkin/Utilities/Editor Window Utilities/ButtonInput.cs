#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 18, 2015
#endregion 

#if UNITY_EDITOR

#region using
using UnityEngine;
using System.Collections.Generic;
#endregion

namespace WhatPumpkin.EditorWindowUtilities {

	[System.Serializable]

	public class ButtonInput : PropertyInput<bool, string, object>  {

		//static Dictionary<string, ButtonInput> _buttonInputs = new Dictionary<string, ButtonInput> ();

		/// <summary>
		/// The default constructor.
		/// </summary>
		/// <param name="width">Width.</param>

		public ButtonInput(string key, float width) {
	
			_width = width;

	
			PropertyInput<bool, string, object> propertyInput;
			if (_inputFields.TryGetValue(key, out propertyInput)  == false) {
								
							_inputFields.Add (key, this);
			}
		
		}




		public ButtonInput(string key, float width, Color defaultColor, Color selectedColor) {

			//Key = key;
			_width = width;
			_defaultColor = defaultColor;
			_selectedColor = selectedColor;

			PropertyInput<bool, string, object> propertyInput;
			if (_inputFields.TryGetValue(key, out propertyInput)  == false) {
				
				_inputFields.Add (key, this);
			}
		}
		

		/// <summary>
		/// Draw the specified content. Designed to be within the scope of a Monobehaviour's OnGUI method.
		/// Returns true if the button was pressed
		/// </summary>
		/// <param name="content">Content.</param>

		public override bool Draw(string content, bool isSelected = false, params object [] parameters) {

			if(isSelected){UseSelectColor ();}
			bool result = EditorWindowUtils.DrawSimpleButton(content, _width);
			if(isSelected){ClearSelectColor ();}

			// Return the result
			return result;
		}

	}
}

#endif
