#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created -  January 13, 2014
#endregion 

namespace WhatPumpkin.Screens {

	/// <summary>
	/// Implements game screen features
	/// </summary>
	
	public interface IGameScreen {

		#region properties

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>

		string Name {get;}

		/// <summary>
		/// Is this screen active
		/// </summary>

		bool IsActive { get; }

		/// <summary>
		/// Close this instance.
		/// </summary>

		void Close();
			
		/// <summary>
		/// Open this instance.
		/// </summary>

		void Open();

		/// <summary>
		/// Switchs between active and inactive state (ie switches between open and close)
		/// </summary>

		void SwitchActiveState();

		#endregion

	}

}
