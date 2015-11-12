#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Sergio Nizama
// Date Created - March 11th, 2015
#endregion

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TypingTextEffect : MonoBehaviour {

	public Text text;
	public float letterPause = 0.05f;
	public Font font;
    private string _message;

    public TypingTextEffect()
    {
        
    }

    public TypingTextEffect(string message, Text textPromptObject )
    {
        _message = message;
        text = textPromptObject;
    }

    public string Message
    {
        get { return _message; }
        set { _message = value; }
    }

    public Text TextPrompt
    {
        get { return text; }
        set { text = value; }
    }

    public void PlayText() {
		StartCoroutine(BlurbText());
	}

    public void PlayText(string message, Text textPromptObject)
    {
        _message = message;
        text = textPromptObject;
        StartCoroutine(BlurbText());
    }

	IEnumerator BlurbText(){
		foreach (char letter in _message.ToCharArray()){
			text.text += letter;
			yield return new WaitForSeconds(letterPause);
		}
	}
}
