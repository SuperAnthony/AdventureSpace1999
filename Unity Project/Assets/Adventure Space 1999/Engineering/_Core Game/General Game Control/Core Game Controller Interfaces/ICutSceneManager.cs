#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - June 15, 2015
#endregion

namespace WhatPumpkin {

	/// <summary>
	/// Interface for managing cut scenes
	/// </summary>

	public interface ICutSceneManager {

		/// <summary>
		/// Starts the cutscene.
		/// </summary>
		/// <param name="cutsceneKey">Cutscene key.</param>
		/// <param name="continuesFromPrevious">If set to <c>true</c> continues from previous.</param>

		void StartCutscene(string cutsceneKey, bool continuesFromPrevious = false);

		/// <summary>
		/// Gets the cut scene.
		/// </summary>
		/// <returns>The cut scene.</returns>
		/// <param name="key">Key.</param>

		ICutScene GetCutScene(string key);

	}
}