#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created -  January 13, 2014
#endregion 

#region using
using UnityEngine;
#endregion

namespace WhatPumpkin.Screens {

	/// <summary>
	/// Implements screens with messages
	/// </summary>

	public interface IBark : IMessageScreen  {

		#region methods

		/// <summary>
		/// Sets the properties.
		/// </summary>
		/// <param name="pos">Position.</param>
		/// <param name="duration">Duration.</param>

		void SetProperties(Vector3 pos, float duration);

		#endregion
	}

}
