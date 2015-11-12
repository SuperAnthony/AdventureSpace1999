#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - August 27, 2015
#endregion 

using UnityEngine;
using System.Collections;

using WhatPumpkin.Actions.Sequences;

namespace WhatPumpkin {

	/// <summary>
	/// Triggers an action sequence after a certain amount of time has passed
	/// The time has a range that will be randomly chosen
	/// </summary>

	public class TimeTriggeredSequence : MonoBehaviour, ISwitchable {

		#region fields

		/// <summary>
		/// The min time.
		/// </summary>

		[SerializeField] float _minTime = 0F;

		/// <summary>
		/// The max time.
		/// </summary>

		[SerializeField] float _maxTime = 100F;

		/// <summary>
		/// Is this active
		/// </summary>

		[SerializeField] bool _isActive = true;

		/// <summary>
		/// Can we play this while another action sequence is playing? No, by default
		/// </summary>

//		[SerializeField] bool _dontPlayDuringActionSequence = true; // TODO: Bring this back when ready

		/// <summary>
		/// Do we reset the timer after a sequence is complete?
		/// </summary>

//		[SerializeField] bool _resetTimerAfterAnySequenceComplete = true; // TODO: Bring this back when ready

		/// <summary>
		/// The action sequence to play when the timer is complete.
		/// </summary>

		[SerializeField] ActionSequence _actionSequence;

		/// <summary>
		/// The time since sequence played.
		/// </summary>

		float _timeSinceSequencePlayed = 0F;

		/// <summary>
		/// The time until next sequence.
		/// </summary>

		float _timeUntilNextSequence = 0F;

		#endregion

		#region properties

		/// <summary>
		/// Gets a value indicating whether this instance is active.
		/// </summary>
		/// <value>true</value>
		/// <c>false</c>

		public bool IsActive { get { return _isActive; } }

		#endregion

		#region methods

		/// <summary>
		/// Occurs on start.
		/// </summary>

		void Start() {

			Reset ();



		}

		/// <summary>
		/// Occurs on update.
		/// </summary>

		void Update() {
		

			if (_isActive) {
			
				_timeSinceSequencePlayed += Time.deltaTime;
			
				// Check to see if it's time to play the action sequence stored. If so, then play it.
				if(_timeSinceSequencePlayed >= _timeUntilNextSequence) {
				

					_actionSequence.Play();



					Reset ();
				}

			}

		}

		/// <summary>
		/// Gets the time.
		/// </summary>
		/// <returns>The time.</returns>

		float GetTime() {return UnityEngine.Random.Range (_minTime, _maxTime);}


		/// <summary>
		/// Reset this instance.
		/// </summary>

		public void Reset() {
			_timeSinceSequencePlayed = 0F;
			_timeUntilNextSequence = GetTime ();
		
		}

		/// <summary>
		/// Activate this instance.
		/// </summary>

		public void Activate() {

			_isActive = true;
		
		}

		/// <summary>
		/// Deactivate this instance.
		/// </summary>

		public void Deactivate() {

			_isActive = false;
		}

		#endregion

	}
}