#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - June 29, 2015
#endregion 

#region using
using UnityEngine;
#endregion

namespace WhatPumpkin {

	/// <summary>
	/// What pumpkin cursor contains a texture for the icon and it's offset
	/// </summary>

	[System.Serializable]

	public class Cursor : Keyed {

		#region fields

		/// <summary>
		/// The cursor icon.
		/// </summary>

		[SerializeField] Texture2D _icon;

		/// <summary>
		/// The frames if it's an animating icon
		/// </summary>

		[SerializeField] Texture2D [] _frames;

		/// <summary>
		/// The current frame.
		/// </summary>

		int _currentFrame = 0;

		/// <summary>
		/// The offset.
		/// </summary>

		[SerializeField] Vector2 _offset = new Vector2(0,0);


		#endregion

		#region properties

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>

		public override string Key { get { return this._key; } }

		/// <summary>
		/// Gets the icon.
		/// </summary>
		/// <value>The icon.</value>

		public Texture2D Icon { get { return _icon;} }

		/// <summary>
		/// Gets the offset.
		/// </summary>
		/// <value>The offset.</value>

		public Vector2 Offset { get { return _offset; } }

		/// <summary>
		/// Gets a value indicating whether this instance is animated.
		/// </summary>
		/// <value><c>true</c> if this instance is animated; otherwise, <c>false</c>.</value>

		public bool IsAnimated { get { return _frames.Length > 0; } }


		#endregion

		#region methods

		/// <summary>
		/// Go to next frame.
		/// </summary>

		public void GoToNextFrame() {

			_currentFrame++;

			if (_frames.Length > 0) {
			
				if(_currentFrame > _frames.Length - 1) {	
					_currentFrame = 0;
				}
			
				_icon = _frames [_currentFrame];

			} 

		}

		#endregion

	}
}
