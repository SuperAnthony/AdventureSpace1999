#region Copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 1, 2015
#endregion

#region using
using UnityEngine;
using System.Collections;

using WhatPumpkin;
using WhatPumpkin.Actions.Sequences;

#endregion

namespace WhatPumpkin.Hiveswap.TimedChallenges {

	/// <summary>
	/// A timed challenge. When an action sequence occurs the timed challenge increases by one.
	/// </summary>

	[System.Serializable]

	public class TimedChallenge : Keyed, IKeyed, ISwitchable, ITimedChallenge {

		#region events

		/// <summary>
		/// Occurs when turn starts.
		/// </summary>

		public event TurnOccured TurnStart; 

		/// <summary>
		/// Occurs when the time challenge has ended.
		/// </summary>

		public event System.Action TimeChallengeEnd;

		#endregion

		#region fields

		/// <summary>
		/// The key of this timed challenge.
		/// </summary>

		//[SerializeField] string _key; 

		/// <summary>
		/// The _turns.
		/// </summary>

		[SerializeField] Turn [] _turns;

		/// <summary>
		/// The number of turns the player has taken so far in this timed challenge. The first turn is 0.
		/// </summary>
		
		int _currentTurn = 0;

		/// <summary>
		/// Is this challenge currently active
		/// </summary>

		bool _isActive = false;

		#endregion

		#region properties

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>

		public override string Key { get { return _key;} }

		/// <summary>
		/// Gets the turns.
		/// </summary>
		/// <value>The turns.</value>

		public Turn [] Turns { get { return _turns; } }

		/// <summary>
		/// Gets a value indicating whether the timed challenge is
		/// </summary>
		/// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>

		public bool IsActive { get { return _isActive; } }

		#endregion

		#region methods

		/// <summary>
		/// Constructor: Initializes a new instance of the <see cref="WhatPumpkin.Hiveswap.TimedChallenges.TimedChallenge"/> class.
		/// </summary>

		TimedChallenge() {
		

		}

		void HandleAddTurn (object sender, System.EventArgs e)
		{
			Debug.Log (" Handle Add Turn ");
			AddTurn ();
		}

		/// <summary>
		/// Activate this instance.
		/// </summary>
		
		public void Activate() {
		
			Debug.Log ("Timed Challenge Activated");


			// Make sure the current turn is 0
			_currentTurn = 0;

			// Check to see if the controller is attempting to add a turn
			TimedChallengeController.Instance.AddTurn += HandleAddTurn;


			_isActive = true;

			//  Subscribe the action sequence played (or attempted?) event
			//  A turn will happen whenever an action sequence is played
			ActionSequence.ActionSequencePlayed += HandleActionSequencePlayed;

			// Initialize the turns
			if (_turns != null && _turns.Length > 0) {
			
				foreach (ITurn turn in _turns) {
					turn.TimeChallengeStart(this);
				}

			}
			//
		}

		void HandleActionSequencePlayed (ActionSequence actionSequence)
		{

			if (!IsTurnActionSequence (actionSequence.Key)) {
				// Turn should only occur if the action sequence played is not one that relates to an actual turn
				OnTurn();

			}

		}

		bool IsTurnActionSequence(string actionSequenceKey) {
				
			foreach (Turn turn in _turns) {

				if(turn.ActionSequenceKey == actionSequenceKey) {
					return true;
				}

			}
		
			return false;
		}



		/// <summary>
		/// Deactivate this instance. Designer's can use deactivate when player "succeeds"
		/// </summary>

		public void Deactivate() {
		

			TimedChallengeController.Instance.AddTurn -= HandleAddTurn;

			_isActive = false;

			//  Unsubscribe the action sequence played (or attempted?) event
			ActionSequence.ActionSequencePlayed -= HandleActionSequencePlayed;
		


		}

		/// <summary>
		/// Adds a turn to the player.
		/// </summary>

		void AddTurn() {
			Debug.Log ("Add Turn");
			_currentTurn++;

		}

		/// <summary>
		/// Raises the turn event.
		/// </summary>

		void OnTurn() {

			Debug.Log ("On Turn");

			AddTurn ();

			if (TurnStart != null) {
				TurnStart.Invoke (_currentTurn);
			}
		
		}

		/// <summary>
		/// Raises the time challenge end event.
		/// </summary>

		void OnEnd() {
		
			if (TimeChallengeEnd != null) {TimeChallengeEnd.Invoke(); }
		
		}
	

		#endregion

#if UNITY_EDITOR
		
		/// <summary>
		/// Sets the properties.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="turnSequenceKeys">Turn sequence keys.</param>
		
		public void SetProperties(string key, int selectedTurnIndex, int turnNumber, string turnSequence) {

			RenameKey (key);

			_turns [selectedTurnIndex].SetProperties (turnNumber, turnSequence);
				 	
		}

		// Add a turn

		public void AddToTurnCollection() {_turns = DataUtilities.AddArrayElement<Turn> (_turns, new Turn ());}

		// Remove a turn

		public void RemoveTurn(Turn turn) {_turns = DataUtilities.RemoveArrayElement (_turns, turn);}
		
#endif

	}
}
