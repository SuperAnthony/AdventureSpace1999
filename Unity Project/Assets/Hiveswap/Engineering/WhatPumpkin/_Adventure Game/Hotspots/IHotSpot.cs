#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created: June 8, 2015
#endregion 

#region using
using System.Collections.Generic;
#endregion

namespace WhatPumpkin {
	
	/// <summary>
	/// Interface for hotspots.
	/// </summary>
	
	public interface IHotSpot : IEnable {

		/// <summary>
		/// Gets the required items.
		/// </summary>
		/// <value>The required items.</value>

		List<string> RequiredItems {get;}

		/// <summary>
		/// Gets a value indicating whether this instance has required cam.
		/// </summary>
		/// <value><c>true</c> if this instance has required cam; otherwise, <c>false</c>.</value>

		bool HasRequiredCam { get;}

		/// <summary>
		/// Gets the name of the required cam.
		/// </summary>
		/// <value>The name of the required cam.</value>

		string RequiredCamName { get;}

		/// <summary>
		/// Gets a value indicating whether this instance is live.
		/// </summary>
		/// <value><c>true</c> if this instance is live; otherwise, <c>false</c>.</value>

		bool IsLive {get;}
		
	}
}