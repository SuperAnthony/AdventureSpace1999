#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - July 3, 2015
#endregion 

#region using
using UnityEngine;
using WhatPumpkin.Sgrid.Characters;
using WhatPumpkin.Sgrid.Markers;
#endregion

/// <summary>
/// Dead zone. If the active player falls through this dead zone then respawn the player.
/// </summary>

namespace WhatPumpkin {

	public class DeadZone : MonoBehaviour {

		[SerializeField] SpawnPoint _spawnPoint;

		/// <summary>
		/// Raises the trigger enter event.
		/// </summary>
		/// <param name="col">Col.</param>

		void OnTriggerEnter(Collider col) {
		
						// Nothing to do if there is no referenced spawn point; return
			if(_spawnPoint == null){return;}


			// Spawn the player				 if the player lands in this dead zone and is active
		
			if (col != null) {
			
				PlayerCharacter pc = col.GetComponent<PlayerCharacter>();
			
				if(pc != null && pc.IsActive) {
				
					_spawnPoint.Activate();

				}

			}

		
		}


	}
}