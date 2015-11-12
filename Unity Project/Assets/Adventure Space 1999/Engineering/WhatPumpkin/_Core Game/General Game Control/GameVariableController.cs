#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 28, 2015
#endregion 

#region using
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using WhatPumpkin.ScriptingLanguage;
using PixelCrushers.DialogueSystem;
#endregion

namespace WhatPumpkin {

	/// <summary>
	/// Game variable controller. A collection of all the variables in the game
	/// </summary>

	public class GameVariableController : MonoBehaviour {


		#region static fields

		/// <summary>
		/// The instance.
		/// </summary>
		
		static GameVariableController _instance; 

		#endregion

		#region static properties

		static public GameVariableController Instance { get { return _instance; } }


		#endregion


		#region fields


		/// <summary>
		/// A collection of all the game variables
		/// </summary>

		[SerializeField] List<GameVariable> _gameVariables = new List<GameVariable>(); 

		

		#endregion


		#region events

		/// <summary>
		/// Occurs when a variable is set.
		/// </summary>

		public event EventHandler<VariableEventArgs> SetVariable;

		#endregion

		#region properties

		/// <summary>
		/// Gets the game variables.
		/// </summary>
		/// <value>The game variables.</value>

		public List<GameVariable> GameVariables { get { return _gameVariables; } }

		#endregion


		#region methods

		void Awake() {

			if (_instance == null) {
				_instance = this;
				SaveLoad.UserLoadsScene += HandleUserLoadsScene;
			}
		}

		void HandleUserLoadsScene (object sender, SaveEventArgs e)
		{
			_gameVariables.Clear ();
		}


		void Start() {
			InitializeGameVariables ();
		}

		/// <summary>
		/// Initializes the game variables.
		/// </summary>
		
		void InitializeGameVariables() {ResetAllToDefault ();}

		/// <summary>
		/// Resets all game variabls to default.
		/// </summary>
		
		public void ResetAllToDefault() {
			
			// Search through each game variable and reset it to it's default value
			
			foreach (GameVariable gameVariable in _gameVariables) {
				
				gameVariable.SetToDefault();
				
			}
			
		}

		public void AssignVariable (string variableName, string value) {
		

			// Get the variable
			GameVariable gameVariable = GetVariable (variableName);

			// If the variable doesn't exist then create a new one
			if (gameVariable == null) {
				_gameVariables.Add(new GameVariable(variableName, value));
				return;
			}

			// Otherwise, assign the variable value
			gameVariable.Assign(value);

		}

		/// <summary>
		/// Gets the variable.
		/// </summary>
		/// <returns>The variable.</returns>
		/// <param name="name">Name.</param>

		GameVariable GetVariable(string name) {
		
			foreach (GameVariable variable in _gameVariables) {
			
				if(name == variable.Name) {
				
					return variable;
				}
			
			}


			return null;
		}

		/// <summary>
		/// Adds a game variable to the list of game variables
		/// </summary>
		/// <param name="gameVariable"></param>

		public void AddVariable(GameVariable gameVariable) {

			// Add the variable to the list
			_gameVariables.Add (gameVariable);

			// Set the variable with the lua properties
			// TODO: It would be nice to use a wrapper to control this this way I can easily slot out lua if need be
			DialogueLua.SetVariable (gameVariable.Name, gameVariable.StringValue);
		
		}

		bool HasVariable(string name) {
		
			// TODO: Optimize
			foreach (GameVariable gameVariable in _gameVariables) {
			
				if(gameVariable.Name == name) {
					return true;
				}
			
			}

			return false;
		
		}

		public void OnVariableSet(string name, string value) {

		
			if (SetVariable != null) {
			
				SetVariable(this, new VariableEventArgs(name, value));
			
			}
			else {
//				Debug.Log ("Set Var Event is null");
			}
		
		}

		#endregion

#if UNITY_EDITOR

		/// <summary>
		/// Adds a variable to the game variables list.
		/// </summary>

		public void AddVariable() {
		
			_gameVariables.Add(new GameVariable());
			//_gameVariables = DataUtilities.AddArrayElement<GameVariable> (_gameVariables, new GameVariable ());

		}


#endif

	}

	public class VariableEventArgs : System.EventArgs {
	
		string _variableName;

		string _variableValue;

		public string VariableName { get { return _variableName;} }

		public string VariableValue { get { return _variableValue; } }

		public VariableEventArgs(string name, string value) {

			_variableName = name;
			_variableValue = value;
		
		}
	
	
	}
}
