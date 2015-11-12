#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - January 27, 2015
#endregion 

using System;

namespace WhatPumpkin {

	/// <summary>
	/// IKeyed interface. All keyed objects can implement this interface.
 	/// </summary>

	public interface IKeyed  {

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>

		string Key { get; }

		/// <summary>
		/// Occurs when object is destroyed.
		/// </summary>

		event EventHandler Destroyed;
	}
}