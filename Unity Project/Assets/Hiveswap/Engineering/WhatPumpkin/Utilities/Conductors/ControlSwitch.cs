#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - February 18, 2014
#endregion 

#region using
using UnityEngine;
using System.Collections;
#endregion

namespace WhatPumpkin.NodeControllers {

	[System.Serializable]

	/// <summary>
	/// Control switch.
	/// </summary>

	public class ControlSwitch {

		#region fields

		/// <summary>
		/// The value.
		/// </summary>

		[SerializeField] float _value;

		/// <summary>
		/// The method.
		/// </summary>

		[SerializeField] string _methodName;

		// TODO:
		// Component Name

		#endregion

		#region propeties

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <value>The value.</value>

		public float Value { get { return _value; } }

		/// <summary>
		/// Gets the name of the method.
		/// </summary>
		/// <value>The name of the method.</value>

		public string MethodName { get { return _methodName; } }

		#endregion

	}

}
