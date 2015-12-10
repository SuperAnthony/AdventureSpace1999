// Copyright (c) Anthony Paul Albino
using System.Collections.Generic;


namespace WhatPumpkin.Actions.Sequences {

	/// <summary>
	/// Interface for implementing an action seqeunce
	/// </summary>

	public interface IActionSequence  {

		/// <summary>
		/// List of actions associated with this sequence
		/// </summary>

		List<Action> Actions {get;}

		/// <summary>
		/// Adds the action.
		/// </summary>
		/// <param name="action">Action.</param>

		void AddAction(Action action);

		/// <summary>
		/// Removes the action.
		/// </summary>
		/// <param name="action">Action.</param>

		void RemoveAction(Action action);


	}
}