using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// ReferencedSceneObjectCollection. Scene manager will point to this object to reference any objects the player may interact with in the scene
/// This object will automatically be created in the editor when an enviroment artist associates an object with a scene
/// Each scene will have it's own collection that I do not want to get replaced by the previous scene 
/// </summary>

public class ReferencedSceneObjectCollection : MonoBehaviour {

	#region static fields

	static string  _collectionName = "Referenced Scene Object Collection";

	#endregion

	#region fields

	/// <summary>
	/// Gets the name of the collection.
	/// </summary>
	/// <value>The name of the collection.</value>

	public static string CollectionName { get { return _collectionName; } }

	/// <summary>
	/// The collection of referenced game objects in the scene.
	/// </summary>

	[SerializeField] List<GameObject> _collection = new List<GameObject>(); 

	#endregion

	#region properties

	/// <summary>
	/// Gets the collection.
	/// </summary>
	/// <value>The collection.</value>

	public List<GameObject> Collection { get { return _collection; } }

	#endregion

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Adds the game object to the collection.
	/// </summary>
	/// <param name="gObject">G object.</param>

	public void AddObject(GameObject gObject) {
	
		if(gObject != null) {
			_collection.Add (gObject);
		}
	}


	/// <summary>
	/// Finds the game object of a given name.
	/// </summary>
	/// <returns>The game object.</returns>
	/// <param name="objectName">Object name.</param>

	public GameObject FindGameObject(string objectName) {
	
		
		// Search through the referenced objects
		foreach(GameObject gObject in _collection) {
			
			// If match is found then return the object
			if(objectName == gObject.name) {
				return gObject;
			}
			
		}

		return null;


	}

	public void Remove(GameObject gObject) {
	
		_collection.Remove (gObject);

	}

	public bool HasObject(GameObject gObject) {
		
		// Search through the referenced objects

		foreach(GameObject gO in _collection) {
			
			// If match is found then return true
			if(gObject == gO) {
				return true;
			}
			
		}
		
		// No match was found, return false
		return false;
		
	}

	public void Add(GameObject gObject) {

		_collection.Add (gObject);
	
	}
}
