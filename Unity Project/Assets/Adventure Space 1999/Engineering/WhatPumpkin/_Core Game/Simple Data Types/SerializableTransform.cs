#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - July, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;
#endregion

// TODO: Double check to see what I'm using this for

namespace WhatPumpkin {

	/// <summary>
	/// Serializable transform. Because Unity won't let me.
	/// </summary>

	[System.Serializable]

	public class SerializableTransform {

		/// <summary>
		/// The position.
		/// </summary>

		[SerializeField] SerializableVector3 _position = new SerializableVector3();
		public SerializableVector3 Position { get { return _position; } }

		/// <summary>
		/// The local scale.
		/// </summary>

		[SerializeField] SerializableVector3 _localScale = new SerializableVector3();
		public SerializableVector3 LocalScale { get { return _localScale; } }

		/// <summary>
		/// The rotation.
		/// </summary>

		[SerializeField] SerializableQuaternion _rotation = new SerializableQuaternion();
		public SerializableQuaternion Rotation { get { return _rotation; } }

		/// <summary>
		/// Gets a new serializable transform object from a unity transform.
		/// </summary>
		/// <returns>The serializable transform from transform.</returns>
		/// <param name="transform">Transform.</param>
/*
		public static SerializableTransform GetSerializableTransformFromTransform(Transform transform) {
				
		
			return new SerializableTransform (transform);

		}*/

		/// <summary>
		/// Initializes a new instance of the <see cref="WhatPumpkin.SerializableTransform"/> class.
		/// </summary>

		public SerializableTransform() {
		
			_position = new SerializableVector3 ();
			_localScale = new SerializableVector3 ();
			_rotation = new SerializableQuaternion ();

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WhatPumpkin.SerializableTransform"/> class.
		/// </summary>
		/// <param name="transform">Transform.</param>

		public SerializableTransform(Transform transform) {

			ReceiveTransformInfo (transform);

		}

		/// <summary>
		/// Receives the transform info.
		/// </summary>
		/// <param name="transform">Transform.</param>

		public void ReceiveTransformInfo(Transform transform) {

			_position.ReceiveVector3Values (transform.position);
			_localScale.ReceiveVector3Values (transform.localScale);
			_rotation.ReceiveQuaternionValues (transform.rotation);

		}

	}

	/// <summary>
	/// Serializable vector3. Because Unity won't let me.
	/// </summary>

	[System.Serializable]

	public class SerializableVector3 {

		/// <summary>
		/// The x value.
		/// </summary>
		
		[SerializeField] float _x = 0F;
		public float X { get { return _x; } }
		
		/// <summary>
		/// The y value.
		/// </summary>
		
		[SerializeField] float _y = 0F;
		public float Y { get { return _y; } }
		
		/// <summary>
		/// The z value.
		/// </summary>
		
		[SerializeField] float _z = 0F;
		public float Z { get { return _z; } }

		/// <summary>
		/// Initializes a new instance of the <see cref="WhatPumpkin.SerializableVector3"/> class.
		/// </summary>

		public SerializableVector3() {

			_x = 0;
			_y = 0;
			_z = 0;

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SerializableVector3"/> class.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="z">The z coordinate.</param>

		public SerializableVector3 (float x, float y, float z) {

			_x = x;
			_y = y;
			_z = z;

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SerializableVector3"/> class.
		/// </summary>
		/// <param name="item">Item.</param>

		public SerializableVector3 (Vector3 item) {

			ReceiveVector3Values (item);
		
		}

		/// <summary>
		/// Receives the values of a vector 3
		/// </summary>
		/// <param name="item">The Vector 3 Item.</param>

		public void ReceiveVector3Values(Vector3 item) {

			_x = item.x;
			_y = item.y;
			_z = item.z;
		
		}

		/// <summary>
		/// Gets as unity vector3.
		/// </summary>
		/// <returns>The as vector3.</returns>

		public Vector3 GetAsVector3() {return new Vector3(_x,_y,_z);}


	}

	/// <summary>
	/// Serializable quaternion. Because Unity won't let me.
	/// </summary>

	[System.Serializable]

	public class SerializableQuaternion {

		/// <summary>
		/// The x value.
		/// </summary>
		
		[SerializeField] float _x = 0F;
		public float X { get { return _x; } }
		
		/// <summary>
		/// The y value.
		/// </summary>
		
		[SerializeField] float _y = 0F;
		public float Y { get { return _y; } }
		
		/// <summary>
		/// The z value.
		/// </summary>
		
		[SerializeField] float _z = 0F;
		public float Z { get { return _z; } }

		/// <summary>
		/// The w.
		/// </summary>

		[SerializeField] float _w = 0F;
		public float W { get { return _w; } }

		public SerializableQuaternion() {

			_x = 0;
			_y = 0;
			_z = 0;
			_w = 0;
		
		
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SerializeableQuaternion"/> class.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="z">The z coordinate.</param>
		/// <param name="w">The width.</param>

		public SerializableQuaternion (float x, float y, float z, float w) {

			_x = x;
			_y = y;
			_z = z;
			_w = w;
		
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SerializeableQuaternion"/> class.
		/// </summary>
		/// <param name="item">Item.</param>

		public SerializableQuaternion (Quaternion item) {

			ReceiveQuaternionValues (item);
		
		}
		public void ReceiveQuaternionValues (Quaternion item) {

			_x = item.x;
			_y = item.y;
			_z = item.z;
			_w = item.w;
				
		
		}

		public Quaternion GetAsQuaternion () {return new Quaternion (_x, _y, _z, _w);}

	}
}
