#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 26, 2015
#endregion 

#if UNITY_EDITOR

#region using
using UnityEngine;
#endregion

namespace WhatPumpkin.EditorWindowUtilities {

	[System.Serializable]

	public class ToggleInput : PropertyInput<bool, bool, string>  {


		/// <summary>
		/// The default constructor.
		/// </summary>
		/// <param name="width">Width.</param>

		public ToggleInput(string key, float width) {
	
			_width = width;

			AddToKeyList (key);
		
		}


		public ToggleInput(string key, float width, Color defaultColor, Color selectedColor) {
		
			_width = width;
			_defaultColor = defaultColor;
			_selectedColor = selectedColor;

			AddToKeyList (key);
		
		}

		void AddToKeyList(string key) {

			PropertyInput<bool, bool, string> propertyInput;
			if (_inputFields.TryGetValue(key, out propertyInput)  == false) {
				
				_inputFields.Add (key, this);
			}

		}

		/// <summary>
		/// Draw the specified content. Designed to be within the scope of a Monobehaviour's OnGUI method.
		/// Returns true if the button was pressed
		/// </summary>
		/// <param name="content">Content.</param>

		public override bool Draw(bool content, bool isSelected = false, params string [] parameters) {

			// Return a simple color  field	
			return EditorWindowUtils.DrawSimpleToggle(content, parameters[0]);

		}

	}
}

#endif
