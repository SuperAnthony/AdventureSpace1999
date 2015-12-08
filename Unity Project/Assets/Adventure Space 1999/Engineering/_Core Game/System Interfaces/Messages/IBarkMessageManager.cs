#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - June 6, 2015
#endregion 


#region using 
using UnityEngine;
using WhatPumpkin.Localization;
using WhatPumpkin.Screens; // TODO: I'm not thrilled with this being required (Create a hiveswap specific message manager) Why is bark part of Screens?
#endregion

/// <summary>
/// Interface for handling bark messages
/// </summary>

public interface IBarkMessageManager<TKey> {

	Bark Bark (TKey key, Vector3 pos, float duration = 3F);
	
	LocalizedText[] BarkTextCollection  { get; } 
	

}
