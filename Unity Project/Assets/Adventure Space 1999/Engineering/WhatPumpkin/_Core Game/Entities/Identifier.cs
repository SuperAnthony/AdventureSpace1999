#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - September 2, 2015
#endregion 

#region using
using UnityEngine;
#endregion

namespace WhatPumpkin {

	/// <summary>
	/// Component to provide an Identifier.
	/// </summary>

	public class Identifier : MonoBehaviour, IIdentified {

		#region fields

		/// <summary>
		/// The _id.
		/// </summary>

		#endregion

		[SerializeField] int _id;


		#region properties

		/// <summary>
		/// Gets the ID.
		/// </summary>
		/// <value>The ID.</value>

		public int ID { get { return _id; } }

		#endregion



	}
}