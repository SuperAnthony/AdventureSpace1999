#region using
using UnityEngine;
using System.Collections;
#endregion

namespace WhatPumpkin {

	/// <summary>
	/// Load a specified scene after a performance has completed playing. 
	/// </summary>

	public class LoadAfterPerformance : MonoBehaviour {

		/// <summary>
		/// The name of the scene that will load when the performer has completed.
		/// </summary>

		[SerializeField] string sceneName = "";

		/// <summary>
		/// The load screen that will get displayed when loading.
		/// </summary>

		[SerializeField] GameObject loadScreen;

		/// <summary>
		/// The performer.
		/// The component attached to this game object that performs and will finish playing
		/// </summary>

		IPerform performer; // TODO: I keep saying this but if I get full inspector this may be great


		// Use this for initialization
		void Start () {
		
			// Look for an object attached to this component that performs and register to the finished playing event
			performer = this.GetComponent(typeof(IPerform)) as IPerform;
			performer.FinishedPlaying += HandleFinishedPlaying;


		}

		/// <summary>
		/// Handles the finished playing event on the performer.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>

		void HandleFinishedPlaying (object sender, System.EventArgs e)
		{
			// Load the scene when we've finished playing
			LoadScene ();
		}

		/// <summary>
		/// Loads the scene of the sceneName.
		/// </summary>

		void LoadScene() {

			// Activate the load screen if there is one
			if (loadScreen != null) {
				loadScreen.SetActive(true);
			}

			Application.LoadLevel (sceneName);

		}

	

	}
}