#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 6, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;
#endregion


namespace WhatPumpkin.Debuging {


	/// <summary>
	/// Debug console input.
	/// </summary>

	public class DebugConsoleInput : MonoBehaviour {



		bool IsActive { get { return DebugConsole.IsActive; } }

		/// <summary>
		/// The console command.
		/// </summary>

		string _consoleCommand = ""; 

		/// <summary>
		/// The width.
		/// </summary>
		
		float _width = 800F;

	
		/// <summary>
		/// Raises the update event.
		/// </summary>

		void Update() {
		
			HandleInput ();

			//Debug.Log ("Is mouse over poiner event: " + GameObject.FindObjectOfType<UnityEngine.EventSystems.EventSystem> ().IsPointerOverGameObject ());

		}

		/// <summary>
		/// Handles the input.
		/// </summary>


		void HandleInput() {
		

			// Enter (return) submits the console command
			if (Input.GetKeyDown (KeyCode.KeypadEnter) || Input.GetKeyDown (KeyCode.Return)) {DebugConsole.ReceiveCommand(_consoleCommand);}

		}




		/// <summary>
		/// Raises the GUI event.
		/// </summary>

		void OnGUI () {

//			Debug.Log ("Num Sequences Playing: " + WhatPumpkin.Actions.Sequences.ActionSequence.NumSequencesPlaying);


			Event e = Event.current;

			if (e.type == EventType.keyDown  && (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter)) {
			
				if(IsActive) {
					DebugConsole.ReceiveCommand(_consoleCommand);
				}
			
			}

			if (e.type == EventType.keyDown && (e.keyCode == KeyCode.Tab)) {
				DebugConsole.Switch();
				if(IsActive) {
					GUI.FocusControl("Console Text Field");
				}
			}
			if(IsActive){	

				try {
					GUI.SetNextControlName("Console Text Field");
					_consoleCommand = GUILayout.TextField (_consoleCommand, GUILayout.Width(_width));

				}
				catch{
				
					// Argument Exception? Not sure why, but this seems to be working and is not too integral
				}

				// Submit Console Command? (Wasn't working and then was for some reason?)
				/*
				if (e.type == EventType.keyDown && (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter)) {      
					DebugConsole.ReceiveCommand(_consoleCommand);
				}*/

			}	

			GUILayout.Label ("Num Sequences Playing: " + WhatPumpkin.Actions.Sequences.ActionSequence.NumSequencesPlaying, GUILayout.Width (_width));

		}

	}
}