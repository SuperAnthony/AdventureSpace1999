#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - September 2, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;
#endregion

namespace WhatPumpkin {
	

	/// <summary>
	/// Keyed sprite collection.
	/// </summary>

	public class KeyedSpriteCollection : DataCollection  {

		/// <summary>
		/// The collection of keyed sprites.
		/// </summary>

		[SerializeField] KeyedSprite [] _keyedSprites;

		/// <summary>
		/// Gets the collection.
		/// </summary>
		/// <value>The collection.</value>

		public override IKeyed [] Collection { get { return (IKeyed[])_keyedSprites as IKeyed[]; } } 
	}
}