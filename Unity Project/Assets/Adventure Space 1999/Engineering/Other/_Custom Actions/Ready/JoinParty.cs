#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - June 2, 2015
#endregion 

#region using
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using WhatPumpkin.Sgrid.Characters;
#endregion

namespace WhatPumpkin.Actions {

	public class JoinParty : ActionType {

		#region constants

		/// <summary>
		/// The name.
		/// </summary>
		
		public const string NAME = "JoinParty"; 

		#endregion

		#region argument receiver
		
		Type [] _validTypes = new Type[] {typeof(IPartyMember)};
		
		
		protected override Type[] ValidTypes {
			get {
				return _validTypes;
			}
		}
		
		List<IKeyed> _validArguments = new List<IKeyed> ();
		
		
		protected override List<IKeyed> ValidArguments {
			get {
				return _validArguments;
			}
		}

		
		#endregion

		#region methods

		/// <summary>
		/// Add party member to the party
		/// </summary>

		public JoinParty() {
			_name = NAME;

		}

		public override IEnumerator BeginAction(params object[] parameters) {

			// Make sure there is a parameter
			if (parameters.Length > 0) {

				IPartyMember partyMember = GameController.SceneManager.FindKeyedObject<IPartyMember>(parameters[0].ToString());



				if(partyMember != null) {partyMember.JoinParty();}
				else {
					Debug.LogError(parameters[0].ToString() + " Object not found in scene. Please check for correct spellings and that the correct game object is in the scene named properly.");
				}

			}
			else {
				Debug.LogError("No target is defined for the Activate command.");
			}

			EndAction ();
			yield break;

		}

		#endregion

	}
}