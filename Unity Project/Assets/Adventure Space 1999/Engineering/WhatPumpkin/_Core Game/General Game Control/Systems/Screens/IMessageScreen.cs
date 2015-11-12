#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created -  January 13, 2015
#endregion 

namespace WhatPumpkin.Screens {

	/// <summary>
	/// Implements screens with messages
	/// </summary>

	public interface IMessageScreen  {

		#region methods

		/// <summary>
		/// Open a message screen and specifies a message.
		/// </summary>
		/// <param name="message">Message.</param>

		void Open(string message, bool isGraphicMessage = false);

		#endregion
	}

}
