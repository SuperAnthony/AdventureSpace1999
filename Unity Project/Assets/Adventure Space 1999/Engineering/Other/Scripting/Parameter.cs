#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 13, 2015
#endregion 

#region using
using UnityEngine;
#endregion

namespace WhatPumpkin.ScriptingLanguage {

	[System.Serializable]

	/// <summary>
	/// A Parameter. For objects that have parameters (duh)
	/// </summary>

	public class Parameter<ValueType> : Keyed  {

		/// <summary>
		/// The value.
		/// </summary>

		[SerializeField] ValueType _value;


		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>

		public override string Key { get { return _key; } }

		/// <summary>
		/// Gets the value of the parameter.
		/// </summary>
		/// <value>The value.</value>

		public ValueType Value { get { return _value; } } 

	}

}