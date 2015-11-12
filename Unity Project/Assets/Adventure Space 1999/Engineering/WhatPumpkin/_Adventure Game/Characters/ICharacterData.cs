#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WhatPumpkin.Entities.Inventory;
#endregion

// TODO: Change the name of this file

namespace WhatPumpkin {

	/// <summary>
	/// ICharacter data.
	/// </summary>

	public interface ICharacterSaveData_Act1  {

		#region properties


		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>

		string Key { get; }

		/// <summary>
		/// Gets the scene.
		/// </summary>
		/// <value>The scene.</value>

		string Scene { get; } 

		/// <summary>
		/// Gets the last cam since active.
		/// </summary>
		/// <value>The last cam since active.</value>

		string LastCamSinceActive { get; }

		/// <summary>
		/// Gets the items.
		/// </summary>
		/// <value>The items.</value>

		List<IItem> Items { get; } 

		/// <summary>
		/// Receives the character data.
		/// </summary>
		/// <param name="data">Data.</param>

		void ReceiveData(ICharacterSaveData_Act1 data); 

		/// <summary>
		/// Gets the serializable transform data.
		/// </summary>
		/// <value>The transform data.</value>

		SerializableTransform TransformData { get; }

		/// <summary>
		/// Gets the room.
		/// </summary>
		/// <value>The room.</value>

		//string Room { get;}

		#endregion
	}

}
