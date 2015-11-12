using UnityEngine;
using System.Collections;

public static class GlobalGamePause
{

    private static bool paused;

    public static bool Paused
    {
        get { return paused; }
        set { paused = value; }
    }

    /*private static GlobalGamePause instance;

    public static GlobalGamePause Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GlobalGamePause>();
            }
            return GlobalGamePause.instance; 
        }
    }*/

    private static void switchPauseValue()
    {
        paused = !paused;
    }

    /// <summary>
    /// Public to be invoked by scene button.
    /// </summary>
    public static void PauseGame()
    {
        switchPauseValue();
        if (paused)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    public static void Pause()
    {
        Time.timeScale = 0.0f;
    }

    public static void UnPause()
    {
        Time.timeScale = 1.0f;
    }
    
    
    /*private void PauseGame(bool pause)
    {
        if (paused == true)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }*/


	// Use this for initialization
	/*void Start () {
	
	}*/
	
	// Update is called once per frame
	/*void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            paused = !paused;
            PauseGame(paused);
        }
	}*/
}
