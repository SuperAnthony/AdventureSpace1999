#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 15, 2015
#endregion 

#region using
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using WhatPumpkin.Sgrid;
using WhatPumpkin.ScriptingLanguage;
using PixelCrushers.DialogueSystem;
#endregion

namespace WhatPumpkin.Actions.Sequences {

	#region attributes
	[System.Serializable]
	#endregion

	/// <summary>
	/// Random Action sequence. 
	/// Stores a list of actions, a random one will get played when the play method is invoked
	/// </summary>

	public class RandomActionSequence : ActionSequence   {
	
		#region IPerform

		/// <summary>
		/// Occurs when finished playing.
		/// </summary>

		//public event EventHandler FinishedPlaying;

		/// <summary>
		/// Play this instance.
		/// </summary>
		/*
		public override void Play() {

			// Make sure there is an action controller otherwise log a warning and return
			if(ActionController.Instance == null) {Debug.LogWarning("Could not play random action sequence because an action controller was not found");return;}

			// Get a random action
			if(_actions.Count > 0) {
	
				int randomAction = UnityEngine.Random.Range (0, _actions.Count);

				ActionType actionType = ActionTypeCollection.Instance[_actions[randomAction].ActionType];



				if(actionType != null) {
					ActionController.Instance.PerformAction(actionType, _actions[randomAction].Parameters);
				}
				else {
					Debug.LogWarning("Action type returned null on the RandomActionSequence keyed '" + this.Key);
				}

			}
			else {
				Debug.LogWarning("RandomActionSequence did not play. No actions were found on the ActionSequence keyed '" + this.Key);
			}


		
		}*/

		/// <summary>
		/// Stop this instance.
		/// </summary>
		/*
		public override void Stop() {
		
			// TODO:

		}

		/// <summary>
		/// Pause this instance.
		/// </summary>

		public override void Pause() {
		
			// TODO:

		}

		/// <summary>
		/// Resume this instance.
		/// </summary>

		public override void Resume() {
		
			// TODO:

		}*/


		#endregion

	}
}
