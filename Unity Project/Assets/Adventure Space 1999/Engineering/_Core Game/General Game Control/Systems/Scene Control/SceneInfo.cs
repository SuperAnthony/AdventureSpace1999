#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - July 28, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#endregion

using WhatPumpkin.Sgrid; // TODO: Remove this when I abstract the data

namespace WhatPumpkin {

	[System.Serializable]

	public class SceneInfo : Keyed {

		/// <summary>
		/// The _scene objects.
		/// </summary>

		[SerializeField] List<SceneObject> _sceneObjects = new List<SceneObject>();

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>

		public override string Key { get { return _key; } }

		/// <summary>
		/// Gets the scene objects.
		/// </summary>
		/// <value>The scene objects.</value>

		public List<SceneObject> SceneObjects { get { return _sceneObjects; } }

		/// <summary>
		/// Initializes a new instance of the <see cref="WhatPumpkin.SceneInfo"/> class.
		/// I'm using this constructor primarily for receiving save/load data.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="sceneObjects">Scene objects.</param>
		/*
		public SceneInfo(string key, IEnumerable sceneObjects) {
				
			_key = key;

		}*/

		/// <summary>
		/// Initializes a new instance of the <see cref="WhatPumpkin.SceneInfo"/> class.
		/// </summary>
		/// <param name="key">Key.</param>

		public SceneInfo(string key) {
		
			_key = key;

			// TODO: Abstract this data

			foreach (Entity entity in  GameObject.FindObjectsOfType<Entity>()) {
				
				
							ISceneObject<string> item = (ISceneObject<string>)entity;
				
							if (item != null) {
									try {
						
											SceneObject sceneObject = new SceneObject ();

											_sceneObjects.Add (sceneObject);

											sceneObject.ReceiveData(item);
									} catch (System.Exception e) {
											Debug.LogException(e);
									}
							}

			}
		}


		/// <summary>
		/// Receives the scene object change.
		/// </summary>
		/// <param name="iSceneObject">I scene object.</param>

		public void ReceiveSceneObjectChange(ISceneObject<string> iSceneObject) {

//			Debug.Log ("Receive Change : " + iSceneObject.Key);

					foreach (SceneObject sceneObject in _sceneObjects) {

						if(sceneObject.Key == iSceneObject.Key) {

							sceneObject.ReceiveData(iSceneObject);
						}
								
				
					}
			}


	}
}
