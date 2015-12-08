#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - June 6, 2015
#endregion 

#region using
using System;
using UnityEngine;
using System.Collections;

using WhatPumpkin.CameraManagement; // TODO: ICameraNode - this is unfortunatly causing more problems than desired
#endregion

namespace WhatPumpkin {
	
	/// <summary>
	/// I camera manager.
	/// </summary>

	public interface ICameraManager  {

		#region events
			
		/// <summary>
		/// Occurs when switch camera node.
		/// </summary>

		event EventHandler<CamSwitchArgs> SwitchCameraNode;

		#endregion

		#region properties

		/// <summary>
		/// Gets the active camera.
		/// </summary>
		/// <value>The active camera.</value>

		Camera ActiveCamera { get; } 

		/// <summary>
		/// Gets or sets the active camera node.
		/// </summary>
		/// <value>The active camera node.</value>

		CameraNode ActiveCameraNode { get; set; }

		#endregion

		#region methods

		void Start();

		/// <summary>
		/// Starts the closeup.
		/// </summary>
		/// <param name="cameraName">Camera name.</param>

		void StartCloseup(string cameraName);

		/// <summary>
		/// Ends the closeup.
		/// </summary>

		void EndCloseup();

		#endregion

	}

	

}
