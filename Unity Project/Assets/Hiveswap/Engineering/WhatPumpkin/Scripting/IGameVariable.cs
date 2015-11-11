#region copyright (c) 2015 What Pumpkin Studio
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 28, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;
#endregion

namespace WhatPumpkin.ScriptingLanguage {

	/// <summary>
	/// I game variable.
	/// </summary>

	public interface IGameVariable  {
		
		/// <summary>
		/// Parses the assignment script.
		/// </summary>
		/// <param name="script">Script.</param>

		void Assign(string s_value);
	}
}