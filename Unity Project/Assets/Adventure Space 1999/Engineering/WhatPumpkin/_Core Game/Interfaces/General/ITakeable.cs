#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - November 5, 2014
#endregion 

#region summary
/// <summary>
/// ITakeable - For objects that can be taken
/// </summary>
#endregion

using UnityEngine;
using System.Collections;

public interface ITakeable  {

	// TODO: Should objects you can take also be objects you can give by definition?
	bool Give(); // Returns a message
	bool Take(); // Returns a message
}
