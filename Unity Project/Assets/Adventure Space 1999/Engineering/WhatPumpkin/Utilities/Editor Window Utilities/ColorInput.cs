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

	public class ColorInput : PropertyInput<Color, Color, object>  {


		/// <summary>
		/// The default constructor.
		/// </summary>
		/// <param name="width">Width.</param>

		public ColorInput(string key, float width) {
	
			_width = width;

	
			PropertyInput<Color, Color, object> propertyInput;
			if (_inputFields.TryGetValue(key, out propertyInput)  == false) {
								
							_inputFields.Add (key, this);
			}
		
		}

		

		/// <summary>
		/// Draw the specified content. Designed to be within the scope of a Monobehaviour's OnGUI method.
		/// Returns true if the button was pressed
		/// </summary>
		/// <param name="content">Content.</param>

		public override Color Draw(Color content, bool isSelected = false, params object [] parameters) {

			// Return a simple color  field	
			return EditorWindowUtils.DrawSimpleColorField (content, _width);

		}

	}
}

#endif
