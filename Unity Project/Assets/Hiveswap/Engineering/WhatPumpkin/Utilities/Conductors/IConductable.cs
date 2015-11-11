#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - April 1, 2014
#endregion 

namespace WhatPumpkin.Conductors {

	/// <summary>
	/// IConductable. Allows the implementing object to be conducted by a conductor.
	/// </summary>

	public interface IConductable {

		/// <summary>
		/// Receives the conduct from the conductor.
		/// </summary>
		/// <param name="value">Value.</param>

		void ReceiveConduct(float value); // TODO: Check - Do I really only need integer values?

	}

}
