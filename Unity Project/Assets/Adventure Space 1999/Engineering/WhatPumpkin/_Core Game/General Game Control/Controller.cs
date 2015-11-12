#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - June 6, 2015
#endregion 

#region using
using UnityEngine;
using System;
#endregion

namespace WhatPumpkin {

	/// <summary>
	/// Intended to be inhereted by all controllers.
	/// </summary>

	public abstract class Controller : MonoBehaviour, IKeyed  {

		/// <summary>
		/// The name of the controller
		/// </summary>

		[SerializeField] string _key;

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>

		public string Key { get { return _key; } }

		/// <summary>
		/// Occurs when object is destroyed.
		/// </summary>

		public event EventHandler Destroyed;

	
	}
}