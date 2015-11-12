#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 4, 2015
#endregion 

// TODO: This may move to the "Start" or "Activate" command (these can be used interchangably in some cases.


#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using WhatPumpkin.Hiveswap.TimedChallenges;
#endregion

namespace WhatPumpkin.Actions {

	/// <summary>
	/// Allows the developer to start a timed challenge.
	/// </summary>

	public class StartTimedChallenge : ActionType {

		/// <summary>
		/// The name.
		/// </summary>
		
		public const string NAME = "StartTC"; 


		#region methods

		public StartTimedChallenge() {
			_name = NAME;
		}

		public override IEnumerator BeginAction(params object[] parameters) {

			// Make sure there is a parameter
			if (parameters.Length > 0) {

				string key = parameters[0].ToString();
				TimedChallengeController.Instance.ActivateTimedChallenge(key);

			}

			
			// End the action
			EndAction ();
			yield break;
		}


		#endregion

	}
}
