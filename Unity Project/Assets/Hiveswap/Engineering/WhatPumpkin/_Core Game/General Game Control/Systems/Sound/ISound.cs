#region Copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - April 16, 2015
#endregion

#region using
using UnityEngine;
using System.Collections;
#endregion

namespace WhatPumpkin.Sound {

	/// <summary>
	/// A simple sound.
	/// </summary>

	public interface ISound {

		void Play();
		void Stop();
		
	}
}
