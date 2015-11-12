#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - January 5, 2015
#endregion


using UnityEngine;
using System.Collections;
using WhatPumpkin.Localization;

namespace WhatPumpkin.Entities {

	// Exists because the abstract entity class inherits from a MonoBehaviour and 
	// Not all Entities will be a unity game object I will also have this IEntity interface

	public interface IEntity : IKeyed {

		/// <summary>
		/// Gets the identifier.
		/// </summary>
		/// <value>The identifier.</value>

		int Id { get; }

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>

		string Name { get; }


	}
}