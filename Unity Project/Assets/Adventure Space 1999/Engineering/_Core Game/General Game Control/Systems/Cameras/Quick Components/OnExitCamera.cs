#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - September 1, 2015
#endregion 

#region using
using UnityEngine;
using WhatPumpkin.Actions.Sequences;
#endregion

namespace WhatPumpkin.CameraManagement {

	public class OnExitCamera : MonoBehaviour {

		CameraNode camNode;

		[SerializeField] ActionSequence _exitSequence;

		// Use this for initialization
		void Start () {
		
			camNode = this.GetComponent<CameraNode> ();
			GameController.CamManager.SwitchCameraNode += HandleSwitchCameraNode;

		}

		/// <summary>
		/// Handles the switch camera node.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>


		void HandleSwitchCameraNode (object sender, CamSwitchArgs e)
		{
			if (e.PrevActiveCamera == null) {
				return;			
			}

			if (camNode == e.PrevActiveCamera) {
				_exitSequence.Play();			
			}

		}
		
	}
}
