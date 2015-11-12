#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - October, 2014
#endregion 


#region using
using WhatPumpkin.Actions.Sequences;
using System.Collections.Generic;
using UnityEngine;
#endregion

namespace WhatPumpkin.Entities.Inventory {

	/// <summary>
	/// IItem.
	/// </summary>

	public interface IItem : IContainer, IEntity {

		#region properties

		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>The description.</value>

		string Description { get; } 

		/// <summary>
		/// Gets a value indicating whether this instance is container.
		/// </summary>
		/// <value><c>true</c> if this instance is container; otherwise, <c>false</c>.</value>

		bool IsContainer { get; }

		/// <summary>
		/// Gets the number of uses this item has.
		/// </summary>
		/// <value>Max Amount.</value>

		int MaxAmount { get; } 

		/// <summary>
		/// Gets the amount.
		/// </summary>
		/// <value>The amount.</value>

		int Amount {get;}

		/// <summary>
		/// Gets the icon.
		/// </summary>
		/// <value>The icon.</value>


		Sprite Icon { get; }

		/// <summary>
		/// Gets the verb sequences attached to this item.
		/// </summary>
		/// <value>The verb sequences.</value>
		
		List<VerbActionSequence> VerbSequences { get; } // TODO: (IList) Create a HIVESWAP folder for Hiveswap items (put verb sequences there)

		#endregion

		#region methods

		/// <summary>
		/// Determines whether this instance is empty.
		/// </summary>
		/// <returns><c>true</c> if this instance is empty; otherwise, <c>false</c>.</returns>

		bool IsEmpty(); 

		/// <summary>
		/// Select this instance.
		/// </summary>

		void Select();

		#endregion


	}

}