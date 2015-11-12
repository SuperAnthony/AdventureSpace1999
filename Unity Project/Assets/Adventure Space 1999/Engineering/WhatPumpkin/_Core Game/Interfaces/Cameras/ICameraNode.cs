#region Copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - June 6, 2015
#endregion

#region using
using UnityEngine;
#endregion

public interface ICameraNode {

	/// <summary>
	/// Gets a value indicating whether this instance is closeup cam.
	/// </summary>
	/// <value><c>true</c> if this instance is closeup cam; otherwise, <c>false</c>.</value>
	
	bool IsCloseupCam { get; }
	
	/// <summary>
	/// Gets the active player tracker.
	/// This node tracks the player's position and is needed for rails
	/// </summary>
	/// <value>The active player tracker.</value>
	/// 
	GameObject ActivePlayerTracker { get;  }
	
	/// <summary>
	/// Gets the local X offset.
	/// </summary>
	/// <value>The local X offset.</value>
	
	float LocalXOffset { get; }

}
