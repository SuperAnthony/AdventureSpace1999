#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created -  May 5, 2015
#endregion 

namespace WhatPumpkin.Screens {

	/// <summary>
	/// Implements screens with messages and options
	/// </summary>

	public interface IMessageOptionsScreen : IMessageScreen  {

		#region methods

		/// <summary>
		/// Open the specified message and it's associated options.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="options">Options.</param>

		void Open(string message, IOption [] options);

		#endregion
	}

}
