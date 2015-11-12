#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - February 21, 2014
#endregion 

using UnityEngine;


namespace WhatPumpkin.Entities.Inventory {

	/// <summary>
	/// Icon group. Quickie solution because I can't expose dictionaries in the inspector
	/// </summary>

	[System.Serializable]

	public class IconGroup   {

		/// <summary>
		/// The _key.
		/// </summary>

		[SerializeField] string _key;

		/// <summary>
		/// The icon.
		/// </summary>

		[SerializeField] Sprite _icon; 

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>

		public string Key { get { return _key; } }

		/// <summary>
		/// Gets the icon.
		/// </summary>
		/// <value>The icon.</value>

		public Sprite Icon { get { return _icon; } }
	
	}
}