#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 14, 2015
#endregion 

#region using
using UnityEngine;
#endregion

namespace WhatPumpkin.Sgrid {

	/// <summary>
	/// Autofills. Utilities for autofilling data.
	/// </summary>

	public sealed class Autofills {


		static int GetCharacterIndex(string key, char identifier, int numIdentifiers) {

			int characterIndex = 0;
			int identifiersFound = 0;

			foreach(char c in key) {

				if(c == identifier) {
					identifiersFound++;
				}
				characterIndex++;
				
				if(identifiersFound >= numIdentifiers) {
					// exit this loop
					break;
				}
			}

			return characterIndex;

		}

		/// <summary>
		/// Inserts the string after key prefix.
		/// </summary>
		/// <returns>The string after prefix.</returns>
		/// <param name="originalString">Original string.</param>
		/// <param name="insert">Insert.</param>
		/// <param name="identifier">Identifier.</param>
		/// <param name="numIdentifiers">Number identifiers.</param>

		static public string InsertStringAfterKeyPrefix(string key, string insert, char identifier, int numIdentifiers) {
		
			int characterIndex = GetCharacterIndex (key, identifier, numIdentifiers);
			return key.Insert (characterIndex, insert);
		
		} 

		/// <summary>
		/// Finds the component in a game object by prefixed string.
		/// </summary>
		/// <returns>The GO component by prefix.</returns>
		/// <param name="key">Key.</param>
		/// <param name="identifier">Identifier.</param>
		/// <param name="numIdentifiers">Number identifiers.</param>
		/// <typeparam name="ComponentType">The 1st type parameter.</typeparam>

		static public KeyedComponentType FindGOComponentByPrefix<KeyedComponentType>(string key, string keyPrefix, string targetKeyPrefix, char identifier, int numIdentifiers) where KeyedComponentType : Component, IKeyed
		{

			// Get the character index
			int characterIndex = GetCharacterIndex (key, identifier, numIdentifiers);


			string keySubstring = key.Substring(0, characterIndex);
			string targetKeySubstring = keySubstring.Replace(keyPrefix, targetKeyPrefix);
			
			foreach(KeyedComponentType keyedComponent in GameObject.FindObjectsOfType<KeyedComponentType>()) {
				
				if(keyedComponent.Key.StartsWith(targetKeySubstring)) {
					
					return keyedComponent;
					
				}
				
				
			}
		
			// Nothing found
			return null;

		}



		private Autofills() {
		}
	}
}