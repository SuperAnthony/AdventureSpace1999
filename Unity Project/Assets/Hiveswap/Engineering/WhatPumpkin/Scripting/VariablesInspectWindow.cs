#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - March 31, 2015
#endregion 
/*
using UnityEngine;
using UnityEditor;
using System.Collections;


namespace WhatPumpkin.ScriptingLanguage {



	public class VariablesInspectWindow : EditorWindow  {

		static GameVariable [] _gameVariables;

		/// <summary>
		/// The default color.
		/// </summary>

		static Color defaultColor = new Color (1F, 1F, 1F);

		/// <summary>
		/// The title.
		/// </summary>
		
		static string title = "Variables List";
		
		/// <summary>
		/// The color of the banner.
		/// </summary>
		
		static Color bannerColor = new Color(.623F,0F,.501F);
		
		/// <summary>
		/// The color of the background.
		/// </summary>
		
		static Color backgroundColor = new Color (1F, .701F, .941F);
		
		/// <summary>
		/// The color of the selection.
		/// </summary>
		
		static Color selectionColor  = new Color (.01F, .52F, .64F);

		/// <summary>
		/// The height of the banner.
		/// </summary>
		
		static float bannerHeight = 35F;


		/// <summary>
		/// The width of the variable name text field.
		/// </summary>

		static float variableNameTextFieldWidth = 130F;

		/// <summary>
		/// The width of the variable value text field.
		/// </summary>

		static float variableValueTextFieldWidth = 130F;

		/// <summary>
		/// Open this window.
		/// </summary>

		[MenuItem ("HiveSwap/sgrid/Variable List &v")]

		static void Open() {
			// Open the window
			EditorWindow.GetWindow(typeof(VariablesInspectWindow));
			/*
			// Assigne the game variales to the game controller's variable colleciton
			if (GameController.Instance != null && GameController.Instance.GameVariables != null) {
			
				_gameVariables = GameController.Instance.GameVariables;
			
			}*/
	//	}

		
		/// <summary>
		/// Draws the title.
		/// </summary>
	/*	
		static void DrawTitle() {
			
			EditorGUILayout.BeginHorizontal ();
			GUILayout.Label ("");
			GUIStyle style = new GUIStyle();
			style.fontSize = 25;
			style.fontStyle = FontStyle.Bold;
			style.normal.textColor = Color.white;
			GUI.color = Color.white;			
			GUILayout.Label (title,style);
			EditorGUILayout.EndHorizontal ();
			
			GUI.color = defaultColor;
			
		}

		/// <summary>
		/// Draws the banner.
		/// </summary>
		
		void DrawBanner() {
			
			EditorGUI.DrawRect (new Rect(0, 0, this.position.width, bannerHeight), bannerColor);
			
		}
		
		/// <summary>
		/// Draws the background.
		/// </summary>
		
		void DrawBackground() {
			EditorGUI.DrawRect (new Rect(0, 0, this.position.width, this.position.height), backgroundColor);
			
		}

		/// <summary>
		/// Draws the variable list.
		/// </summary>

		static void DrawVariableList() {
		
			if (_gameVariables == null) {
				return;			
			}

			foreach (GameVariable gameVariable in _gameVariables) {
			
				EditorGUILayout.BeginHorizontal();

				string varName = GUILayout.TextField(gameVariable.Name, GUILayout.Width(variableNameTextFieldWidth));
				string defaultValue = GUILayout.TextField(gameVariable.DefaultValue, GUILayout.Width(variableValueTextFieldWidth));

				// Set the values
				gameVariable.ChagneDefaults(varName, defaultValue);

				EditorGUILayout.EndHorizontal();
			}

		}

		/// <summary>
		/// Draws the header.
		/// </summary>

		static void DrawHeader() {

			// Draw a label
			GUILayout.Label ("");
			
			EditorGUILayout.BeginHorizontal();
			GUILayout.Label("Variable Name", GUILayout.Width(variableNameTextFieldWidth));
			GUILayout.Label("Value", GUILayout.Width(variableValueTextFieldWidth));
			EditorGUILayout.EndHorizontal();
			
		}



		/// <summary>
		/// Raises the on GUI event.
		/// </summary>

		void OnGUI () {

			// Draw the background
			DrawBackground ();
			
			// Draw the banner
			DrawBanner ();
			
			// Display the title
			DrawTitle ();

			// Draw the header 
			DrawHeader ();

			// Draw the list of variables
			DrawVariableList ();
		
		}

	}
}
*/