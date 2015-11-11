#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - April 6, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;
#endregion


namespace WhatPumpkin.Sgrid.Environment {

	[System.Serializable]

	/// <summary>
	/// Fog. Used to let environment artists create fog settings by room 
	/// </summary>

	public class Fog {

		#region fields

		/// <summary>
		/// Is fog enabled.
		/// </summary>

		[SerializeField] bool _enabled = true;

		/// <summary>
		/// The color of the fog.
		/// </summary>

		[SerializeField] Color _color = Color.gray;

		/// <summary>
		/// The fog density.
		/// </summary>

		[SerializeField] float _density = 0F; 

		/// <summary>
		/// The start distance.
		/// </summary>

		[SerializeField] float _startDistance = 0F;

		/// <summary>
		/// The fog end distance.
		/// </summary>

		[SerializeField] float _endDistance = 0F;

		/// <summary>
		/// The fog mode.
		/// </summary>

		[SerializeField] FogMode _mode; 


		#endregion

		#region properties

		/// <summary>
		/// Gets a value indicating whether this <see cref="WhatPumpkin.Sgrid.Environment.Fog"/> is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>

		public bool Enabled { get { return _enabled; } }

		/// <summary>
		/// Gets the color.
		/// </summary>
		/// <value>The color.</value>

		public Color Color { get { return _color; } }

		/// <summary>
		/// Gets the density.
		/// </summary>
		/// <value>The density.</value>

		public float Density { get { return _density; } }

		/// <summary>
		/// Gets the start distance.
		/// </summary>
		/// <value>The start distance.</value>

		public float StartDistance { get { return _startDistance; } }

		/// <summary>
		/// Gets the end distance.
		/// </summary>
		/// <value>The end distance.</value>

		public float EndDistance { get { return _endDistance; } }

		/// <summary>
		/// Gets the mode.
		/// </summary>
		/// <value>The mode.</value>

		public FogMode Mode { get { return _mode; } }

		#endregion

		#region methods

		/// <summary>
		/// Applies the fog.
		/// </summary>

		internal void ApplyFog() {
		
			RenderSettings.fog = _enabled;
			RenderSettings.fogMode = _mode;
			RenderSettings.fogColor = _color;
			RenderSettings.fogDensity = _density;
			RenderSettings.fogStartDistance = _startDistance;
			RenderSettings.fogEndDistance = _endDistance;
		}

		#endregion



		/// <summary>
		/// Sets the properties. Intended for the editor only.
		/// </summary>
		/// <param name="enabled">If set to <c>true</c> enabled.</param>
		/// <param name="color">Color.</param>
		/// <param name="density">Density.</param>
		/// <param name="startDistance">Start distance.</param>
		/// <param name="endDistance">End distance.</param>
		/// <param name="mode">Mode.</param>

		public void SetProperties(bool enabled, Color color, float density, float startDistance, float endDistance, FogMode mode) {
		
			_enabled = enabled;
			_color = color;
			_density = density;
			_startDistance = startDistance;
			_endDistance = endDistance;
			_mode = mode;
		
		}

		/// <summary>
		/// Applies the fog in editor mode.
		/// </summary>

		public void ApplyFogEditor() {
		
			ApplyFog ();

		}



	}
}
