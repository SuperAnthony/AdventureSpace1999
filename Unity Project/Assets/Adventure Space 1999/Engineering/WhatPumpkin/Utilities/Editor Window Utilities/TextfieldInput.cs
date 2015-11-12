#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 18, 2015
#endregion 

#if UNITY_EDITOR

#region using
using UnityEngine;
#endregion

namespace WhatPumpkin.EditorWindowUtilities {


	[System.Serializable]

	public class TextfieldInput : PropertyInput<string, string, object>  {


		/// <summary>
		/// The default constructor.
		/// </summary>
		/// <param name="width">Width.</param>

		public TextfieldInput(string key, float width) {
		
			_key = key;
			_width = width;
		
			PropertyInput<string, string, object> propertyInput;
			if (_inputFields.TryGetValue(key, out propertyInput)  == false) {
				
				_inputFields.Add (key, this);
			}
			
		}
		
		
		public TextfieldInput(string key, float width, Color defaultColor, Color selectedColor) {

			_key = key;
			_width = width;
			_defaultColor = defaultColor;
			_selectedColor = selectedColor;

			// TODO: This will get moved
			PropertyInput<string, string, object> propertyInput;
			if (_inputFields.TryGetValue(key, out propertyInput)  == false) {
				
				_inputFields.Add (key, this);
			}
		}
		
		
		
		/// <summary>
		/// Draw the specified content. Designed to be within the scope of a Monobehaviour's OnGUI method.
		/// </summary>
		/// <param name="content">Content.</param>

		public override string Draw(string content, bool isSelected = false, params object [] parameters) {

			if(isSelected){UseSelectColor ();}
			// Draw the content
			content = EditorWindowUtils.DrawSimpleTextField (content, _width);
			// Return to the default color
			if(isSelected){ClearSelectColor ();};
			
			// Return the content
			return content ;
		}
	}
}

#endif
