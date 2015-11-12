#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - August 24, 2015
#endregion 

#region using
using System;
#endregion

namespace WhatPumpkin {

	/// <summary>
	/// Mouse event type.
	/// </summary>

	public enum MouseEventType {MOUSE_ENTER, MOUSE_EXIT, MOUSE_UP, MOUSE_DOWN}

	/// <summary>
	/// Mouse event arguments.
	/// </summary>

	public class MouseEventArgs : EventArgs {

		public MouseEventType MouseEventType { get; private set; }

		public MouseEventArgs(MouseEventType _mouseEventType) {

			MouseEventType = _mouseEventType;
		}

	}
}