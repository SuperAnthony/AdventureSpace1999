#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created -  May 5, 2015
#endregion 

#region using
using UnityEngine;
using WhatPumpkin.Actions.Sequences;
#endregion


namespace WhatPumpkin.Screens {
	/// <summary>
	/// Message Options that have an action sequence associated with it.
	/// </summary>

	[System.Serializable]
	
	public class ASOption : Keyed, IOption {

		#region fields



		/// <summary>
		/// The text that gets displayed by this option (we will use a key that gets handled by the message manager).
		/// </summary>

		[SerializeField] string _text;

		/// <summary>
		/// The _action sequence that gets played when this option is selected.
		/// </summary>

		[SerializeField] ActionSequence _actionSequence;

		#endregion

		#region properties

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>

		public override string Key { get { return _key; } }

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <value>The text.</value>

		public string Text { get { return _text; } }


		#endregion

		#region methdos

		public void Select() {

			// Play the assocaited action sequence
			_actionSequence.Play ();

		}

		#endregion

	}
}
