#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - September 11, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;
#endregion

namespace WhatPumpkin {

	public class LookAtConstraints : MonoBehaviour {

		[SerializeField] Transform _target;

		[SerializeField] bool _constrainX = false;
		[SerializeField] bool _constrainY = false;
		[SerializeField] bool _constrainZ = false;


		void Update() {
			Vector3 rot = new Vector3 (this.transform.rotation.x,
			                           this.transform.rotation.y,
			                           this.transform.rotation.z);
			
			this.transform.LookAt (_target);
			
			float x = this.transform.rotation.x;
			float y = this.transform.rotation.y;
			float z = this.transform.rotation.z;
			
			if (_constrainX) {x = rot.x;}
			if (_constrainY) {y = rot.y;}
			if (_constrainZ) {z = rot.z;}
			
			this.transform.rotation = new Quaternion(x,y,z,this.transform.rotation.w);

		}

		// Update is called once per frame
		void LateUpdate () {

			Vector3 rot = new Vector3 (this.transform.rotation.x,
			                           this.transform.rotation.y,
			                           this.transform.rotation.z);

			this.transform.LookAt (_target);

			float x = this.transform.rotation.x;
			float y = this.transform.rotation.y;
			float z = this.transform.rotation.z;

			if (_constrainX) {x = rot.x;}
			if (_constrainY) {y = rot.y;}
			if (_constrainZ) {z = rot.z;}

			this.transform.rotation = new Quaternion(x,y,z,this.transform.rotation.w);

		
		}
	}
}
