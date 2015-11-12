#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - April 21, 2015
#endregion

#region using
using UnityEngine;
using System.Collections;

using WhatPumpkin.Entities;
#endregion

namespace WhatPumpkin.Sgrid.Markers {

	/// <summary>
	/// Marker. Generally used to mark points on the ground.
	/// </summary>

	
	public abstract class Marker : Entity {

		/// <summary>
		/// Is this target supposed to influence the character's rotation data. 
		/// If so, then the character's rotation will match the rotation data of this object once the chracter reaches it's desitation or comes close to it.
		/// </summary>
		
		[SerializeField] protected bool _affectCharacterRotation = false;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="WhatPumpkin.Sgrid.Markers.Marker"/> affect character rotation.
		/// </summary>
		/// <value><c>true</c> if affect character rotation; otherwise, <c>false</c>.</value>

		public bool AffectCharacterRotation {
			get { return _affectCharacterRotation; }
			set { _affectCharacterRotation = value; }
		}



	}
}
