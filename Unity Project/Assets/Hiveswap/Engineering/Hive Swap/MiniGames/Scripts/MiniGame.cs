using System;
using UnityEngine;
using System.Collections;
using WhatPumpkin.Entities;

namespace WhatPumpkin.HiveSwap.MiniGames {

	public abstract class MiniGame : Entity, IPerform {

		/// <summary>
		/// Gets or sets the play order.
		/// </summary>
		/// <value>The play order.</value>

		public int PlayOrder { get; set;}

		// What minigame is active
		static MiniGame _activeMiniGame; 
		static public MiniGame ActiveMiniGame { get { return _activeMiniGame; } }

		// What is the active player for this mini game | This is stored in the player as well
//		[SerializeField] bool _isActive = false; // Is this mini game active?
		static Player _player; 
		static public Player ActivePlayer { get { return _player; } set { _player = value; }  }

        // Use this for initialization
        protected void Start () {
			// Set this to the active minigame
			Activate ();
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		/// <summary>
		/// Activate this mini game instance | TODO: I may not want to allow this to be overridden
		/// </summary>

		public virtual void Activate() {
			//_isActive = true;
			_activeMiniGame = this;
		}

		public abstract void Play();
		public abstract void Restart(); // Handle game restart
		public abstract void Pause(); // Handle game pause
		public abstract void Unpause(); // Handle game unpause
		public abstract void StartGame(); // Handle the start of the game
		public abstract void Stop ();

		public virtual void EndGame() {
		
			if (FinishedPlaying != null) {
				FinishedPlaying(this, null);
			}

		}


        public event EventHandler FinishedPlaying;
        

	}
}
