#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - August 21, 2015
#endregion 

#region summary
/// <summary>
/// IActivatable<TARGS> - An activatable that accepts arguments
/// </summary>
#endregion

public interface IActivatable<TArgs>  {

	/// <summary>
	/// Activate this instance.
	/// </summary>

	void Activate(TArgs [] args);

}
