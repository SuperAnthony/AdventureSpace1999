#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - September 2, 2015
#endregion 


#region using
using UnityEngine;
using UnityEngine.UI;
#endregion


namespace WhatPumpkin {

	[System.Serializable]

	/// <summary>
	/// Keyed sprite.
	/// </summary>

	public class KeyedSprite : Keyed {

		#region fields

		/// <summary>
		/// The sprite.
		/// </summary>

		[SerializeField] Sprite _sprite;

		#endregion

		#region properties

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>

		public override string Key { get { return _key; } }

		/// <summary>
		/// Gets the sprite.
		/// </summary>
		/// <value>The sprite.</value>

		public Sprite Sprite { get { return _sprite; } }

		#endregion

	}
}