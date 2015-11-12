using System;
using WhatPumpkin.Entities;

public interface ISceneManager {

	#region public events

	/// <summary>
	/// Occurs when scene load starts.
	/// </summary>
	
	event EventHandler SceneLoadStart;
	
	/// <summary>
	/// Occurs when the scene has completed loading
	/// </summary>
	
	event EventHandler SceneLoadEnd;

	/// <summary>
	/// Loads the scene.
	/// </summary>
	/// <param name="scene">Scene.</param>
	/// <param name="spawnPointName">Spawn point name.</param>

	void LoadScene(string scene, string spawnPointName = "",string preloaderScene = "");

	/// <summary>
	/// Adds the scene entity.
	/// </summary>
	/// <param name="entity">Entity.</param>

	void AddSceneEntity(Entity entity);

	#endregion

}
