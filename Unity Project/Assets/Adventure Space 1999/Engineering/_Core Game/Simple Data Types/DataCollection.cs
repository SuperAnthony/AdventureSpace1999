#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - September 2, 2015
#endregion 

#region using
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
#endregion

namespace WhatPumpkin {

	public abstract class DataCollection : MonoBehaviour, IKeyed {

		#region fields

		/// <summary>
		/// The key.
		/// </summary>

		[SerializeField] string _key;

		#endregion

		#region properties

		/// <summary>
		/// Gets or sets the collection.
		/// </summary>
		/// <value>The collection.</value>

		public abstract  IKeyed [] Collection { get; }

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>
		
		public string Key { get { return _key; } }
		
		/// <summary>
		/// Occurs when object is destroyed.
		/// </summary>
		
		public event EventHandler Destroyed;

		#endregion

		#region methods


		/// <summary>
		/// Occurs on awake
		/// </summary>

		protected void Awake() {
		
			GameController.SceneManager.SceneLoadEnd += HandleSceneLoadEnd;

		}

		/// <summary>
		/// Handles the scene load end.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>

		void HandleSceneLoadEnd (object sender, System.EventArgs e)
		{
			Keyed.AddKeys (Collection);
		}

		#endregion

	}
}