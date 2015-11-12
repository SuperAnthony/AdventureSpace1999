#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 17, 2015
#endregion 

#if UNITY_EDITOR

#region using
using UnityEngine;
using System.Collections.Generic;
#endregion

namespace WhatPumpkin.EditorWindowUtilities {
	
	[System.Serializable]

	// TODO: Inheret from Keyed

	public abstract class PropertyInput<TReturn, TDisplay, TParams> : Keyed {

		/// <summary>
		/// A collection of property input fields.
		/// </summary>

		// Use interface of property inputs?
		protected static Dictionary<string, PropertyInput<TReturn,TDisplay,TParams>> _inputFields = new Dictionary<string, PropertyInput<TReturn,TDisplay,TParams>> ();

		/// <summary>
		/// Gets the property input fields.
		/// </summary>
		/// <value>The property input fields.</value>

		public static Dictionary<string, PropertyInput<TReturn,TDisplay,TParams>> InputFields { get { return _inputFields; } }

		static Color _originalColor;

		#region member fields

		/// <summary>
		/// The width of this input.
		/// </summary>

		protected float _width = 150F;

		/// <summary>
		/// The default color.
		/// </summary>

		protected Color _defaultColor = Color.white;

		/// <summary>
		/// The selected color.
		/// </summary>

		protected Color _selectedColor = Color.white;

		#endregion

		#region IKeyed members

		/// <summary>
		/// The key.
		/// </summary>
		
		//protected string _key = "";

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>

		public override string Key { 
		
			get { return _key; } 

			//protected set { _key = value;}
		
		}


		#endregion

		#region methods

		/// <summary>
		/// Draws an input field of a specified key.
		/// </summary>
		/// <returns>The key.</returns>
		/// <param name="key">Key.</param>
		/// <param name="content">Content.</param>

		public static TReturn DrawKey (string key, TDisplay content, bool isSelected = false) {
		
			return _inputFields [key].Draw (content, isSelected);

		}

		public PropertyInput() {}



		public abstract TReturn Draw(TDisplay content, bool isSelected = false, params TParams [] parameters);

		/// <summary>
		/// Uses the selection color
		/// </summary>

		protected virtual void UseSelectColor() {
			_originalColor = GUI.backgroundColor;
			GUI.backgroundColor = _selectedColor;
		}

		/// <summary>
		/// Returns to the previous color
		/// </summary>

		protected virtual void ClearSelectColor() {
			if (_originalColor != null) {
								// Return to the default color
								GUI.backgroundColor = _originalColor;
						}
		}

		#endregion
	}
}

#endif
