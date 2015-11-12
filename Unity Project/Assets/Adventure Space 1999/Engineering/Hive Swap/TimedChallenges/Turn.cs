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

	public delegate void TurnOccured(int turnNumber); 

	/// <summary>
	/// A timed challenge. When an action sequence occurs the timed challenge increases by one.
	/// </summary>

	[System.Serializable]

	public class Turn : ITurn{

		#region fields 

		/// <summary>
		/// The time challenge that this turn is associated with.
		/// </summary>

		ITimedChallenge _timeChallenge;

		/// <summary>
		/// The turn number.
		/// </summary>

		[SerializeField] int _turnNumber = 0; 

		/// <summary>
		/// The action sequence key.
		/// </summary>

		[SerializeField] string _actionSequenceKey;

		/// <summary>
		/// The action sequence.
		/// </summary>

		[SerializeField] ActionSequence _actionSequence;


		#endregion

		#region properties

		/// <summary>
		/// Gets the turn number.
		/// </summary>
		/// <value>The turn number.</value>

		public int TurnNumber { get {return _turnNumber;} }

		/// <summary>
		/// Gets the action sequence key.
		/// </summary>
		/// <value>The action sequence key.</value>

		public string ActionSequenceKey { 
			get{ 

				if(_actionSequenceKey == null) {
					return "";
				}

				return _actionSequenceKey; 
			} 
		}

		#endregion

		#region methods


		/// <summary>
		/// Times the challenge start.
		/// </summary>
		/// <param name="timeChallenge">Time challenge that this turn is associated with.</param>

		public void TimeChallengeStart(ITimedChallenge timeChallenge) {
		
			_timeChallenge = timeChallenge;

			// Subscribe to the turn start event
			_timeChallenge.TurnStart += HandleTurnStart;

			// Subscribe to the time challenge end event
			_timeChallenge.TimeChallengeEnd += HandleTimeChallengeEnd;



		}

		/// <summary>
		/// Handles the turn start.
		/// </summary>
		/// <param name="turnNumber">Turn number.</param>

		void HandleTurnStart (int turnNumber)
		{


//			Debug.Log ("Handle Turn Start: " + turnNumber + " vs. " + _turnNumber);

			// If the turn number matches then play the associated action sequence
			if (this._turnNumber == turnNumber) 
			{
				Play ();
			}

			// Unsubscribe
			//_timeChallenge.TurnStart -= HandleTurnStart;

		}

		/// <summary>
		/// Times the challenge end.
		/// </summary>

		void HandleTimeChallengeEnd() {
		
			// Unsubscribe:

			try {_timeChallenge.TurnStart -= HandleTurnStart;}
			catch {Debug.Log("Could not unsubscribe HandleTurnStart this is likely because it was already unsubscribed when the turn occurred and is no big deal");}

			_timeChallenge.TimeChallengeEnd -= HandleTimeChallengeEnd;

			
		}





		/// <summary>
		/// Play this instance.
		/// </summary>

		void Play() {

			Debug.Log ("Play Turn");
		
			if (_actionSequence != null) {
			
				_actionSequence.Play();

			}
			else if(_actionSequenceKey != null || _actionSequenceKey != "") {
			
				//Debug.Log ("Attempt to play key: " + _actionSequenceKey);
				//Debug.Log ("Attempt to play: " + GameController.SceneManager.FindKeyedObject<IPerform>(_actionSequenceKey));
				GameController.SceneManager.FindKeyedObject<IPerform>(_actionSequenceKey).Play();
			}
		
		}

		#endregion

#if UNITY_EDITOR

		public void SetProperties(int turnNumber, string actionSequenceKey = "") {
		
			_turnNumber = turnNumber;
			_actionSequenceKey = actionSequenceKey;


		
		}

#endif

	}
}
