#region Copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 4, 2015
#endregion


namespace WhatPumpkin.Hiveswap.TimedChallenges {

	/// <summary>
	/// Interface for a timed challenge.
	/// </summary>

	public interface ITimedChallenge : IActivatable {

		/// <summary>
		/// Occurs when turn start.
		/// </summary>

		event TurnOccured TurnStart;

		/// <summary>
		/// Occurs when this time challenge ends.
		/// </summary>

		event System.Action TimeChallengeEnd;

	}
}