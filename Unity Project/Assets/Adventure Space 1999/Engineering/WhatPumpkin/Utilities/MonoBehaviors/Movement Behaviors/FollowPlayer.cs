using UnityEngine;
using System.Collections;
using WhatPumpkin;

public class FollowPlayer : MonoBehaviour {

	/// <summary>
	/// Does this object follow the player along the x axis.
	/// </summary>

	[SerializeField] bool _followX = true;

	/// <summary>
	/// Does this object follow the player along the y axis.
	/// </summary>
	
	[SerializeField] bool _followY = false;

	/// <summary>
	/// Does this object follow the player along the z axis.
	/// </summary>
	
	[SerializeField] bool _followZ = false;

	/// <summary>
	/// The offset.
	/// </summary>

	[SerializeField] Vector3 _offset = new Vector3(0F,0F,0F);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (GameController.PartyManager.ActivePC == null) {
			return;
			// TODO: Throw exception
		}

		// Declare the would be new coordinates
		float xChange = this.transform.position.x;
		float yChange = this.transform.position.y;
		float zChange = this.transform.position.z;

		if (_followX) {
			xChange = GameController.PartyManager.ActivePC.transform.position.x;
		}

		if (_followY) {
			yChange = GameController.PartyManager.ActivePC.transform.position.y;
		}

		
		if (_followZ) {
			zChange = GameController.PartyManager.ActivePC.transform.position.z;
		}


		this.transform.position = 
			new Vector3 (xChange + _offset.x,
			             yChange + _offset.y,
			             zChange + _offset.z);

	}
}
