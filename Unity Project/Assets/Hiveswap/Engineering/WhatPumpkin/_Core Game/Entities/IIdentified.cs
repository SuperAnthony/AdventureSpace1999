#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - September 2, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;
#endregion

namespace WhatPumpkin {

	/// <summary>
	/// For objects that have an ID associated with them
	/// </summary>

	public interface IIdentified {

		int ID { get; }

	}
}