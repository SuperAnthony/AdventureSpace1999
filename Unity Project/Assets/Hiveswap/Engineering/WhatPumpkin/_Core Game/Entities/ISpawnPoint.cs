#region using
using UnityEngine;
using WhatPumpkin.CameraManagement;
#endregion


namespace WhatPumpkin.Entities {

	/// <summary>
	/// ISpawnable. For objects that can spawn
	/// </summary>

	public interface ISpawnPoint : IKeyed {

		/// <summary>
		/// Gets a value indicating whether the active pc gets spawned here
		/// </summary>
		/// <value><c>true</c> if spawn active P; otherwise, <c>false</c>.</value>
		
		bool SpawnActivePC { get; }
		
		/// <summary>
		/// Gets the camera node.
		/// </summary>
		/// <value>The camera node.</value>
		
		CameraNode CameraNode { get; } 

		/// <summary>
		/// Gets the transform.
		/// </summary>
		/// <value>The transform.</value>

		Transform transform { get; }



	}
}
