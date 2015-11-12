#region Copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - January 9, 2015
#endregion

#region using
using UnityEngine;
#endregion

namespace WhatPupkin {

	/// <summary>
	/// The type of data that will be stored for any entity (objects living in a unity scene)
	/// </summary>

	public interface IEntitySaveData {

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>

		string Key { get; }

		/// <summary>
		/// Gets the transform information.
		/// </summary>
		/// <value>The transform.</value>

		Transform Transform { get; }

		/// <summary>
		/// Gets the state of the animation.
		/// </summary>
		/// <value>The state of the animation.</value>

		string AnimationState { get; }

		/// <summary>
		/// Gets a value indicating whether this <see cref="WhatPupkin.IEntitySaveData"/> is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>

		bool Enabled { get ; }

	}
}
