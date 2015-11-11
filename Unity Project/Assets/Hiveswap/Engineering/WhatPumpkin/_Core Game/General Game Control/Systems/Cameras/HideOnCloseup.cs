#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - September 9, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;
#endregion

namespace WhatPumpkin.CameraManagement {

	/// <summary>
	/// Hide on closeup.
	/// </summary>

	public class HideOnCloseup : MonoBehaviour {

		/// <summary>
		/// The _renderer that is attched.
		/// </summary>

		Renderer _renderer;


		void Start() {

				
			_renderer = this.GetComponent<Renderer>();

			GameController.CamManager.SwitchCameraNode += HandleSwitchCameraNode;

		}

		void HandleSwitchCameraNode (object sender, CamSwitchArgs e)
		{


			if (_renderer != null) {
			
				if(e.NewActiveCamera.IsCloseupCam) {
				
					_renderer.enabled = false;

				}
				else if(e.PrevActiveCamera != null && e.PrevActiveCamera.IsCloseupCam && !e.NewActiveCamera.IsCloseupCam) {
					_renderer.enabled = true;
				}
			
			
			}
		}

	}
}