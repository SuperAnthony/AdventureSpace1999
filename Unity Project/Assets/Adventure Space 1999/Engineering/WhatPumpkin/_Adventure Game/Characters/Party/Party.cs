#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - June 1, 2015
#endregion

#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#endregion

namespace WhatPumpkin.Sgrid.Characters {

	/// <summary>
	/// Party. A party system for characters.
	/// </summary>

	public class Party /*: Keyed, IReceiver*/  {
		/*
		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>

		public override string Key { get { return _key; } }

		/// <summary>
		/// The party memebers.
		/// </summary>

		List<PlayerCharacter> _partyMembers;

		/// <summary>
		/// Which party member is the leader?
		/// </summary>

		PlayerCharacter _leader;

		/// <summary>
		/// If a character is following the active character
		/// </summary>

		PlayerCharacter _follower;

		public Party() {
		
			// Assign the party members to this party
			foreach (PlayerCharacter partyMember in _partyMembers) {
				partyMember.Party = this;
			}
	
		}

		/// <summary>
		/// Determines whether this instance is in party the specified playerCharacter.
		/// </summary>
		/// <returns><c>true</c> if this instance is in party the specified playerCharacter; otherwise, <c>false</c>.</returns>
		/// <param name="playerCharacter">Player character.</param>

		public bool IsInParty(PlayerCharacter playerCharacter) {

			foreach (PlayerCharacter partyMember in _partyMembers) {
			
				if(playerCharacter == partyMember) {

					return true;

				}
			}

			return false;

		}
	
		/// <summary>
		/// Is the party member following the leaeder
		/// </summary>
		/// <param name="partyMember">Party member.</param>

		public bool IsFollower(PlayerCharacter partyMember) {
		
			return partyMember == _follower;
		
		}

		/// <summary>
		/// Sets the leader of the party.
		/// </summary>
		/// <param name="partyMember">Party member.</param>
		/// <param name="forceInParty">If set to <c>true</c> force in party.</param>

		public void SetLeader(PlayerCharacter partyMember, bool forceInParty = true) {
		
			bool isInParty = IsInParty (partyMember);

			// Make sure the character is added to the party or is being forced in, if not then exit		
			if (!isInParty && !forceInParty) {return;}

			// If not in the party then add
			if (!isInParty) {Add(partyMember);}

			// Set the leader
			_leader = partyMember;
		
		}

		public void SetFollower(PlayerCharacter partyMember, bool forceInParty = true) {
			
			bool isInParty = IsInParty (partyMember);
			
			// Make sure the character is added to the party or is being forced in, if not then exit		
			if (!isInParty && !forceInParty) {return;}
			
			// If not in the party then add
			if (!isInParty) {Add(partyMember);}
			
			// Set the leader
			_follower = partyMember;
			
		}

		/// <summary>
		/// And to party
		/// </summary>

		public void Add(PlayerCharacter partyMember) {

			if (partyMember == null) {
				return;
			}
		
			// Make sure the character is not in the party before adding
			if (!IsInParty (partyMember)) {
			
				_partyMembers.Add(partyMember);
			
			}

		}

		/// <summary>
		/// Remove from party.
		/// </summary>

		public void Remove(PlayerCharacter partyMember) {

			if (partyMember == null) {
				return;
			}


			// Make sure the character is in the party before removing
			if (IsInParty (partyMember)) {
				
				_partyMembers.Remove(partyMember);
				
			}

		}


		#region implement IReceiver

		public void Add (string item) {

			Add (GameController.SceneManager.FindKeyedObject<PlayerCharacter> (item));
		

		}

		public void Remove(string item) {
		
			Remove (GameController.SceneManager.FindKeyedObject<PlayerCharacter> (item));
		}


		#endregion
		*/

	}
}
