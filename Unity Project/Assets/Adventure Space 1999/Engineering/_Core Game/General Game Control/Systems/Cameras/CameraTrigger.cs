#region Copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - November 13, 2014
#endregion

#region using
using UnityEngine;
using System.Collections;
using WhatPumpkin.Sgrid.Triggers;
#endregion

namespace WhatPumpkin.CameraManagement {

	[RequireComponent(typeof(BoxCollider))]

	/// <summary>
	/// For camera switches
	/// </summary>

	public class CameraTrigger : Trigger  {

		#region fields

		/// <summary>
		/// What is the camera node that will be switched to when the player enters this trigger
		/// </summary>

		[SerializeField] CameraNode _activatedCameraNode; 

		// TODO: Logic for this
		//[SerializeField] bool _animateToNext = false;
		//[SerializeField] bool _animateFromPrev = false;


		#endregion

		#region methods

		void OnTriggerEnter(Collider col) {

			// Check to see that the camera node to be activated is not the same as the 
			// Currently active camera node

			if(_activatedCameraNode != null) {


				ITriggersCamSwitch trigger = col.GetComponent(typeof(ITriggersCamSwitch)) as ITriggersCamSwitch;
			
				// Is this an object that can currently trigger camera swiches
				if(trigger != null && trigger.CanTriggerSwitch) {
					// If so, then switch the active camera node
					_activatedCameraNode.Activate();
				}

			}


		}

		#endregion

		#region editor methods
		[ExecuteInEditMode]
		void OnDrawGizmos() {

			Gizmos.matrix = this.transform.localToWorldMatrix;
			Gizmos.color = new Color(0, 0, 1, 1F);
			Gizmos.DrawWireCube (Vector3.zero, Vector3.one);
			Gizmos.color = new Color(0, 0, 1, 0.2F);
			Gizmos.DrawCube (Vector3.zero, Vector3.one);

			
		}
		
		#endregion
	}

}
