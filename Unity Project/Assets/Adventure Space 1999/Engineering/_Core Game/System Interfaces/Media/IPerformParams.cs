#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 14, 2015
#endregion 

#region using
using System;
#endregion

// Note - I'm not actually crazy about this yet if this only applies to animation then it's more practical to use StartAnim and PlayAnim

namespace WhatPumpkin {
	/// <summary>
	/// IPerformParams differs from IPerform in that play, stop, pause and resume require additional parameters
	/// 
	/// Interface for types that can play, start (begin), stop, pause and resume. 
	/// In general when play completes an action when it has completed playing, start completes its action immediately. 
	/// For instance, an action in an action sequence will wait for an animation to complete it's performance when it was "played", however it will trigger immediately when it is "started" 
	/// Note: Changed start to "begin" due to mono behaviours, however, an action in an action sequence may still refer to start
	/// </summary>

	public interface IPerformParams<TParameterType>  {

		/// <summary>
		/// Occurs when finished playing.
		/// </summary>

		event EventHandler FinishedPlaying;

		/// <summary>
		/// Play this instance.
		/// </summary>

		void Play(TParameterType [] parameters);

		/// <summary>
		/// Start this instance.
		/// </summary>

		//void Begin();

		/// <summary>
		/// Stop this instance.
		/// </summary>

		//void Stop();

		/// <summary>
		/// Pause this instance.
		/// </summary>

		//void Pause(); 

		/// <summary>
		/// Resume this instance.
		/// </summary>

		//void Resume();

	}
}
