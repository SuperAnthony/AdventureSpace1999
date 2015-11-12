#region Copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 1, 2015
#endregion

namespace WhatPumpkin.States {

	/// <summary>
	/// I have states. Interface for types that change multiple types of states.
	/// </summary>

	public interface IHaveStates  {

		/// <summary>
		/// Changes the state to.
		/// </summary>
		/// <param name="stateType">State type. Defaults to "" for those with only one state type</param>
		/// <param name="toState">To state of that type.</param>

		void ChangeStateTo(string toState, string stateType = "");

		/// <summary>
		/// Gets the active state of a speified state type
		/// </summary>
		/// <returns>The active state.</returns>
		/// <param name="type">Type of state.</param>

		State GetActiveState(string type);

	}
}
