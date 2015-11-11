#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - September 10, 2015
#endregion 

#region using
using UnityEngine;
#endregion

namespace WhatPumpkin {

	public class FollowMainCamera : MonoBehaviour {

		void Update() {
		
			if (GameController.CamManager.GetMainCamera () == null)
								return;

			this.gameObject.transform.position = new Vector3 (GameController.CamManager.GetMainCamera ().transform.position.x,
			                                                 GameController.CamManager.GetMainCamera ().transform.position.y,
			                                                 GameController.CamManager.GetMainCamera ().transform.position.z);

		}

	}
}