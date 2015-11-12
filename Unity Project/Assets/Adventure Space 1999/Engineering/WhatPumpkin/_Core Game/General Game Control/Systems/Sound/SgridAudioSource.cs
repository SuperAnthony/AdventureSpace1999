#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Sergio Nizama
// Edits by Anthony Paul Albino
#endregion 

using System;
using UnityEngine;


namespace WhatPumpkin {

	[RequireComponent(typeof(AudioSource))]
	public class SgridAudioSource : MonoBehaviour, IAudioSource
	{
	    /// <summary>
	    /// Represents the type of audio source.
	    /// </summary>
	    public enum SoundType
	    {
	        Ambient,
	        Music,
	        SFX
	    };

	    /// <summary>
	    /// Determined which event to subscribe to in soundmanager.
	    /// </summary>
	    [SerializeField] public SoundType _soundType; //TODO: make it private.

	    /// <summary>
	    /// The audio source used to play custom sound type's audio clip.
	    /// </summary>
	    private AudioSource _audioSource;

	    /// <summary>
	    /// Base/Master Volume shared amongst all custom audio source types.
	    /// </summary>
	    private static float _baseVolume = 1f;

	    /// <summary>
	    /// The volume of the sound type this Sgrid Audio Source is being referenced by.
	    /// </summary>
	    private float _soundTypeVolume = 1f;

	    /// <summary>
	    /// Setter for the base volume.
	    /// </summary>
	    public static float BaseVolume { get {return _baseVolume;} set { _baseVolume = value; } }

	    /// <summary>
	    /// Signals whether the audio clip is playing.
	    /// </summary>
	    private bool _isPlaying = false;

	    /// <summary>
	    /// Gets the monobehaviour audio source associated with this custom audio source type.
	    /// </summary>
	    public AudioSource AudioSource {
	        get
	        {
	            if (_audioSource == null)
	            {
	                _audioSource = gameObject.AddComponent<AudioSource>();
	            }
	            return _audioSource;
	        }
	    }

	    /// <summary>
	    /// Gets the sound type volume that is controlloed/set by the sound manager/user.
	    /// </summary>
	    public float SoundTypeVolume
	    {
	        get { return _soundTypeVolume; }
	    }

	    /// <summary>
	    /// Signals whether the audio clip is playing.
	    /// </summary>
	    public bool IsPlaying
	    {
	        get { return _isPlaying; }
	    }

	    /// <summary>
	    /// Event that is fired off when the audio clip finishes playing.
	    /// </summary>
	    public Action FinishedPlayback;

	    void Awake()
	    {
	        _audioSource = gameObject.GetComponent<AudioSource>();
	        //TODO: Do we want play on awake?
	#if UNITY_EDITOR
	        SoundManager.Initialize();
	#endif
	    }

	    // Use this for initialization
	    void Start ()
		{
		    switch ((int)_soundType)
		    {
	            case 0:
	                SoundManager.Instance.AmbientVolumeChange += ChangeVolume;
	                //Debug.Log("Ambient Subscribed.");
		            break;
	            case 1:
	                SoundManager.Instance.MusicVolumeChange += ChangeVolume;
	                //Debug.Log("Music Subscribed.");
	                break;
	            case 2:
		            SoundManager.Instance.SFXVolumeChange += ChangeVolume;
	                //Debug.Log("SFX Subscribed.");
		            break;
		    }
	        SoundManager.Instance.PauseAllCurrentlyPlaying += Pause;
	        SoundManager.Instance.ResumeAllCurrentlyPaused += Resume;
		}

	    void Update()
	    {
			// If this is a music source then always follow the main camera
			// Hey Sergio, I added this code here to update the music
			// One thing we could also do is use delegates - one specific for each sound type and have an update method for sfx,music and ambient that we can switch between
			// Not necessary since this is one small condition that I added, but just wanted to let you know that I could have handled it that way as well
			if (_soundType == SoundType.Music && GameController.CamManager.ActiveCamera != null) { 
								this.transform.position = new Vector3 (GameController.CamManager.ActiveCamera.transform.position.x,
			                                    GameController.CamManager.ActiveCamera.transform.position.y,
			                                    GameController.CamManager.ActiveCamera.transform.position.z);
			}

	        if (_isPlaying)
	        {
	            if (!_audioSource.isPlaying)
	            {
	                if (FinishedPlayback != null) FinishedPlayback.Invoke();
	                _isPlaying = false;
	            }
	        }
	    }

	    public void UnsubscribeEvents()
	    {
            switch ((int)_soundType)
            {
                case 0:
                    SoundManager.Instance.AmbientVolumeChange -= ChangeVolume;
                    //Debug.Log("Ambient Subscribed.");
                    break;
                case 1:
                    SoundManager.Instance.MusicVolumeChange -= ChangeVolume;
                    //Debug.Log("Music Subscribed.");
                    break;
                case 2:
                    SoundManager.Instance.SFXVolumeChange -= ChangeVolume;
                    //Debug.Log("SFX Subscribed.");
                    break;
            }
            SoundManager.Instance.PauseAllCurrentlyPlaying -= Pause;
            SoundManager.Instance.ResumeAllCurrentlyPaused -= Resume;
        }

	    /// <summary>
	    /// Plays the audio clip.
	    /// </summary>
	    public void Play()
	    {
	        _audioSource.Play();
	        _isPlaying = _audioSource.isPlaying;
	    }

	    /// <summary>
	    /// Changes volume according to parameter value and internal base/master volume.
	    /// Used mainly for events.
	    /// </summary>
	    /// <param name="soudTypeVolume">Volume value between 0 and 1.</param>
	    public void ChangeVolume(float soudTypeVolume)
	    {
	        _soundTypeVolume = soudTypeVolume;
	        _audioSource.volume = _soundTypeVolume;
	    }

	    /// <summary>
	    /// Used for changing volume during fades.
	    /// </summary>
	    /// <param name="volume"></param>
	    public void FadeVolume(float volume)
	    {
	        _audioSource.volume = _soundTypeVolume * volume;
	    }

	    /// <summary>
	    /// Stops the audio source playback.
	    /// </summary>
	    public void Stop()
	    {
	        _audioSource.Stop();
	        _isPlaying = _audioSource.isPlaying;
	    }

	    public void Pause()
	    {
            if (_audioSource.isPlaying)
	        {
                _audioSource.Pause();
                _isPlaying = _audioSource.isPlaying;
            }
	    }

	    public void Resume()
	    {
	        if (!_audioSource.isPlaying)
	        {
	            _audioSource.UnPause();
	            _isPlaying = _audioSource.isPlaying;
	        }
	    }

	    /// <summary>
	    /// Used for when using cutom sound type editors.
	    /// </summary>
	    /// <param name="soundType">0 = Ambient; 1 = Music; 2 = SFX</param>
	    [ExecuteInEditMode]
	    public void SetAudioSourceType(int soundType)
	    {
	        _soundType = (SoundType)soundType;
	    }


	}
}
