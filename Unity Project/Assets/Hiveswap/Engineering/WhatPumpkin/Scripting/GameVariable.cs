#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - March 20, 2015
#endregion 


#region using
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhatPumpkin.Data.XML;
using System.Xml;

using PixelCrushers.DialogueSystem;
#endregion

namespace WhatPumpkin.ScriptingLanguage {
	
	/// <summary>
	/// A game variable.
	/// </summary>

	[System.Serializable]

	public class GameVariable : Keyed, IGameVariable, IReceiver {

		#region static fields

		// The original intent for these collections is for sgrid.

		/// <summary>
		/// The game variables.
		/// </summary>

		static List<GameVariable> _gameVariables = new List<GameVariable>();

		/// <summary>
		/// Gets the game variables.
		/// </summary>
		/// <value>The game variables.</value>

		static public List<GameVariable> GameVariables { get { return _gameVariables; } }

		#endregion

		/*

		#region static fields

		/// <summary>
		/// The collection of game variables.
		/// </summary>

		static GameVariable [] _gameVariables;

		#endregion */

		#region member fields

		/// <summary>
		/// The name of the variable.
		/// </summary>

		[SerializeField] string _variableName = "";

		/// <summary>
		/// The current value of the variable.
		/// </summary>

		[SerializeField] string _sValue;

		/// <summary>
		/// The _default value.
		/// </summary>

		[SerializeField] string _defaultValue = "";

		/// <summary>
		/// Gets a value indicating whether this instance is a boolean.
		/// </summary>
		/// <value><c>true</c> if this instance is boolean; otherwise, <c>false</c>.</value>

		#endregion

		#region properties

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>

		public override string Key { get { return _variableName; } }

		/// <summary>
		/// Gets a value indicating whether this instance is boolean.
		/// </summary>
		/// <value><c>true</c> if this instance is boolean; otherwise, <c>false</c>.</value>

		bool IsBoolean { get { return Scripting.IsBoolean (_sValue); } }

		/// <summary>
		/// Gets a value indicating whether this instance is number.
		/// </summary>
		/// <value><c>true</c> if this instance is number; otherwise, <c>false</c>.</value>

		bool IsNumber { get {return Scripting.IsNumberValue(_sValue); } }


		/// <summary>
		/// Gets the number value.
		/// </summary>
		/// <value>The number value.</value>


		float NumberValue { 
			get {
				/// If this is a number value
				if(IsNumber) {

					float fValue = 0;
					float.TryParse(_sValue, out fValue);

					//return 
					return fValue;

				}
				else { // Otherwise, return 0
					return 0;
				}
			}
		}

		/// <summary>
		/// Gets the boolean value of this variable from its string value.
		/// </summary>
	
		bool BooleanValue {
		
			get {
				return Scripting.GetBooleanValue(_sValue);
			}
		
		}

		/// <summary>
		/// Gets the name of the variable.
		/// </summary>
		/// <value>The name.</value>

		public string Name { get { return _variableName; } }

		/// <summary>
		/// Gets the string value of this variable.
		/// </summary>
		/// <value>The string value.</value>

		public string StringValue { get { return _sValue; } }

		/// <summary>
		/// Gets the default value.
		/// </summary>
		/// <value>The default value.</value>

		public string DefaultValue { get { return _defaultValue; } }

		#endregion
	
		#region methods

		public GameVariable(string name, string value) {
		
			_variableName = name;
			_sValue = value;
		
		}

		/// <summary>
		/// Parses the assignment script.
		/// </summary>
		/// <param name="script">Script.</param>
		/// <param name="stringValue">String value.</param>

		public void Assign(string stringValue) {

			_sValue = stringValue;

			// TODO

			/*
			float value = 0;

			// Try to convert the string into a number if possible
			if(float.TryParse(s_value, out value))
			{
				// If the parse is successfull then handle the number variable
				// Set the variable as a number value
				DialogueLua.SetVariable(this.Name, value);
			}
			else{
				
				// If the parse was not successfull then handle as a string value
				// Set the variable as a string value
				DialogueLua.SetVariable(this.Name, s_value);
				
				
			}*/
		}
	

		internal void ChangeDefaults (string variableName, string defaultValue) {
		
			
			// Change the variable name
			_variableName = variableName;


			// Change the default value
			_defaultValue = defaultValue;

		
		}

		internal void ChangeProperties(string variableName, string sValue) {

			// Change the variable name
			_variableName = variableName;

			// Set the value
			SetValue (sValue);
		}

		internal void SetValue(string sValue) {
		
			_sValue = sValue;

			
			if(IsNumber) {
				// Set the lua value
				try {
					
					DialogueLua.SetVariable(_variableName, NumberValue);
				}
				catch (Exception e) {
					throw (e);
				}
				
			}
			else if (IsBoolean){
				
				DialogueLua.SetVariable(_variableName, BooleanValue);
				
			}
			else {
				
				// Default - Game variable is a string, use its string value
				DialogueLua.SetVariable(_variableName, _sValue);
				
			}

		}

		/// <summary>
		/// Resets the variable to it's default value.
		/// </summary>

		void ResetToDefaultValue() {
		
			_sValue = _defaultValue;

		}

		/// <summary>
		/// Sets to the default value.
		/// </summary>

		public void SetToDefault() {

			ResetToDefaultValue ();
		}



		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="WhatPumpkin.ScriptingLanguage.GameVariable"/> class.
		/// </summary>
		
		public GameVariable() {

			_gameVariables.Add (this);
			
		}

		#region implement IReceiver


		// TODO: I may not be using this at all

		public void Add(string item) {
		
			float incrementValue = 0;
			string s_value = item;


			// Try to convert the string into a number if possible
			if(float.TryParse(s_value, out incrementValue))
			{
				
				// Add
				float originalValue = DialogueLua.GetVariable(_variableName).AsInt;
				
				// If this is null - assign a default of 0
				if(originalValue == null) {
					originalValue = 0;
				}
				
				// Add value
				DialogueLua.SetVariable(_variableName, originalValue + incrementValue);
			}
			else{
				
				Debug.LogError("Could not convert the assignment to a number value");
				
			}

		}

		// TODO: I may not be using this at all

		public void Remove(string item) {

			float incrementValue = 0;
			string s_value = item;
			
			
			// Try to convert the string into a number if possible
			if(float.TryParse(s_value, out incrementValue))
			{
				
				// Add
				float originalValue = DialogueLua.GetVariable(_variableName).AsInt;
				
				// If this is null - assign a default of 0
				if(originalValue == null) {
					originalValue = 0;
				}
				
				// Add value
				DialogueLua.SetVariable(_variableName, originalValue - incrementValue);
			}
			else{
				
				Debug.LogError("Could not convert the assignment to a number value");
				
			}


		}

		#endregion

#if UNITY_EDITOR


		[ExecuteInEditMode]

		/// <summary>
		/// Sets the default properties. To be used in sgrid.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="defaultValue">Default value.</param>

		public void SetProperties(string key, string defaultValue) {
			ChangeDefaults (key, defaultValue);
			// Make sure to broadcast the rename event
			RenameKey (key);



		}

#endif

	}
}