#region Copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - March 5, 2015
#endregion

#region using
using UnityEngine;
using System.Collections;
using WhatPumpkin.Entities;
#endregion

// TODO: Remove

namespace WhatPumpkin.Sgrid.Triggers {


	#region component requirements
	[RequireComponent(typeof(BoxCollider))] // All triggers should have a box collider that is set to is trigger
	#endregion

	/// <summary>
	/// Trigger - triggers can inherit off this bass class 
	/// </summary>

	public class Trigger : Entity  {

		#region editor methods

		[ExecuteInEditMode]

		void Start() {
			// Get the box collider and set it to a trigger
			BoxCollider boxCollider = this.GetComponent<BoxCollider> ();
			if (boxCollider != null) {
				boxCollider.isTrigger = true;
			}

		}

		#endregion

	}

}
