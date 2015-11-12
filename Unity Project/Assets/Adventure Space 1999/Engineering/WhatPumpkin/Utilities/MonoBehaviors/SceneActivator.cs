// Date: November 25th, 2014

using UnityEngine;
using System.Collections;

// TODO: See where this is getting used
// TODO: Use the scene manager namespace

namespace WhatPumpkin.HiveSwap.GameControl {

	/// <summary>
	/// Scene activator.
	/// </summary>

	public class SceneActivator : MonoBehaviour, ISwitchable {

		// Fields

		/// <summary>
		/// The scene that is being loaded.
		/// </summary>
		[SerializeField] string _scene;

		// Properties

		// This always inactive
		public bool IsActive { get { return false; } }

	
		public void Activate() {

			GameController.SceneManager.LoadScene (_scene);
		
		}

		// There is nothing to deactivate

		public void Deactivate() {
		
		}
	}
}
