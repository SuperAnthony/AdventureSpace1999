#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created -  June 16, 2015
#endregion 

#region using
using System;
#endregion

// TODO: Use sgrid namespace?

namespace WhatPumpkin {

	public delegate void ItemKeyEvent(string itemKey);

	/// <summary>
	/// For items that can be combined
	/// </summary>

	public interface ICombineable {

		event ItemKeyEvent SendItem;

	}
}