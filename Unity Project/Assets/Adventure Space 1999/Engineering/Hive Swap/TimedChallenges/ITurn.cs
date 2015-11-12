#region Copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 4, 2015
#endregion


namespace WhatPumpkin.Hiveswap.TimedChallenges {

	/// <summary>
	/// ITurn. Interface for turns in a timed challenge.
	/// </summary>

	public interface ITurn {

		/// <summary>
		/// Happens when the time challenge starts.
		/// </summary>
		/// <param name="timeChallenge">Time challenge.</param>

		void TimeChallengeStart(ITimedChallenge timeChallenge);
	}
}