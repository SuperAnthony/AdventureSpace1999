#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - April 13, 2015
#endregion 

#if UNITY_EDITOR

#region using
using UnityEngine;
using UnityEditor;
using System.Collections;

#endregion

/// <summary>
/// Editor window utils.
/// </summary>

public class EditorWindowUtils : EditorWindow {

	/// <summary>
	/// Draws a simple text field.
	/// </summary>
	/// <returns>The simple text field.</returns>
	/// <param name="text">Text.</param>
	/// <param name="width">Width.</param>
	
	static public string DrawSimpleTextField(string text, float width) {
		return GUILayout.TextField (text, GUILayout.Width (width));	
	}
	
	/// <summary>
	/// Draws the banner.
	/// </summary>
	
	static public void DrawBanner(float width, float thickness, Color color) {
		
		EditorGUI.DrawRect (new Rect(0, 0, width, thickness), color);
		
	}
	
	/// <summary>
	/// Draws the background.
	/// </summary>
	
	static public void DrawBackground(float width, float height, Color color) {
		EditorGUI.DrawRect (new Rect(0, 0, width, height), color);
		
	}

	/// <summary>
	/// Draws a simple button.
	/// </summary>

	static public bool DrawSimpleButton(string content, float width) {
	
		return GUILayout.Button (content, GUILayout.Width (width));
	
	}
	
	/// <summary>
	/// Draws the popup.
	/// I think popup is stupidly named (by Unity) this to me is more of a dropdown.
	/// </summary>
	/// <returns>The popup. </returns>
	/// <param name="selected">Selected.</param>
	/// <param name="options">Options.</param>
	/// <param name="width">Width.</param>
	
	static public int DrawSimplePopup(int selected, string [] options, float width) {
		
		return EditorGUILayout.Popup (selected, options, GUILayout.Width (width));
	}

	/// <summary>
	/// Draws the simple color field.
	/// </summary>
	/// <returns>The simple color field.</returns>
	/// <param name="content">Content.</param>
	/// <param name="width">Width.</param>

	static public Color DrawSimpleColorField(Color content, float width) {

		return EditorGUILayout.ColorField (content, GUILayout.Width (width));

	}

	static public bool DrawSimpleToggle(bool selected, string displayContent) {
	

		return false;
		//return EditorGUILayout.Toggle (selected, displayContent);
	
	}

	/// <summary>
	/// Draws the radio buttons.
	/// </summary>
	/// <returns>The radio buttons.</returns>
	/// <param name="selected">Selected.</param>
	/// <param name="options">Options.</param>
	/// <param name="max_buttons_row">Max_buttons_row.</param>
	/// <param name="selectedColor">Selected color.</param>

	static public int DrawRadioButtons(int selected, string [] options, float buttonWidth, int max_buttons_row, Color selectedColor) {

		int row = 1;

		// Draw buttons, make new lines when appropriate
		for (int i = 0; i < options.Length; i++) {
		
			// Make new line if necessary
			if(i + 1 > (max_buttons_row * row)){
				row++;
				EditorGUILayout.EndHorizontal ();
				EditorGUILayout.BeginHorizontal ();
			}

			// Check to see if this button is selected, if so, use the selected color, if not use a default color (white)
			if(i == selected) {GUI.backgroundColor = selectedColor;}

			// Draw option | If the button is pressed then change the selection

			if(DrawSimpleButton(options[i], buttonWidth)){selected = i;}

			// Draw the default color, white
			GUI.backgroundColor = Color.white;
		}

		return selected;
		
	}


}

#endif
