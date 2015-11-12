// Date: November 26th, 2014
// TODO: I should be able to remove this. There is no way this is getting used.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WhatPumpkin.Entities.Inventory;

namespace WhatPumpkin.Screens { 

	/// <summary>
	/// Container.
	/// </summary>

	public class ContainerPanel : GameScreen {

		// Instance Fields

		/// <summary>
		/// The inventory slots associated with this container.
		/// </summary>

		// TODO: Change to ability slot?

		//[SerializeField] List<InventorySlot> _inventorySlots = new List<InventorySlot>(); 


		/// <summary>
		/// The container that this screen is refering to (can be an object or character).
		/// </summary>

		IContainer _container; 


	



		// Methods

		public override void Close() {
		

		}


		public override void Open() {
		
		}



	}
}
