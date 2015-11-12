#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 18, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections.Generic;
#endregion

namespace WhatPumpkin.EditorWindowUtilities {
	
	[System.Serializable]

	public sealed class PropertyInputCollection {

		//static public PropertyInputCollection

		/// <summary>
		/// A collection of property input fields.
		/// </summary>

		// Use interface of property inputs?
		//static protected Dictionary<string, PropertyInput<T, U, V>> _propertyInputFields = new Dictionary<string, PropertyInput<TReturn, TDisplay, TParams>> ();

		/// <summary>
		/// Gets the property input fields.
		/// </summary>
		/// <value>The property input fields.</value>

		//static public Dictionary<string, PropertyInput> PropertyInputFields { get { return _propertyInputFields; } }

		/*
		static public void AddPropertyInput(PropertyInput<TReturn, TDisplay, TParams> propertyInput) {

			_propertyInputFields.Add (propertyInput.Key, propertyInput);
		
		}*/

		private PropertyInputCollection() {
		
		}


	}
}
