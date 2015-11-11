#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Created: December 2014
#endregion 

#region using
using UnityEngine;
using System.Collections;
using WhatPumpkin.Dialogue;
using PixelCrushers.DialogueSystem;
#endregion

namespace WhatPumpkin.Actions {

	public class Talk : ActionType {

		/// <summary>
		/// The name.
		/// </summary>
		
		public const string NAME = "Talk"; 

	
		#region methods

		/// <summary>
		/// Activate this instance.
		/// </summary>
		
		public Talk() {
			_name = NAME;
		}
		
		public override IEnumerator BeginAction(params object[] parameters) {



			// Make sure there is a parameter
			if (parameters.Length > 0) {


				GameController.ConversationController.Talk(parameters[0].ToString()); 
					
			}
			else {
				Debug.LogError("No conversation is defined for the talk command.");
			}



			// Do not complete the action until conversation is no longer active

			while (DialogueManager.IsConversationActive) {
			
				yield return false;
			}

			// Conversation is complete
			// End the action
			EndAction ();
			yield break;
		}

		#endregion
	
	}
}
