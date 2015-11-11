using UnityEngine;
using System.Collections;

namespace WhatPumpkin.HiveSwap.MiniGames {

	public abstract class Player : MonoBehaviour {

		static Player _activePlayer; // What is the active player for our mini game. This should not be touched | Set in the property
		static public Player ActivePlayer { 
			get { 
				return _activePlayer; 
			} 
			set 
			{ 
				// Set active player
				_activePlayer = value;
				// Make sure the active mini game is aware of the active player
				MiniGame.ActivePlayer = _activePlayer;
			} 
		}

		public virtual Vector3 Position { get { return transform.position; } }


		// Use this for initialization
		protected virtual void Start () {

			// When the game starts make sure to set the active player to this
			ActivePlayer = this;

		}
		
		// Update is called once per frame
		void Update () {
		
		}
	}
}

