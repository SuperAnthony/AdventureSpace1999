#region using
#endregion


namespace WhatPumpkin.Entities {

	/// <summary>
	/// ISpawnable. For objects that can spawn
	/// </summary>

	public interface ISpawnable {

		/// <summary>
		/// Spawn at the specified spawnPoint.
		/// </summary>
		/// <param name="spawnPoint">Spawn point.</param>
		
		void Spawn(ISpawnPoint spawnPoint, bool rotate = false);



	}
}
