#region copyright (c) 2015 What Pumpkin Studio
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 28, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;
#endregion

/// <summary>
/// I game variable control.
/// </summary>

public interface IGameVariableControl  {

	/// <summary>
	/// Resets all to default.
	/// </summary>

	void ResetAllToDefault();

	/// <summary>
	/// Parses the assignment script.
	/// </summary>
	/// <param name="script">Script.</param>

	void ParseAssignmentScript(string script);
}
