#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - February 13, 2014
#endregion 

#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WhatPumpkin.Actions.Sequences;

#endregion

namespace WhatPumpkin.Hiveswap.Abilities {

	[System.Serializable]

	public class Weapon : Ability {

		/// <summary>
		/// The required item of the active PC
		/// </summary>

		[SerializeField] string _requiredItemKey; // Move to weapon


		#region properties

		/// <summary>
		/// Gets a value indicating whether this instance is available.
		/// </summary>
		/// <value><c>true</c> if this instance is available; otherwise, <c>false</c>.</value>

		public bool IsAvailable { get { return RequiresItem == false || GameController.PartyManager.ActivePC.Inventory.HasItem (_requiredItemKey); } } 

		/// <summary>
		/// Gets a value indicating whether this <see cref="WhatPumpkin.Hiveswap.Abilities.Ability"/> requires an item.
		/// </summary>
		/// <value><c>true</c> if requires item; otherwise, <c>false</c>.</value>

		public bool RequiresItem { get { return _requiredItemKey != null && _requiredItemKey != ""; } } 

		/// <summary>
		/// Gets the required item key.
		/// </summary>
		/// <value>The required item.</value>

		public string RequiredItemKey { get { return _requiredItemKey; } }


		#endregion

	}
}