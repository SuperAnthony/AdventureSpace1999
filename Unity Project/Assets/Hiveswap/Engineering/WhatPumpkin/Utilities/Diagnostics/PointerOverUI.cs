using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PointerOverUI : MonoBehaviour {

	void Update() {

		Debug.Log ("Pointer Over Game Object: " + this.GetComponent<EventSystem> ().IsPointerOverGameObject ());

	}

}
