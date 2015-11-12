#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - June 18, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;
#endregion

public class FollowTransform : MonoBehaviour {

	/// <summary>
	/// The control node.
	/// </summary>

	[SerializeField] Transform controlNode;

	/// <summary>
	/// Follow the x position of the control node?
	/// </summary>
	/// 
	[SerializeField] bool xPosition = false;
	
	/// <summary>
	/// Follow the y position of the control node?
	/// </summary>
	
	
	[SerializeField] bool yPosition = false;
	
	/// <summary>
	/// Follow the z position of the control node?
	/// </summary>
	
	
	[SerializeField] bool zPosition = false;


	/// <summary>
	/// Follow the x rotation of the control node?
	/// </summary>
	/// 
	[SerializeField] bool xRotation = false;

	/// <summary>
	/// Follow the y rotation of the control node?
	/// </summary>


	[SerializeField] bool yRotation = false;

	/// <summary>
	/// Follow the z rotation of the control node?
	/// </summary>


	[SerializeField] bool zRotation = false;


	// Update is called once per frame
	void Update () {

	

		if (controlNode != null) {

			this.transform.position = GetPos ();
			this.transform.rotation = GetRot ();
		
		}
	
	}

	float GetAxisValue(bool doesAxisFollow, float thisObjectAxis, float controlObjectAxis) {

		if (!doesAxisFollow) {
			return thisObjectAxis;
		}

		return controlObjectAxis;

	}

	Vector3 GetPos() {
	
		float x = GetAxisValue (xPosition, this.transform.position.x, controlNode.transform.position.x);
		float y = GetAxisValue (yPosition, this.transform.position.y, controlNode.transform.position.y);
		float z = GetAxisValue (zPosition, this.transform.position.z, controlNode.transform.position.z);

		return new Vector3(x,y,z);

	}

	Quaternion GetRot() {
	
		float x = GetAxisValue (xRotation, this.transform.rotation.x, controlNode.transform.rotation.x);
		float y = GetAxisValue (yRotation, this.transform.rotation.y, controlNode.transform.rotation.y);
		float z = GetAxisValue (zRotation, this.transform.rotation.z, controlNode.transform.rotation.z);

		return new Quaternion (x, y, z, 0);
	
	}

}
