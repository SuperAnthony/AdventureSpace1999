#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - November 5, 2014
#endregion 

#region summary
/// <summary>
/// ISwitchable - for all objects in the game that can switched between activated and deactivated
/// </summary>
#endregion


using UnityEngine;
using System.Collections;

public interface ISwitchable : IActivatable  {

	/// <summary>
	/// Gets a value indicating whether this instance is active.
	/// </summary>
	/// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>

	bool IsActive { get; }

	/// <summary>
	/// Deactivate this instance.
	/// </summary>

	void Deactivate();

	//void Activate();
	


}
