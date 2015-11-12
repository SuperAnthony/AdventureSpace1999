#region Copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - January 7, 2015
#endregion

#region using
using UnityEngine;
using System.Collections;
#endregion

namespace WhatPumpkin.Localization {
	
	public interface IEntityDescriptionText  {

		#region properties

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>

		string Key { get; }

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>

		string Name { get; }

		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>The description.</value>

		string Description { get; }

		#endregion
	}
}
