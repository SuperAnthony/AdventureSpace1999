#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 10, 2015
#endregion 

#region using
using System;
#endregion

namespace WhatPumpkin {
	/// <summary>
	/// Interface for types that can play, stop, pause and resume. 
	/// </summary>

	public interface IPerform  {

		/// <summary>
		/// Gets or sets the play order. The play action itself will use this to keep track of which object is getting played when. 
		/// This is being used as a way or IDing the object
		/// </summary>
		/// <value>The play order.</value>

		int PlayOrder { get; set;}

		/// <summary>
		/// Occurs when finished playing.
		/// </summary>

		event EventHandler FinishedPlaying;

		/// <summary>
		/// Play this instance.
		/// </summary>

		void Play();

		/// <summary>
		/// Stop this instance.
		/// </summary>

		void Stop();

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
