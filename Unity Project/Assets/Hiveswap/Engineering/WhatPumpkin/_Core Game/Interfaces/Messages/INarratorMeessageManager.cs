#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - June 6, 2015
#endregion 

#region using
using WhatPumpkin.Localization;
#endregion

/// <summary>
/// Interface for handling narrator messages
/// </summary>

public interface INarratorMeessageManager<TKey> {

	/// <summary>
	/// Gets a value indicating whether this instance is showing narrator message.
	/// </summary>
	/// <value><c>true</c> if this instance is showing narrator message; otherwise, <c>false</c>.</value>

	bool IsShowingNarratorMessage { get; }

	/// <summary>
	/// Starts the narrator message.
	/// </summary>
	/// <param name="key">Key.</param>

	void StartNarratorMessage (TKey key); 

	/// <summary>
	/// Gets the narrator text collection.
	/// </summary>
	/// <value>The narrator text collection.</value>

	LocalizedText[] NarratorTextCollection { get; } 

	
}
