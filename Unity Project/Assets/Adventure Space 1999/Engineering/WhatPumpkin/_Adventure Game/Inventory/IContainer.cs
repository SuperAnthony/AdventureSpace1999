#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - December 1, 2014
#endregion 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace WhatPumpkin.Entities.Inventory {


/// <summary>
/// IContainer. Interface for handling objects that can hold inventory items
/// </summary>

public interface IContainer {

		/// <summary>
		/// Gets the items.
		/// </summary>
		/// <value>The items.</value>

		List<IItem> Items {get;}

		/// <summary>
		/// Gets a value indicating whether this instance is open.
		/// </summary>
		/// <value><c>true</c> if this instance is open; otherwise, <c>false</c>.</value>

		bool IsOpen { get; }

		/// <summary>
		/// Adds the item to next empty inventory slot.
		/// </summary>
		/// <returns><c>true</c>, if item to next empty was added, <c>false</c> otherwise.</returns>
		/// <param name="item">Item.</param>

		bool AddItemToNextEmpty(IItem item);

		/// <summary>
		/// Removes the specified item from the container.
		/// </summary>
		/// <param name="item">Item.</param>

		void RemoveItem(IItem item);

		/// <summary>
		/// Removes the item.
		/// </summary>
		/// <param name="itemKey">Item key.</param>

		void RemoveItem(string itemKey);

		/// <summary>
		/// Adds the specified item to the specified container slot.
		/// </summary>
		/// <param name="item">Item.</param>
		/// <param name="containerSlot">Container slot.</param>

		void AddItem(IItem item, int containerSlot);

		/// <summary>
		/// Open this instance.
		/// </summary>

		void Open();

		/// <summary>
		/// Close this instance.
		/// </summary>

		void Close();

	}
}

