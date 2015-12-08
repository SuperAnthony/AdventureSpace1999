#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - August 6th, 2015
#endregion 

#region using
using System;
using UnityEngine;
using System.Collections;

using WhatPumpkin.Sgrid;
#endregion

// TODO: Remove, I don't need this


namespace WhatPumpkin.Actions.Sequences {

	// TODO: Paramaterized IPerform (Apparently I did this?)

	/// <summary>
	/// Performer. Keyed Entity that performs an action sequence
	/// Initial purprose is to use this for strife
	/// </summary>

	public class PerformerSequence : MonoBehaviour, IKeyed, IPerform {

	
		enum PerformerBehaviour {PlaysFirstPossible};


		#region implement IKeyed

		/// <summary>
		/// The key.
		/// </summary>

		[SerializeField] string _key;

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>

		public string Key { get { return _key; } }

		/// <summary>
		/// Occurs when object is destroyed.
		/// </summary>

		public event EventHandler Destroyed;

		/// <summary>
		/// The _currently playing.
		/// </summary>

		ActionSequence _currentlyPlaying;

		/// <summary>
		/// Raises the destroyed event.
		/// </summary>

		void OnDestroyed() {
			if (Destroyed != null) {Destroyed(this, new EventArgs());}
		}

		/// <summary>
		/// Awake this instance.
		/// </summary>

		void Awake() {
		
			// Add this key to the list of keys
			Keyed.AddKey (this);
		
		}


		#endregion

		#region performer fields

		/// <summary>
		/// The action sequences.
		/// </summary>

		[SerializeField] ActionSequence [] _actionSequences; 

		#endregion



		#region implement IPeform

		///// <summary>
		/// Gets or sets the play order. The play action itself will use this to keep track of which object is getting played when. 
		/// This is being used as a way or IDing the object
		/// </summary>
		/// <value>The play order.</value>

		
		public int PlayOrder { get; set;}


		/// <summary>
		/// Occurs when finished playing.
		/// </summary>
		
		public event EventHandler FinishedPlaying;


		/// <summary>
		/// Automatically attempts to play the first action sequence if there is one
		/// </summary>

		public void Play() {

			Debug.Log ("Play Performer: " + Key);

			foreach(ActionSequence actionSequence in _actionSequences) {

				if(actionSequence != null && actionSequence.ConditionsAreMet()) {
				

					actionSequence.FinishedPlaying += HandleFinishedPlaying;
					actionSequence.Play();
					_currentlyPlaying = actionSequence;
					return;
				}
			}


			// Nothing playable found so we've finisehd playing
			HandleFinishedPlaying (this, null);
		
		}

		public void Stop() {
		
			// TODO:

		}

		/// <summary>
		/// Handles the finished playing event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>

		void HandleFinishedPlaying (object sender, System.EventArgs e)
		{

			Debug.Log ("Performer Seqeuence Finished Playing");

			if (FinishedPlaying != null) {
			
				// Raise the finished playing event
				FinishedPlaying(this, null);
				// Remove event
				_currentlyPlaying.FinishedPlaying -= HandleFinishedPlaying;
			}
		}

		#endregion


	}
}
