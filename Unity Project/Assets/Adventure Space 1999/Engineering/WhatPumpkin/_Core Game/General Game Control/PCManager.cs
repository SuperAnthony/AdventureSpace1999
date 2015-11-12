using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using WhatPumpkin.Entities;
using WhatPumpkin.Sgrid.Characters;
using WhatPumpkin.Sgrid.Markers;
using WhatPumpkin.Hiveswap.Abilities;

namespace WhatPumpkin {

	/// <summary>
	/// PC Manager - Manage Player Characters
	/// </summary>

	public class PCManager : MonoBehaviour {

		#region static fields

		static PCManager _instance;

		#endregion

		#region fields

		/// <summary>
		/// The list of player characters.
		/// </summary>

		[SerializeField] List<PlayerCharacter> _playerCharacters = new List<PlayerCharacter>();

		/// <summary>
		/// The active character.
		/// </summary>

		[SerializeField] PlayerCharacter _activeCharacter;

		#endregion


		#region properties

		public PlayerCharacter ActivePC { get { return PlayerCharacter.Active; } }

		#endregion

		#region methods
		void Start () {

			if (_instance == null) {

				// Set the singleton instance
				_instance = this;

		
			}


		}

		public void InitActivePC() {
			// Get each character in the game
			foreach (PlayerCharacter p in GameObject.FindObjectsOfType<PlayerCharacter>()) {
				_playerCharacters.Add (p);
				// Make sure the characters are not destroyed on load
				DontDestroyOnLoad (p); // Note: I think this is already handled by the required scene objects
			}
			
			// If the active player character is null then set the active character
			if (PlayerCharacter.Active == null && _activeCharacter != null) {
				//PlayerCharacter.Active = _activeCharacter;
				_activeCharacter.Activate();
			} else {
				Debug.LogError ("No active character is assigned in the PCManager will try to assign one automatically");
				GameObject.FindObjectOfType<PlayerCharacter>().Activate();
			}
		}

		void Awake() {
		
		

		}
		
		/// <summary>
		/// Switchs the character.
		/// </summary>
		/// <param name="playerCharacter">Player character.</param>
		/*
		public void SwitchCharacter(PlayerCharacter playerCharacter) {
			
			PlayerCharacter.Active = playerCharacter;
			
			// If the new character is in a different room from the current room then load it
			if (playerCharacter.Room != GameController.SceneManager.CurrentScene) {
				GameController.SceneManager.LoadScene(playerCharacter.Room);
			}
			
		}*/


		/// <summary>
		/// Subscribes to events.
		/// </summary>

		void SubscribeToEvents() {

			// Subscribe to the pc created event
			PlayerCharacter.PCCreated += OnPCCreated;
			GameController.Instance.ApplicationClose += OnApplicationClose;

		}

		/// <summary>
		/// Unsubscribes from events.
		/// </summary>

		void UnsubscribeFromEvents() {

			PlayerCharacter.PCCreated -= OnPCCreated;
		
		}

		/// <summary>
		/// Raises the application close event.
		/// </summary>

		public void OnApplicationClose() {
			UnsubscribeFromEvents ();
		}

		/// <summary>
		/// Activates the ability of a specified key.
		/// </summary>
		/// <param name="key">Key.</param>
		/*
		public void ActivateAbility(string key) {

			// Get the ability of the name
			Ability ability = Ability.FindAbility (key);

			// Activate the ability if it's currently available
			if (ability.IsActive) {
						// Open a verb coin panel based on an ability
						GameController.ActionControl.OpenVerbCoinPanel (ability.VerbActionSequences, this.transform);
				}

		}*/

		/// <summary>
		/// Adds the PC to collection.
		/// </summary>
		/// <param name="pc">Pc.</param>

		void AddPCToCollection(PlayerCharacter pc) {
			if (pc != null) {
				_playerCharacters.Add(pc);
			}
		}



		/// <summary>
		/// Raises the PC created event.
		/// </summary>
		/// <param name="pc">Pc.</param>
		/// <param name="e">E.</param>

		void OnPCCreated(object pc, EventArgs e) {
		
			PlayerCharacter playerCharacter = (PlayerCharacter)pc;
		
			if (playerCharacter != null) {
								AddPCToCollection (playerCharacter);
						}
		}

		/*
		Ability GetPCAbility(string pcKey, string abilityKey ) {
		
			PlayerCharacter pc = GetPC (pcKey);

			CharacterAbilities pcAbilitiesComponent = pc.GetComponent<CharacterAbilities> ();

			if(pcAbilitiesComponent != null) {

				Ability ability = pcAbilitiesComponent.GetAbility(abilityKey);

				return ability;
			}

			return null;
		}*/

		/// <summary>
		/// Gets the Player Character of a given key.
		/// </summary>
		/// <returns>The Player Character.</returns>
		/// <param name="key">Key.</param>

		PlayerCharacter GetPC(string key) {
			foreach (PlayerCharacter pc in _playerCharacters) {
			
				if(pc.Key == key) {
					return pc;
				}

			}

			return null;
		}

		#endregion
	}
}
