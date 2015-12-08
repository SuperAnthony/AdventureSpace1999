#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - January 7, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#endregion

namespace WhatPumpkin.Localization {
	
	public interface ILocalizedText  {

		#region properties

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>

		string Key { get; }

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <value>The text.</value>

		List<string> Text { get; }

		#endregion
	}

}
