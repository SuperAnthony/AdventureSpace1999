using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using WhatPumpkin.Sgrid.Environment;

namespace WhatPumpkin {

	public class StampCurrentRoomImage : MonoBehaviour {

		[SerializeField] Image _imageActive;
		[SerializeField] Image _imageInactive;


		/// <summary>
		/// Activate this instance.
		/// </summary>

		public void Activate() {	

			/*
			if (_imageActive != null && Room.ActiveRoom != null && Room.ActiveRoom.ThumbnailSprite != null) {
				_imageActive.sprite = Room.ActiveRoom.ThumbnailSprite;
			}*/


		}


	}
}
