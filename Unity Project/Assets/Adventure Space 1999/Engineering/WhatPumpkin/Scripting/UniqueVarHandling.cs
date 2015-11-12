#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - February 19, 2015
#endregion 


#region using
using UnityEngine;
using System.Collections;
#endregion

namespace WhatPumpkin.ScriptingLanguage {

	/// <summary>
	/// 
	/// </summary>

	public class UniqueVarHandling  {

		#region constants

		const string HASITEM = "hasItem";

		#endregion

		#region static fields


		static string[] _uniqueVariables = new string [1] {HASITEM};


		#endregion
		/*
		static void Init() {
		
			_uniqueVariables [0] = "HasItem";
		
		}*/

		#region static methods

		/// <summary>
		/// Determines if specified variableName has unique qualities.
		/// </summary>
		/// <returns><c>true</c> if is unique the specified variableName; otherwise, <c>false</c>.</returns>
		/// <param name="variableName">Variable name.</param>

		static public bool HasUniqueQualities(string variableName) {

			foreach (string uVar in _uniqueVariables) {
			
				if(uVar == variableName || "!"+uVar == variableName) {
					return true;
				}
			
			}

			return false;
		}

		/// <summary>
		/// Handles the condition.
		/// </summary>
		/// <returns><c>true</c>, if condition was handled, <c>false</c> otherwise.</returns>
		/// <param name="variableName">Variable name.</param>
		/// <param name="parameter">Parameter.</param>

		static public bool HandleCondition(string variableName, string parameter) {
		

			if (variableName == HASITEM) {
				return HandleHasItem(parameter);
			}

			if (variableName == "!" + HASITEM) {
				return HandleDoesNotHaveItem(parameter);
			}

			// This should not happen
			Debug.LogError ("Was unable to handle unique variable " + variableName + "this should not happen" +
								"and will have unintended consequences... returning true to prevent critical path block");

			return true;

		}

		// Handle unique variables

		/// <summary>
		/// Handles the unique hasitem variable name.
		/// </summary>
		/// <returns><c>true</c>, if has item was handled, <c>false</c> otherwise.</returns>
		/// <param name="parameter">Parameter.</param>

		static bool HandleHasItem(string parameter) {
		
			return GameController.PartyManager.ActivePC.Inventory.HasItem(parameter);
		
		}

		/// <summary>
		/// Handles the unqique !HasItem variable name
		/// </summary>
		/// <returns><c>true</c>, if does not have item was handled, <c>false</c> otherwise.</returns>
		/// <param name="parameter">Parameter.</param>

		static bool HandleDoesNotHaveItem(string parameter) {
		
			return !GameController.PartyManager.ActivePC.Inventory.HasItem(parameter);
		}

		#endregion
	}
}