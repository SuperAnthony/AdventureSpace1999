#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - August 13, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;
#endregion

namespace WhatPumpkin {

	/// <summary>
	/// Hide on a particular state. Quick and dirty script. Not reccomended for excessive use.
	/// </summary>

	public class HideOnState : MonoBehaviour {


		/// <summary>
		/// Hide on closeup?
		/// </summary>

		[SerializeField] bool _hideOnCloseup;

		/// <summary>
		/// The _disable on cutscene.
		/// </summary>

		[SerializeField] bool _hideOnCutscene;

		/// <summary>
		/// Enable on explore?
		/// </summary>


		[SerializeField] bool _enableOnExplore;

		void Start() {
		
			GameController.GameStateMachine.StateChanged += HandleStateChanged;
		
		}

		/// <summary>
		/// Handles the state changed.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>

		void HandleStateChanged (object sender, System.EventArgs e)
		{
				
		}

		void OnDestroy() {
		
			GameController.GameStateMachine.StateChanged -= HandleStateChanged;

		}

		/// <summary>
		/// Hide this instance.
		/// </summary>

		void Hide() {
		
		}

		/// <summary>
		/// Show this instance.
		/// </summary>


		void Show() {
		}


	}
}