#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - April 2, 2015
#endregion 

#region using
using System;
using UnityEngine;
using System.Collections;
#endregion


namespace WhatPumpkin.Actions {

	/// <summary>
	/// IFrequency - for objects which have a frequency such as Once, Always, After first
	/// </summary>

	public interface IFrequency  {

		/// <summary>
		/// Gets the frequency.
		/// </summary>
		/// <value>The frequency.</value>

		string Frequency { get; } 

	}

}
