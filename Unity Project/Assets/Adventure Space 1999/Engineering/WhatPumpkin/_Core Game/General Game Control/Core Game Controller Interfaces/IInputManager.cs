#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - June 17, 2015
#endregion 

#region using
using System;
using UnityEngine;
#endregion

namespace WhatPumpkin {

	/// <summary>
	/// IInputManager
	/// </summary>

	public interface IInputManager  {

		Transform Target { get; }

		/// <summary>
		/// Occurs when target moved.
		/// </summary>

		event EventHandler<TargetEventArgs> TargetChangeEvent;

		/// <summary>
		/// Occurs when left mouse click.
		/// </summary>

		event Action LeftMouseClick;

		/// <summary>
		/// Occurs when right mouse click.
		/// </summary>

		event Action RightMouseClick;

		/// <summary>
		/// Receives a target that was moved.
		/// </summary>
		/// <param name="target">Target.</param>
		
		void ReceiveTargetEvent (UnityEngine.GameObject target);


	}

	public enum TargetEventType { Moved }

	public class TargetEventArgs : EventArgs {

		#region fields

		/// <summary>
		/// The _target.
		/// </summary>

		UnityEngine.GameObject _target;

		/// <summary>
		/// The type of the _target event.
		/// </summary>

		TargetEventType _targetEventType = TargetEventType.Moved;


		#endregion

		#region properties

		/// <summary>
		/// Gets the target.
		/// </summary>
		/// <value>The target.</value>

		public UnityEngine.GameObject Target { get { return _target; } }

		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		/// <value>The type of the event.</value>

		public TargetEventType EventType { get { return _targetEventType; } }

		/// <summary>
		/// Initializes a new instance of the <see cref="WhatPumpkin.TargetEvent"/> class.
		/// </summary>
		/// <param name="target">Target.</param>

		public TargetEventArgs(UnityEngine.GameObject target, TargetEventType eventType) {
			_target = target;
			_targetEventType = eventType;

		}

		#endregion

	}

}