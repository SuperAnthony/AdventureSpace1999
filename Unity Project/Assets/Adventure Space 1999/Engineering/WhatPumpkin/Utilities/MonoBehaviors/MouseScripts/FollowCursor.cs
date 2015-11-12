#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - September 2, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;
#endregion

/// <summary>
/// Follow cursor.
/// </summary>

public class FollowCursor : MonoBehaviour {

	private enum CursorAxis {X, Y, NONE}

	/// <summary>
	/// The cursor axis that the x value follows
	/// </summary>

	[SerializeField] CursorAxis _xFollowsCursorAxis = CursorAxis.X;

	/// <summary>
	/// The cursor axis that the y value follows
	/// </summary>


	[SerializeField] CursorAxis _yFollowsCursorAxis = CursorAxis.Y;

	/// <summary>
	/// The cursor axis that the z value follows
	/// </summary>


	[SerializeField] CursorAxis _zFollowsCursorAxis = CursorAxis.NONE;

	/// <summary>
	/// The _offset.
	/// </summary>

	[SerializeField] Vector3 _offset;

	/// <summary>
	/// The _follow local position.
	/// </summary>

	[SerializeField] bool _followLocalPosition = true;

	/// <summary>
	/// The normalize to screen proportions.
	/// </summary>

	[SerializeField] bool _normalizeToScreenProportions = false;



	void Update() {
	
		// Follow the cursor each frame

		float x = GetValueFromCursorAxis (_xFollowsCursorAxis, GetXValue);
		float y = GetValueFromCursorAxis (_yFollowsCursorAxis, GetYValue);
		float z = GetValueFromCursorAxis (_zFollowsCursorAxis, GetZValue);

		if (_normalizeToScreenProportions) {
		
			x = (x / Screen.width);
			y = (y / Screen.height);

		}

		Debug.Log ("Y: " + y);

		if (_followLocalPosition) {
			this.transform.localPosition = new Vector3 (x + _offset.x, y + _offset.y, z + _offset.z);
		}
		else {
			this.transform.position      = new Vector3 (x + _offset.x, y + _offset.y, z + _offset.z);
		}

	}

	/// <summary>
	/// Gets the value from cursor axis.
	/// </summary>
	/// <returns>The value from cursor axis.</returns>
	/// <param name="axis">Axis.</param>
	/// <param name="noneValue">None value.</param>

	float GetValueFromCursorAxis(CursorAxis axis, System.Func<float> noneValue) {
	
		switch (axis) {
		
		case CursorAxis.X:
			return (float)Input.mousePosition.x;
		case CursorAxis.Y:
			return (float)Input.mousePosition.y;
		case CursorAxis.NONE:
			if(noneValue != null) {
				return noneValue();
			}
			break;
		
		}

		return noneValue ();
	
	}

	/// <summary>
	/// Gets the X value.
	/// </summary>
	/// <returns>The X value.</returns>

	float GetXValue() {
	
		if (_followLocalPosition) {
				
			return this.transform.localPosition.x;
		}
		else {

			return this.transform.position.x;
		}

	}

	/// <summary>
	/// Gets the Y value.
	/// </summary>
	/// <returns>The Y value.</returns>

	float GetYValue() {
	
		if (_followLocalPosition) {
			
			return this.transform.localPosition.y;
		}
		else {
			
			return this.transform.position.y;
		}


	}

	/// <summary>
	/// Gets the Z value.
	/// </summary>
	/// <returns>The Z value.</returns>

	float GetZValue() {

		if (_followLocalPosition) {
			
			return this.transform.localPosition.z;
		}
		else {
			
			return this.transform.position.z;
		}

	
	}

}
