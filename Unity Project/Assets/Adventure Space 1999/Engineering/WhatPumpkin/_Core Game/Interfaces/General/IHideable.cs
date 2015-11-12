#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - March 12, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;
#endregion

namespace WhatPumpkin {

	/// <summary>
	/// IHideable. For objects that can be hidden and shown
	/// </summary>

	public interface IHideable {

		#region methods

		/// <summary>
		/// Hide this instance.
		/// </summary>

		void Hide();

		/// <summary>
		/// Show this instance.
		/// </summary>

		void Show();

		#endregion
	
	}
}
