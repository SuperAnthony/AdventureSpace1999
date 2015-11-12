#region Copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - April 16, 2015
#endregion

using WhatPumpkin.States;

// TODO: Deprecate

namespace WhatPumpkin.Sgrid.Characters {

	/// <summary>
	/// The emotional state of a character
	/// </summary>

	public interface IEmotional  {

		/// <summary>
		/// Gets the emotion.
		/// </summary>
		/// <value>The emotion.</value>

		State Emotion { get; }

		/// <summary>
		/// Changes the emotional state.
		/// </summary>
		/// <param name="state">State.</param>

		State ChangeState(string state);

	}
}