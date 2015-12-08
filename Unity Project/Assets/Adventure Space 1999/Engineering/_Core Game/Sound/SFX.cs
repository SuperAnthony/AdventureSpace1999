#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Sergio I. Nizama
// Date Created - June 3rd, 2015
#endregion 

using System;
using UnityEngine;

namespace WhatPumpkin.Sound
{

    /// <summary>
    /// Non-MonoBehaviour sound type.
    /// </summary>

    [System.Serializable]
    public class SFX : Keyed, ISFX, IFadeable
    {
        #region Fields

        /// <summary>
        /// Eventhandler that objects can subscribe to.
        /// </summary>
        public event EventHandler FinishedPlaying;

        /// <summary>
        /// Ending time of the fade curve relative to the time the fade began.
        /// </summary>
        protected double _fadeEndTime;

        /// <summary>
        /// Signals whether sound is currenlty fading.
        /// </summary>
        protected bool _isFading = false;

        /// <summary>
        /// Used for preventing multiple Play() calls on fade-in track once fading begins.
        /// </summary>
        protected bool _isFadingIn = false;

        /// <summary>
        /// Used to distiguish the fade curve as either a fade-out or fade-in curve.
        /// </summary>
        protected bool _fadeCurveType = false;

        /// <summary>
        /// Used to reference the fade curve passed into BeginFade() while being subscribed to EventManager.OnUpdate.
        /// </summary>
        [SerializeField] protected AnimationCurve _fadeCurve;
        
        /// <summary>
        /// The actual audio clip of this sound type.
        /// </summary>
        [SerializeField] protected AudioClip _audioClip;

		/// <summary>
		/// The audio source.
		/// </summary>

		protected SgridAudioSource _audioSource;

        /// <summary>
        /// Custom audio source type for custom audio types.
        /// </summary>

		public SgridAudioSource AudioSource { 
		
			get {
				return _audioSource;
			}
			set {

				_audioSource = value;
				_audioSource.AudioSource.clip = _audioClip;
			
			}

		}

        #endregion

        #region Properties

		/// <summary>
		/// Gets or sets the play order. The play action itself will use this to keep track of which object is getting played when. 
		/// This is being used as a way or IDing the object
		/// </summary>
		/// <value>The play order.</value>
		
		
		public int PlayOrder { get; set;}


        /// <summary>
        /// Returns the key of this sound.
        /// </summary>
        public override string Key { get { return _key; } }

        /// <summary>
        /// Returns the audio clip of this sound type.
        /// </summary>
        public AudioClip AudioClip { get { return _audioClip; } }

        /// <summary>
        /// Returns whether the sound is currently fading or not.
        /// </summary>
        public bool IsFading { get { return _isFading; } }

        /// <summary>
        /// Gets the audio source of the SgridAudioSource.
        /// </summary>
		/// 
        
		/*
		protected AudioSource AudioSource
        {
            get
            {
                if (SoundTypeAudioSource == null)
                {
//                    SoundTypeAudioSource = GameObject.Find(_key).GetComponent<SgridAudioSource>();
                }
				return null;
            }
        }*/

        /// <summary>
        /// Signals whether the audio source is currently playing.
        /// </summary>
        public bool IsPlaying
        {
            get
            {
				if (AudioSource == null) return false;
                
				return AudioSource.IsPlaying;
				//return AudioSource.isPlaying;
            }
        }

        #endregion

        #region Constructors
        protected SFX() { }

        protected SFX(AudioClip audioClip, SgridAudioSource audioSourceReference)
        {
            _audioClip = audioClip;
            AudioSource = audioSourceReference;
        }

        protected SFX(AudioClip audioClip, SgridAudioSource audioSourceReference, string key)
        {
            _audioClip = audioClip;
            AudioSource = audioSourceReference;
            _key = key;
        }
		#endregion

        #region Methods

        /// <summary>
        /// Unsubscribe from all events.
        /// </summary>
        protected virtual void UnsubscrbieEvents()
        {
            SoundManager.Instance.StopAllCurrentPlaying -= Stop;
            AudioSource.FinishedPlayback -= UnsubscrbieEvents;
        }

        /// <summary>
        /// Plays the audio clip of this sound type.
        /// </summary>
        public virtual void Play()
        {
            AudioSource.FinishedPlayback += UnsubscrbieEvents;
            AudioSource.AudioSource.clip = _audioClip;
            AudioSource.Play();
            SoundManager.Instance.StopAllCurrentPlaying += Stop;
        }

        /// <summary>
        /// Stops the audio clip playback.
        /// </summary>
        public virtual void Stop()
        {
            AudioSource.Stop();
            UnsubscrbieEvents();
            //SoundManager.Instance.StopAllCurrentPlaying -= Stop;
        }
        
        /// <summary>
        /// Unsubscribes from EventManager.OnUpdate, effectively stopping the fade.
        /// Sound type will continue to play at the volume level the fade was stopped at if volume level is above 0.
        /// </summary>
        public virtual void StopFade()
        {
            _isFading = false;
            //Debug.Log("Stopped Fading.");
            EventManager.OnUpdate -= StartFade;
        }

        /// <summary>
        /// Prepares private fields for StartFade()
        /// </summary>
        /// <param name="fadeCurve">The curve used to control volume for fading.</param>
        /// <param name="fadeCurveType">True: Fade-out curve; False: Fade-in curve.</param>
        public virtual void BeginFade(AnimationCurve fadeCurve, bool fadeCurveType)
        {
			if (fadeCurveType && AudioSource == null)
            {
                Play();
            }
            if (fadeCurveType == false)
            {

				AudioSource.AudioSource.volume = 0.0f;
            }
            _fadeCurveType = fadeCurveType;
            _fadeCurve = fadeCurve;
            _fadeEndTime = UnityEngine.AudioSettings.dspTime + fadeCurve[fadeCurve.length - 1].time;
            _isFading = true;
            EventManager.OnUpdate += StartFade;
        }

        /// <summary>
        /// Used for fading out any sound type that is currently playing.
        /// </summary>
        /// <param name="curve"></param>
        public void FadeOut(AnimationCurve curve)
        {
            if (IsPlaying) BeginFade(curve, true);
        }

        /// <summary>
        /// Starts fading-out/in the sound. Subscribes to EventManager.OnUpdate.
        /// </summary>
        protected virtual void StartFade()
        {
			var timeAlongCurve = UnityEngine.AudioSettings.dspTime;
            if (_fadeEndTime >= timeAlongCurve)
            {
                AudioSource.FadeVolume(_fadeCurve.Evaluate(_fadeCurve[_fadeCurve.length - 1].time - (Convert.ToSingle(_fadeEndTime - timeAlongCurve))));
                if (_fadeCurveType == false)
                {
                    if ((_fadeCurve[0].time - 0.001f) <= (_fadeCurve[_fadeCurve.length - 1].time) - Convert.ToSingle(_fadeEndTime - timeAlongCurve) && _isFadingIn == false)
                    {
                        _isFadingIn = true;
                        Play();
                    }
                }
            }
			if (AudioSource.AudioSource.volume <= (0.0f * AudioSource.SoundTypeVolume) && _fadeCurveType == true)//fadeout curve
            {
                StopFade();
                Stop();
            }
			if (AudioSource.AudioSource.volume >= (0.9999997f * AudioSource.SoundTypeVolume) && _fadeCurveType == false)//fadein curve
            {
                StopFade();
            }
        }
        #endregion

        /// <summary>
        /// Used under editor mode to set key name of newly created sfx.
        /// </summary>
        /// <param name="key">The key name of the sfx.</param>
        [ExecuteInEditMode]
        public void SetKey(string key)
        {
            _key = key;
        }

        /// <summary>
        /// Used under editor mode to set audio clip of newly created sfx.
        /// </summary>
        /// <param name="audioClip"></param>
        [ExecuteInEditMode]
        public void SetAudioClip(AudioClip audioClip)
        {
            _audioClip = audioClip;
        }
    }
}