#region Copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - April 16, 2015
#endregion

namespace WhatPumpkin.Sgrid.Characters {

	/// <summary>
	/// The emotional state of a character
	/// </summary>

	public interface IFollow  {

		/// <summary>
		/// Gets the emotion.
		/// </summary>
		/// <value>The emotion.</value>

		string Emotion { get; }

		/// <summary>
		/// Changes the emotional state.
		/// </summary>
		/// <returns>The state.</returns>
		/// <param name="state">State.</param>

		string ChangeState(string state);

	}
}
