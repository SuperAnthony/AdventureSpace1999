#region Copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 1, 2015
#endregion

#region using
using UnityEngine;
using System;
using System.Collections;
#endregion

namespace WhatPumpkin.Hiveswap.TimedChallenges {

	/// <summary>
	/// Timed challenge controller.
	/// </summary>

	public class TimedChallengeController : MonoBehaviour, ITimedChallengeController {

		#region static fields

		/// <summary>
		/// The singleton instance of this timed challenge controller
		/// </summary>
		/// <value>The instance.</value>

		static public ITimedChallengeController Instance { get; private set; }

		#endregion

		#region events

		/// <summary>
		/// Occurs when add turn.
		/// </summary>

		public event EventHandler AddTurn;

		#endregion

		/// <summary>
		/// The collection of timed challenges.
		/// </summary>

		//TimedChallenge [] _timedChallenges;

		/// <summary>
		/// Occurs when this instance starts
		/// </summary>

		void Start () {
		
			Init ();
		}

		/// <summary>
		/// Init this instance.
		/// </summary>

		internal void Init() {

			if (Instance == null) {
				Instance = this;
			}

		}


		/// <summary>
		/// Raises the add turn event.
		/// </summary>

		public void OnAddTurn() {
		
			if (AddTurn != null) {
			
				AddTurn(this, null);
			}

		}

		/// <summary>
		/// Activates the timed challenge of a given key.
		/// </summary>
		/// <returns>The timed challenge.</returns>
		/// <param name="key">Key.</param>

		public void ActivateTimedChallenge(string key) {

			try {
				GameController.SceneManager.FindKeyedObject<ITimedChallenge>(key).Activate();
			}
			catch (System.NullReferenceException e){
				Debug.LogWarning("Could not locate the timed challenge '" + key + "'"); 
			}


			/*
			foreach (TimedChallenge timedChallenge in _timedChallenges) {
				if(timedChallenge.Key == key) {
					timedChallenge.Activate();
					return;
				}
			}*/
		}

	}
}