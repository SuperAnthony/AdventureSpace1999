#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 29, 2015
#endregion 

using UnityEngine;
using System.Collections;

/// <summary>
/// Receivers can add and remove items
/// </summary>

public interface IReceiver  {

	/// <summary>
	/// Adds and item with the item key name to the receiver
	/// </summary>
	/// <param name="itemKey">Item key.</param>

	void Add (string item);

	/// <summary>
	/// Removes an item with the item key name from the receiver
	/// </summary>
	/// <param name="itemKey">Item key.</param>

	void Remove (string item);

}
