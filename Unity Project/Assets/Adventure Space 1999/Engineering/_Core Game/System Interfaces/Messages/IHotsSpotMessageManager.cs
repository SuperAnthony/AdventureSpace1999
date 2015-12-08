#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - June 6, 2015
#endregion 

/// <summary>
/// Interface for handling hot spot messages
/// </summary>

public interface IHotsSpotMessageManager<TKey> {

	/// <summary>
	/// Shows the hot spot rollover.
	/// </summary>
	/// <param name="key">Key.</param>
	
	void ShowHotSpotRollover (TKey key); 

	/// <summary>
	/// Hides the hot spot rollover.
	/// </summary>

	void HideHotSpotRollover();

	
}
