#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - November 6, 2014
#endregion 

#region summary
/// <summary>
/// IMoveable - designate an object as moveable 
/// Primarily used for the follow script to be able to follow objects that are "Moveable" such as Players, NPCs, Vehicles, Etc.
/// </summary>
#endregion


using UnityEngine;
using System.Collections;

public interface IMoveable {

	Transform transform { get; }
	bool IsMoving(); 

}
