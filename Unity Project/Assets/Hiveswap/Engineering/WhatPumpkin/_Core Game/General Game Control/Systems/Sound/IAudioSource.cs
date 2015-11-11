#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - September 10, 2015
#endregion 


using UnityEngine;

namespace WhatPumpkin {

	public interface IAudioSource {

		/// <summary>
		/// Play this instance.
		/// </summary>

		void Play();

		/// <summary>
		/// Gets or sets the audio source.
		/// </summary>
		/// <value>The audio source.</value>

		AudioSource AudioSource { get; }

	}
}
