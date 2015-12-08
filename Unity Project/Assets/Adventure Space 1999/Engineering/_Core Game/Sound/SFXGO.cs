#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Sergio Nizama
// Date Created - May 28th 2015
#endregion

using System;
using UnityEngine;
using WhatPumpkin.Sgrid;

namespace WhatPumpkin.Sound
{
    /// <summary>
    /// 3D/Positional Sound
    /// </summary>

    [RequireComponent(typeof(SgridAudioSource))]
    [System.Serializable]
    public class SFXGO : Entity, ISFX, IFadeable
    {
		/// <summary>
		/// Gets or sets the play order. The play action itself will use this to keep track of which object is getting played when. 
		/// This is being used as a way or IDing the object
		/// </summary>
		/// <value>The play order.</value>

		public int PlayOrder { get; set;}


        public event EventHandler FinishedPlaying;

        /// <summary>
        /// Used for setting the SgridAudioSource to "SFX" (Monobehaviour) at Start.
        /// </summary>
        protected const int SFX = 2;

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
        protected AnimationCurve _fadeCurve;

        /// <summary>
        /// Indicates whether the SFXGO has been played.
        /// </summary>
        protected bool _hasBeenPlayed;

		protected bool _soundStarted = false;

        /// <summary>
        /// The audio clip used for playback.
        /// </summary>
        [SerializeField]
        protected AudioClip _audioClip;

        /// <summary>
        /// The custom audio source.
        /// </summary>
        protected SgridAudioSource _sgridAudioSource;

        /// <summary>
        /// Gets the custom audio source.
        /// </summary>
        public SgridAudioSource SgrdAudioSource
        {
            get
            {
                if (_sgridAudioSource == null) _sgridAudioSource = gameObject.AddComponent<SgridAudioSource>();
                return _sgridAudioSource;
            }
        }

        /// <summary>
        /// Gets and sets the audio clip.
        /// </summary>
        public AudioClip AudioClip
        {
            get { return _audioClip; }
            set { _audioClip = value; }
        }

        /// <summary>
        /// Returns whether the audio clip has been played or not.
        /// </summary>
        public bool HasBeenPlayed
        {
            get { return _hasBeenPlayed; }
            //set { _hasBeenPlayed = value; }
        }

        //TODO: Add property that returns perform.

        void Awake()
        {
            _sgridAudioSource = gameObject.GetComponent<SgridAudioSource>();
        }

        void Start()
        {
            _sgridAudioSource.AudioSource.playOnAwake = false;
            _sgridAudioSource.SetAudioSourceType(SFX);
        }

        public void Update()
        {
			if (_soundStarted && !_sgridAudioSource.AudioSource.isPlaying) {
				OnFinishedPlaying ();
			}




         
        }

        /// <summary>
        /// Plays the audio clip and subscribes to events.
        /// </summary>
        public virtual void Play()
        {
            PlayGameAudio();
            SoundManager.Instance.FadeOutSFX += FadeOut;
            SoundManager.Instance.StopAllCurrentPlaying += Stop;
        }

        public void Begin()
        {
            BeginGameAudio();
        }

        public void Stop()
        {
            StopGameAudio();
        }

        public void Pause()
        {
            PauseGameAudio();
        }

        public void Resume()
        {
            ResumeGameAudio();
        }

        /// <summary>
        /// Unsubscribe from all events.
        /// </summary>
        protected virtual void UnsubscribeEvents()
        {
            SoundManager.Instance.FadeOutSFX -= FadeOut;
            SoundManager.Instance.StopAllCurrentPlaying -= Stop;
            _sgridAudioSource.FinishedPlayback -= UnsubscribeEvents;
        }

        /// <summary>
        /// Plays the audio clip.
        /// </summary>
        private void PlayGameAudio()
        {
            if (AudioClip != null && !_sgridAudioSource.AudioSource.isPlaying)
            {
                _sgridAudioSource.AudioSource.clip = AudioClip;
                _sgridAudioSource.FinishedPlayback += UnsubscribeEvents;
                _sgridAudioSource.Play();
				_soundStarted = true;
                //_hasBeenPlayed = true;
            }
            // else throw new NullReferenceException("Audio clip not attached."); // Sergio, why are you throwing a null reference exception here? I see several reasons not to
        }

        /// <summary>
        /// 
        /// </summary>
        private void BeginGameAudio()
        {
            if (AudioClip == null) return;
            _sgridAudioSource.AudioSource.clip = AudioClip;
            _sgridAudioSource.AudioSource.Stop();
            _sgridAudioSource.AudioSource.Play();
            _hasBeenPlayed = true;
        }

        /// <summary>
        /// 
        /// </summary>
        private void StopGameAudio()
        {
            _sgridAudioSource.Stop();
            _hasBeenPlayed = false;
            OnFinishedPlaying();
            UnsubscribeEvents();
        }

        /// <summary>
        /// Pauses the audio clip playback.
        /// </summary>
        private void PauseGameAudio()
        {
            _sgridAudioSource.AudioSource.Pause();
        }

        /// <summary>
        /// Resumes the playback of pased audio clip.
        /// </summary>
        private void ResumeGameAudio()
        {
            _sgridAudioSource.AudioSource.UnPause();
        }

        /// <summary>
        /// Make sure that subscribers to this, unsubscribe at the end of execution.
        /// </summary>
        private void OnFinishedPlaying()
        {

//			Debug.Log ("On Finished Playing");

			_soundStarted = false;

            if (FinishedPlaying != null)
                FinishedPlaying.Invoke(this, null);
        }

        /// <summary>
        /// Prepares fields for fading.
        /// </summary>
        /// <param name="fadeCurve">The curve containing the fade setting.</param>
        /// <param name="fadeCurveType">True = FadeOut; False = FadeIn</param>
        public void BeginFade(AnimationCurve fadeCurve, bool fadeCurveType)
        {
            if (fadeCurveType && _sgridAudioSource.AudioSource == null)
            {
                Play();
            }
            if (fadeCurveType == false)
            {
                _sgridAudioSource.AudioSource.volume = 0.0f;
            }
            _fadeCurveType = fadeCurveType;
            _fadeCurve = fadeCurve;
            _fadeEndTime = AudioSettings.dspTime + fadeCurve[fadeCurve.length - 1].time;
            _isFading = true;
            EventManager.OnUpdate += StartFade;
        }

        /// <summary>
        /// Starts fading the volume of the audio clip.
        /// </summary>
        protected virtual void StartFade()
        {
            var timeAlongCurve = AudioSettings.dspTime;
            if (_fadeEndTime >= timeAlongCurve)
            {
                _sgridAudioSource.FadeVolume(_fadeCurve.Evaluate(_fadeCurve[_fadeCurve.length - 1].time - (Convert.ToSingle(_fadeEndTime - timeAlongCurve))));
                if (_fadeCurveType == false)
                {
                    if ((_fadeCurve[0].time - 0.001f) <= (_fadeCurve[_fadeCurve.length - 1].time) - Convert.ToSingle(_fadeEndTime - timeAlongCurve) && _isFadingIn == false)
                    {
                        _isFadingIn = true;
                        Play();
                    }
                }
            }
            if (_sgridAudioSource.AudioSource.volume <= (0.0f * _sgridAudioSource.SoundTypeVolume) && _fadeCurveType == true)//fadeout curve
            {
                StopFade();
                Stop();
            }
            if (_sgridAudioSource.AudioSource.volume >= (0.9999997f * _sgridAudioSource.SoundTypeVolume) && _fadeCurveType == false)//fadein curve
            {
                StopFade();
            }
        }

        /// <summary>
        /// Method that subscribes to SoundMangaer to initiate fade out of currently playing audio clip.
        /// </summary>
        /// <param name="curve"></param>
        public void FadeOut(AnimationCurve curve)
        {
            if (_sgridAudioSource.AudioSource.isPlaying) BeginFade(curve, true);
        }

        /// <summary>
        /// Stops fading of volume.
        /// </summary>
        public virtual void StopFade()
        {
            //Debug.Log("Stoped Fading.");
            _isFading = false;
            EventManager.OnUpdate -= StartFade;
            SoundManager.Instance.FadeOutSFX -= FadeOut;
        }

        [ExecuteInEditMode]
        public virtual void SetKey(string key)
        {
            _key = key;
        }
        [ExecuteInEditMode]
        public virtual void SetAudioClip(AudioClip audioClip)
        {
            _audioClip = audioClip;
        }
    }
}


