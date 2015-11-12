#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - February 18, 2014
#endregion 

#region using
using UnityEngine;
using System.Collections;
using WhatPumpkin.Entities;
#endregion

namespace WhatPumpkin.Hiveswap.Abilities {

	/// <summary>
	/// Character abilities. Attach this component to a character that has abilities.
	/// </summary>

	public class CharacterAbilities : MonoBehaviour {

		[SerializeField] Ability [] _abilities;

		#region methods

		void Awake() {
		
			GameController.SceneManager.SceneLoadEnd += HandleSceneLoadEnd;

		}

		/// <summary>
		/// Handles the scene load end.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>

		void HandleSceneLoadEnd (object sender, System.EventArgs e)
		{
			// TODO: Temp quick fix
			// Add all the abiliities back to the data
			Keyed.AddKeys (_abilities);
		}

		/// <summary>
		/// Determines whether this instance has ability of the specified key.
		/// </summary>
		/// <returns><c>true</c> if this instance has ability the specified key; otherwise, <c>false</c>.</returns>
		/// <param name="key">Key.</param>

		public bool HasAbility(string key) {
		
			// Check for null reference 
			if (_abilities == null || _abilities.Length < 1) {return false;}


			// If key is found return true
			foreach (Ability ability in _abilities) {
			
				if(ability.Key == key) {
					return true;
				}
			
			}

			// No key was found so return false
			return false;

		}

		/// <summary>
		/// Gets an ability by name.
		/// </summary>
		/// <returns>The ability.</returns>
		/// <param name="key">Key.</param>

		public Ability GetAbility(string key) {
		
			return Entity.FindObjectByKey<Ability> (key, _abilities);

		}

		/// <summary>
		/// Open an ability's verb coin from the abilities array specified by the ability key.
		/// </summary>
		/// <param name="abilityKey">Ability key.</param>
		/// <param name="sender">The transform that is attempting this performance, persumably the UI element.</param>

		public void OpenVerbCoin(string abilityKey, Transform sender) {

			GetAbility (abilityKey).OpenVerbCoin (sender);
		
		
		}

		#endregion
		
	}
}
