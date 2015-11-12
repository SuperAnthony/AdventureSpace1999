using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour {

	public Button button;
	public Vector2 expandScaleValue = new Vector2(1.5f, 1.5f);
	public Vector2 defaultScaleValue = new Vector2(1f, 1f);
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void ScaleOnHover(){
		button.transform.localScale = expandScaleValue;
	}
	public void RevertScaleOffHover(){
		button.transform.localScale = defaultScaleValue;
	}
}
