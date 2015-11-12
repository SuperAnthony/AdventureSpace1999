using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace WhatPumpkin {

	public class LoadScene : MonoBehaviour {


		/// <summary>
		/// The _load screen image.
		/// </summary>

		[SerializeField] Image _loadScreenImage;

		/// <summary>
		/// The name of the _scene.
		/// </summary>

		[SerializeField] string _sceneName;

		/// <summary>
		/// The name of the _spawn point.
		/// </summary>

		[SerializeField] string _spawnPointName;

		/// <summary>
		/// Loads the scene.
		/// </summary>

		public void Load() {

			// Display the load screen image

			_loadScreenImage.gameObject.SetActive (true);

			// Activate Level

			Application.LoadLevel (_sceneName);

			//GameController.SceneManager.LoadScene(_sceneName, _spawnPointName);
		}

	}
}
