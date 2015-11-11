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

	public class PopupInput : PropertyInput<int, int, string>  {

		/// <summary>
		/// The _popup inputs.
		/// </summary>

		static Dictionary<string, PopupInput> _popupInputs = new Dictionary<string, PopupInput> ();

		/// <summary>
		/// Gets the popup inputs.
		/// </summary>
		/// <value>The popup inputs.</value>

		static Dictionary<string, PopupInput> PopupInputs { get { return _popupInputs; } }

		/// <summary>
		/// The default constructor.
		/// </summary>
		/// <param name="width">Width.</param>

		public PopupInput(string key, float width) {
		
			_key = key;
			_width = width;
		
			PropertyInput<int, int, string> propertyInput;
			if (_inputFields.TryGetValue(key, out propertyInput)  == false) {
				
				_inputFields.Add (key, this);
			}

		}


		public PopupInput(string key, float width, Color defaultColor, Color selectedColor) {

			_key = key;
			_width = width;
			_defaultColor = defaultColor;
			_selectedColor = selectedColor;

			
			PropertyInput<int, int, string> propertyInput;
			if (_inputFields.TryGetValue(key, out propertyInput)  == false) {
				
				_inputFields.Add (key, this);
			}
		}



		/// <summary>
		/// Draw the specified content. Designed to be within the scope of a Monobehaviour's OnGUI method.
		/// </summary>
		/// <param name="content">Content.</param>

		public override int Draw(int content, bool isSelected = false, params string [] parameters) {
			if(isSelected){UseSelectColor();}
			content = EditorWindowUtils.DrawSimplePopup (content, parameters, _width);
			if(isSelected){ClearSelectColor ();}
			return content;
		}
	}
}

#endif
