using UnityEngine;
using System.Collections;

namespace WhatPumpkin {

	[RequireComponent(typeof(RectTransform))]

	public class ScrollUI : MonoBehaviour {

		[SerializeField] float _speed;

		RectTransform _rectTransform;

		void Start() {
		
			_rectTransform = this.GetComponent<RectTransform> ();
		
		}

		void Update() {
		
			_rectTransform.localPosition = new Vector3 (this.transform.localPosition.x,
			                                            this.transform.localPosition.y + (_speed * Time.deltaTime),
			                                           this.transform.localPosition.z);

		}

	}
}