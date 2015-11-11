#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 15, 2015
#endregion 

#region using
using System;
using UnityEngine;
using System.Collections;
#endregion

namespace WhatPumpkin { 

	/// <summary>
	/// Play a video texture.
	/// </summary>

	public class PlayVideo : MonoBehaviour, IPerform {

		/// <summary>
		/// Gets or sets the play order. The play action itself will use this to keep track of which object is getting played when. 
		/// This is being used as a way or IDing the object
		/// </summary>
		/// <value>The play order.</value>
		
		public int PlayOrder { get; set;}



		public event EventHandler FinishedPlaying;

		/// <summary>
		/// Should this video play on start
		/// </summary>

		[SerializeField] bool playOnStart = false;

		/// <summary>
		/// Has movie started playing
		/// </summary>

		bool hasStartedPlaying = false;

		/// <summary>
		/// The movie texture associated with this video.
		/// </summary>

		MovieTexture movieTexture;

		// Use this for initialization
		void Start () {

			movieTexture = (MovieTexture)this.GetComponent<Renderer>().material.mainTexture;

			if(playOnStart){Play ();}
		

		}

		/// <summary>
		/// Raises the finished playing event.
		/// </summary>

		void OnFinishedPlaying() {
		
			// Reset the has started playing field
			hasStartedPlaying = false;

			// Broadcast the finished playing event
			if (FinishedPlaying != null) {FinishedPlaying.Invoke(this, null);}

		

		}

		/// <summary>
		/// Play the video;
		/// </summary>

		public void Play() {movieTexture.Play ();}

		public void Stop () {
		
			movieTexture.Stop ();

		}


		// Update is called once per frame

		void Update () {

			// Has playback completed
			if (movieTexture.isPlaying == false && hasStartedPlaying == true) {
			
				// Playback complete
				OnFinishedPlaying();

			}

			// Has playback started
			if (movieTexture.isPlaying && hasStartedPlaying == false) {
				hasStartedPlaying = true;
			}
		}
	}
}
