#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 18, 2015
#endregion 

#region using
using UnityEngine;

#endregion

// TODO: Get rid of this

namespace WhatPumpkin.CutScenes {

	/// <summary>
	/// Quick script to deactivate something whenever a cutscene is playing
	/// </summary>

	// TODO: Eventually I'd like all these start and stop cutscene events to live in the cutscene manager using the EventHandler delegate

	public class DeactivateDuringCS : MonoBehaviour {


		void Awake() {
		
			CutScene.StartCutscene += HandleStartCutscene;

		}

		/// <summary>
		/// Handles the start cutscene.
		/// </summary>

		void HandleStartCutscene ()
		{
			this.GetComponent<ISwitchable> ().Deactivate ();
			CutScene.EndCutscene += HandleEndCutscene;
		}

		void HandleEndCutscene ()
		{
			CutScene.EndCutscene -= HandleEndCutscene;	
			this.GetComponent<ISwitchable> ().Activate();
		}


		void OnDestroy() {
		
			try{
				CutScene.StartCutscene -= HandleStartCutscene;
			}
			catch{
			
				Debug.LogWarning("Attempted to unregister Start Cutscene event but could not");
			
			}


		}


	}
}
