#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - June 16, 2015
#endregion 
using System;
#region using

#endregion

namespace WhatPumpkin {

	public delegate bool Combine(string itemKey1, string itemKey2);

	/// <summary>
	/// ICombine item controller.
	/// </summary>

	public interface ICombineItemController  {


		event Combine Combine;

		bool IsUseMode { get; }

		/// <summary>
		/// Starts the combine item mode.
		/// </summary>
		/// <param name="itemKey">Item key.</param>
		
		void StartCombineMode (string itemKey);
		
		/// <summary>
		/// Ends the combine mode.
		/// </summary>
		
		void EndCombineMode ();

		/// <summary>
		/// Adds to combine list.
		/// </summary>
		/// <param name="combinable">Combinable.</param>

		void AddToCombineList (ICombineable combineable);
			
		/// <summary>
		/// Removes from combine list.
		/// </summary>
		/// <param name="combinable">Combinable.</param>
		
		void RemoveFromCombineList (ICombineable combineable);


	}
}
