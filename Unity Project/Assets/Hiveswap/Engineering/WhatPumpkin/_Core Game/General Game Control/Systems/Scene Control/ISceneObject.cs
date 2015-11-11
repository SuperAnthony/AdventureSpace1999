#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - July 22, 2015
#endregion 

namespace WhatPumpkin {

	public interface ISceneObject<TKey>  {

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>

		TKey Key { get; }

		/// <summary>
		/// Gets a value indicating whether this <see cref="WhatPumpkin.ISceneObject`1"/> is active.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>

		bool IsActive { get; }

		/// <summary>
		/// Gets the state of the animation.
		/// </summary>
		/// <value>The state of the animation.</value>

		//string AnimationState { get; }

		/// <summary>
		/// Receives the data.
		/// </summary>
		/// <param name="item">Item.</param>

		void ReceiveData(ISceneObject<TKey> item);


	}
}