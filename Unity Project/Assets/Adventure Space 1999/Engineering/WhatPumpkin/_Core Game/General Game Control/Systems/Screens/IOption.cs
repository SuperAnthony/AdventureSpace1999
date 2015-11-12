#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created -  May 5, 2015
#endregion 


namespace WhatPumpkin.Screens {

	/// <summary>
	/// Options a user can select from. Options must have a key the developer can use, front facing text and select method.
	/// </summary>

	public interface IOption  {

		#region methods

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>

		string Key { get; }

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <value>The text.</value>

		string Text { get; } 

		/// <summary>
		/// When the user selects this option
		/// </summary>

		void Select();

		#endregion
	}

}
