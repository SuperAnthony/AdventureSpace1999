#region Copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 1, 2015
#endregion

using System;

namespace WhatPumpkin.Hiveswap.TimedChallenges {

	public interface ITimedChallengeController {

		/// <summary>
		/// Activates the timed challenge.
		/// </summary>
		/// <param name="key">Key.</param>

		void ActivateTimedChallenge(string key);

		
		/// <summary>
		/// Occurs when add turn.
		/// </summary>
		
		event EventHandler AddTurn;

	}
}