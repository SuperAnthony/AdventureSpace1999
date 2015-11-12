#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - February 1, 2015
#endregion 

namespace WhatPumpkin.Sgrid.Triggers {

	/// <summary>
	/// I trigger actions. Add this interface for objects that will trigger actions when entering action triggers.
	/// </summary>

	public interface ITriggerActions {

		/// <summary>
		/// Gets a value indicating whether this <see cref="WhatPumpkin.ITriggerActions"/> trigger all actions on entry.
		/// </summary>
		/// <value><c>true</c> if trigger all actions on entry; otherwise, <c>false</c>.</value>

		bool TriggerAllActionsOnEntry { get; }

		/// <summary>
		/// Gets a value indicating whether this <see cref="WhatPumpkin.ITriggerActions"/> trigger camera switches on entry.
		/// </summary>
		/// <value><c>true</c> if trigger camera switches on entry; otherwise, <c>false</c>.</value>

		bool TriggerCameraSwitchesOnEntry { get; }

		/// <summary>
		/// Determines whether this instance can trigger action sequence the specified triggerKey.
		/// </summary>
		/// <returns><c>true</c> if this instance can trigger action sequence the specified triggerKey; otherwise, <c>false</c>.</returns>
		/// <param name="triggerKey">Trigger key.</param>

		//bool CanTriggerActionSequence(string triggerKey);


	}
}
