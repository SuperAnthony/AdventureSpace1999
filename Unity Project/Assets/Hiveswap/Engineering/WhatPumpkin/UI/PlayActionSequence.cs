#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - July 24, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;
using WhatPumpkin.Actions.Sequences;
#endregion

// TODO: Remove  From UI Namespace? | Create entity version

namespace WhatPumpkin.UI {

	/// <summary>
	/// A mono behaviour that holds an action sequence to be played. This was primarily created to attach to buttons.
	/// </summary>


	public class PlayActionSequence : MonoBehaviour, IActivatable {

		/// <summary>
		/// The action sequence that will be played when activated.
		/// </summary>

		[SerializeField] ActionSequence _actionSequence;

		/// <summary>
		/// Activate this instance.
		/// </summary>

		public void Activate() {
		
			_actionSequence.Play ();

		}


	}
}