#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - February 18, 2014
#endregion 

#region using
using UnityEngine;
using System.Collections;
#endregion

namespace WhatPumpkin.Hiveswap {

	/// <summary>
	/// Animate the texture's offset
	/// </summary>

	public class AnimateTextureOffset : MonoBehaviour {


		/// <summary>
		/// The speed that the texture animates at
		/// </summary>

		[SerializeField] float _speed = 1F;

		/// <summary>
		/// The _material.
		/// </summary>

		[SerializeField] Material _material;

		/// <summary>
		/// The offseting texture.
		/// </summary>

		[SerializeField] string _offsetingTexture = "_MainTex";


		float _currentOffset = 0F;

		/// <summary>
		/// Start this instance.
		/// </summary>

		void Start() {
			// If no material is set then automatically set to a default material
			SetDefaultMaterial ();
		}

		// Update is called once per frame
		void Update () {

			AnimateTexture (_speed * Time.deltaTime);

		}

		/// <summary>
		/// Animates the texture.
		/// </summary>
		/// <param name="offetAmt">Offet amt.</param>

		void AnimateTexture(float offetAmt) {

			if (_material != null) {
				_currentOffset += offetAmt;
				_material.SetTextureOffset(_offsetingTexture, new Vector2(_currentOffset, 0));
					
			}
		}

		/// <summary>
		/// Sets the default material.
		/// </summary>

		void SetDefaultMaterial() {

			if (_material == null && GetComponent<Renderer>() != null) {
				
				_material = this.GetComponent<Renderer>().material;
			}
		
		}
	}
}
