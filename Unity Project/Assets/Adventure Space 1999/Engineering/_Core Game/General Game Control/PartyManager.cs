#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - June 1, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using WhatPumpkin.Sgrid.Characters;
#endregion

namespace WhatPumpkin {

	/// <summary>
	/// Party manager.
	/// </summary>


	public class PartyManager : MonoBehaviour, IActivator {

		#region fields

		/// <summary>
		/// The singleton instance of this object.
		/// </summary>
	
		PartyManager _instance;

		/// <summary>
		/// The active character on start if the _active character is null.
		/// </summary>
		
		[SerializeField] PlayerCharacter _activeCharacter; // TODO: Move this to a party manager 

		/// <summary>
		/// The list of party members (PCs).
		/// </summary>

		List<PlayerCharacter> _partyMembers = new List<PlayerCharacter>();

		/// <summary>
		/// The _player characters.
		/// </summary>

		List<PlayerCharacter> _playerCharacters = new List<PlayerCharacter> ();
	
		#endregion

		#region properties

		public PartyManager Instance { get { return _instance; } }

		/// <summary>
		/// Gets the active character.
		/// </summary>
		/// <value>The active character.</value>

		public PlayerCharacter ActivePC { get { return PlayerCharacter.Active; } }

		#endregion

		#region methods

		void Awake() {

//			Debug.Log ("Party Manager Awake");

			if (_instance == null) {
				_instance = this;
			}

			Activate(_activeCharacter);

			// Register scene load

			GameController.SceneManager.SceneLoadEnd += HandleSceneLoadEnd;


			// Register target move 	
			// TODO: This does not seem to be working and I have no Idea why - had to do a work around - it is pissing me off
			//GameController.InputManager.TargetMoved += HandleTargetMoved;

		
		}
	

		void HandleSceneLoadEnd (object sender, System.EventArgs e)
		{
			PlayerCharacter.UpdateActiveCharacterScene ();


		}

		void OnDestroy() {

			GameController.SceneManager.SceneLoadEnd -= HandleSceneLoadEnd;

		}

		public void Activate(PlayerCharacter pc) {
		
			if (pc != null) {
								pc.Activate ();
						}
		
		}

		/// <summary>
		/// Activate the specified key.
		/// </summary>
		/// <param name="key">Key.</param>

		public void Activate(string key) {
		

			PlayerCharacter activatingPC = WhatPumpkin.Keyed.FindInCollection<PlayerCharacter> (key, _playerCharacters.ToArray());

			if (activatingPC != null) {
			
				activatingPC.Activate();

			}
			else {

				Debug.LogError("Could not activate the PC '" + activatingPC.Key + ".' Was the correct key used?");
			
			}
		}

		

		PlayerCharacter GetPartyMember(string key) {
		
			foreach (PlayerCharacter partyMember in _partyMembers) {
			
				if(partyMember.Key == key) {
					return partyMember;
				}
			
			}


			return null;

		}

		/// <summary>
		/// Adds the PC to list of all player characters.
		/// </summary>
		/// <param name="item">Item.</param>

		public void AddPCToList(PlayerCharacter item) {

			_playerCharacters.Add (item);
			
		}

		#endregion



		[ExecuteInEditMode]

		public void SetPlayerCharacter(PlayerCharacter pc) {
		
			_activeCharacter = pc;

		}


	

	}
}
