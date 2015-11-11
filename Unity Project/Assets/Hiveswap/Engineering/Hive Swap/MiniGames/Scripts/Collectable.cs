using UnityEngine;
using System.Collections;

// Use this for all mini game types


namespace WhatPumpkin.HiveSwap.MiniGames {

	public class Collectable : MonoBehaviour, ICollectable {

		// Collectable features that may be common to all mini games
		public enum MovementType {None, FollowPlayer, LeftAndRight, UpAndDown};

		// Properties
		[SerializeField] MovementType _movementType;
		[SerializeField] float _collectableSpeed = 0F;
//		bool _followsPlayer = false;

		// Bonuses
		[SerializeField] float _speedBoost = 0F; 
		public float SpeedBoost { get { return _speedBoost; } }

		/// <summary>
		/// Move this instance.
		/// </summary>

		protected void Move() {

			// If this object follows the player
			if (_movementType == MovementType.FollowPlayer) {
				Follow(MiniGame.ActivePlayer.Position);
			}
		}

		void Follow(Vector3 to) {
			//Debug.Log ("Follow: " + MiniGame.ActivePlayer);
			transform.position = Vector3.Lerp(transform.position, to, _collectableSpeed * Time.deltaTime);
		}


		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {

			Debug.Log ("Moving");

			// Use this objects move action
			Move ();
		}
	
	}

}
