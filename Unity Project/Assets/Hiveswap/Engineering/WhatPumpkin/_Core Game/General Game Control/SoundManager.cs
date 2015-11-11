#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - December 30, 2014
#endregion 

#region using

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WhatPumpkin.Sound;

#endregion

namespace WhatPumpkin
{

    public class UserSoundSettingArgument : EventArgs
    {
        public float MasterVolume { get; private set; }
        public float AmbientVolume { get; private set; }
        public float MusicVolume { get; private set; }
        public float SfxVolume { get; private set; }
        public bool MasterIsMuted { get; private set; }
        public bool AmbientIsMuted { get; private set; }
        public bool MusicIsMuted { get; private set; }
        public bool SFXIsMuted { get; private set; }

        public UserSoundSettingArgument(float masterVolume, float ambientVolume, float musicVolume, float sfxVolume, bool masterMuted,
                bool ambientMuted, bool musicMuted, bool sfxMuted)
        {
            MasterVolume = masterVolume;
            AmbientVolume = ambientVolume;
            MusicVolume = musicVolume;
            SfxVolume = sfxVolume;
            MasterIsMuted = masterMuted;
            AmbientIsMuted = ambientMuted;
            MusicIsMuted = musicMuted;
            SFXIsMuted = sfxMuted;
        }

        public void SetSettings(float masterVolume, float ambientVolume, float musicVolume, float sfxVolume, bool masterMuted,
                bool ambientMuted, bool musicMuted, bool sfxMuted)
        {
            MasterVolume = masterVolume;
            AmbientVolume = ambientVolume;
            MusicVolume = musicVolume;
            SfxVolume = sfxVolume;
            MasterIsMuted = masterMuted;
            AmbientIsMuted = ambientMuted;
            MusicIsMuted = musicMuted;
            SFXIsMuted = sfxMuted;
        }
    }

    /// <summary>
    /// The game's sound manager.
    /// </summary>
    public class SoundManager : MonoBehaviour
    {

        #region static fields

        /// <summary>
        /// The singleton instance of this object.
        /// </summary>
        private static SoundManager _instance;

        #endregion

		/// <summary>
		/// Occurs when setting change.
		/// </summary>

        public event EventHandler<UserSoundSettingArgument> SettingChange;

        #region fields

        /// <summary>
        /// Master audio volume of the game.
        /// </summary>
        [SerializeField]
        [HideInInspector]
        private float _masterVolume = 1f;

        private float _masterVolumePreMute;

        /// <summary>
        /// Music volume.
        /// </summary>
        [SerializeField]
        [HideInInspector]
        private float _musicVolume = 1f;

        /// <summary>
        /// Ambient volume.
        /// </summary>
        [SerializeField]
        [HideInInspector]
        private float _ambientVolume = 1f;

        /// <summary>
        /// SFX Volume.
        /// </summary>
        [SerializeField]
        [HideInInspector]
        private float _sfxVolume = 1f;

        /// <summary>
        /// Audio source for music types to reference. Index is the primary and index 1 is the secondary.
        /// </summary>
        [SerializeField] private SgridAudioSource[] _musicAudioSources = new SgridAudioSource[2];

        /// <summary>
        /// The scene's game data for accessing the scene's sound collections.
        /// </summary>
        private GameData _sceneData;

        /// <summary>
        /// Collection of fades.
        /// </summary>
        [SerializeField] private List<Fade> _soundFades = new List<Fade>();


        /*/// <summary>
        /// Switch for displaying in game menu for sound settings.
        /// </summary>
        private bool _toggleSoundVolumeMenu = false;*/

        private bool _masterIsMuted = false;

        private bool _musicIsMuted = false;

        private bool _ambientIsMuted = false;

        private bool _sfxIsMuted = false;

        /// <summary>
        /// Delegate for registering volume changes to the game's sound type.
        /// </summary>
        /// <param name="volume">Volume between 0 and 1.</param>
        /// <returns>Volume value based on the base volume of sound type modified by the master volume value.</returns>
        public delegate void VolumeChange (float volume);

        /// <summary>
        /// Volume change event for Music sound type.
        /// </summary>
        public event VolumeChange MusicVolumeChange;

        /// <summary>
        /// Volume change event for Ambient sount type.
        /// </summary>
        public event VolumeChange AmbientVolumeChange;

        /// <summary>
        /// Volume change event for SFXGO sound type.
        /// </summary>
        public event VolumeChange SFXVolumeChange;

        /// <summary>
        /// Delegate for initiallizing fade-out on sound types.
        /// </summary>
        /// <param name="curve"></param>
        public delegate void FadeOutCurrentlyPlaying(AnimationCurve curve);

        /// <summary>
        /// Event used for triggering fade-out on all currently playing Music.
        /// </summary>
        public event FadeOutCurrentlyPlaying FadeOutMusic;

        /// <summary>
        /// Event used for triggering fade-out on all currently playing Ambient.
        /// </summary>
        public event FadeOutCurrentlyPlaying FadeOutAmbient;

        /// <summary>
        /// Event used for triggering fade-out on all currently playing SFX.
        /// </summary>
        public event FadeOutCurrentlyPlaying FadeOutSFX;

        /// <summary>
        /// Event used for triggering fade-out on all currently playing RandomSFX.
        /// </summary>
        public event FadeOutCurrentlyPlaying FadeOutRandomSFX;

        /// <summary>
        /// Event action used for stopping playback on all currently playing sound types.
        /// </summary>
        public event Action StopAllCurrentPlaying;

        public event Action PauseAllCurrentlyPlaying;

        public event Action ResumeAllCurrentlyPaused;

        #endregion

        /// <summary>
        /// Raises the FadeOutCurrentlyPlying event.
        /// </summary>
        public void OnVolumeChange()
        {
            if (MusicVolumeChange != null) MusicVolumeChange.Invoke(MusicVolume);
            if (AmbientVolumeChange != null) AmbientVolumeChange.Invoke(AmbientVolume);
            if (SFXVolumeChange != null) SFXVolumeChange.Invoke(SFXVolume);
        }
        

        #region static properties


        public static SoundManager Instance
        {
            get { return _instance; }
        }

        #endregion

        #region instance properties

        /// <summary>
        /// Gets the collection fo music in the scene.
        /// </summary>
        public List<Music> Musics { get { return _sceneData.Musics; } }

        /// <summary>
        /// Gets the collection of ambient sounds in the scene.
        /// </summary>
        public List<AmbientSound> AmbientSounds { get { return _sceneData.AmbientSounds; } } 

        /// <summary>
        /// Gets the collection of SFXGO in the scene.
        /// </summary>
        public List<SFXGO> Sfxs { get { return _sceneData.Sfxs; } } 

        /// <summary>
        /// Gets the collection of Random SFXGO in the scene.
        /// </summary>
        public List<RandomSFXGO> RandomSfxgoList { get { return _sceneData.RandomSfxs; } }

        /// <summary>
        /// Gets the fades collection
        /// </summary>
        public List<Fade> Fades
        {
            get { return _soundFades; }
        }

        /// <summary>
        /// Gets the available (not currently playing) audio source for music type.
        /// </summary>
        public SgridAudioSource MusicAudioSource
        {
            get
            {
                if (_musicAudioSources[0].AudioSource.isPlaying) return _musicAudioSources[1];
                return _musicAudioSources[0];
            }
        }

        /// <summary>
        /// Gets the collection of music Sgrid audio sources.
        /// </summary>
        public SgridAudioSource[] MusicAudioSources
        {
            get { return _musicAudioSources; }
        }

        /// <summary>
        /// Get/Set the master audio volume.
        /// </summary>
        public float MasterVolume
        {
            get
            {
                if (_masterIsMuted) return _masterVolume*0f;
                return _masterVolume;
            }
            set
            { _masterVolume = value; }
        }

        /// <summary>
        /// Gets the music volume modified by the master volume.
        /// </summary>
        public float MusicVolume
        {
            get
            {
                if (_musicIsMuted) return _musicVolume*MasterVolume*0f;
                return _musicVolume * MasterVolume;
            }
            set { _musicVolume = value; }
        }

        /// <summary>
        /// Gets the ambient volume modified by the master volume.
        /// </summary>
        public float AmbientVolume
        {
            get
            {
                if (_ambientIsMuted) return _ambientVolume*MasterVolume*0f;
                return _ambientVolume * MasterVolume;
            }
            set { _ambientVolume = value; }
        }

        /// <summary>
        /// Gets the sfx volume modified by the master volume.
        /// </summary>
        public float SFXVolume
        {
            get
            {
                if (_sfxIsMuted) return _sfxVolume*MasterVolume*0f;
                return _sfxVolume * MasterVolume;
            }
            set { _sfxVolume = value; }
        }

        #endregion

        #region methods

        // Use this for initialization
        private void Awake()
        {

            // Set singleton instance of this object
            _instance = this;

            //Sets reference to scenedata.
            _sceneData = GameData.SceneData;

        }

        /// <summary>
        /// Populates the ChangeAlphaValue list.
        /// </summary>
        /// <param name="fade">The fade type to aggregate.</param>
        public void AddFade(Fade fade)
        {
            _soundFades.Add(fade);
        }
        
        /// <summary>
        /// Initiates a cross fade of sound types that are IFadeable.
        /// </summary>
        /// <param name="fadeInTracks">Collection of custom sound types to fade in to.</param>
        /// <param name="fadeKey">The key of the fade used for loading the proper fade setting.</param>
        /// <param name="fadeOutMusic">Flag used to signal whether currently playing music type should fade out.</param>
        /// <param name="fadeOutAmbient">Flag used to signal whether currently playing ambient sound type should fade out.</param>
        /// <param name="fadeOutSfx">Flag used to signal whether currently playing SFXGO sound type should fade out.</param>
        /// <param name="fadeOutRandomSfx">Flag used to signal whether currently playing Random SFXGO sound type should fade out.</param>
        public void CrossFade(IFadeable[] fadeInTracks, string fadeKey, bool fadeOutMusic, bool fadeOutAmbient, bool fadeOutSfx,
            bool fadeOutRandomSfx)
        {
            var fadeSetting = GetFade(fadeKey);
            if (fadeSetting == null)
            {
                Debug.Log("fadeSetting is null.");
                return;
            }
            if (fadeOutMusic)
            {
                if (FadeOutMusic != null) FadeOutMusic.Invoke(fadeSetting.FadeOutCurve);
            }
            if (fadeOutAmbient)
            {
                if (FadeOutAmbient != null) FadeOutAmbient.Invoke(fadeSetting.FadeOutCurve);
            }
            if (fadeOutSfx)
            {
                if (FadeOutSFX != null) FadeOutSFX.Invoke(fadeSetting.FadeOutCurve);
            }
            if (fadeOutRandomSfx)
            {
                if (FadeOutRandomSFX != null) FadeOutRandomSFX.Invoke(fadeSetting.FadeOutCurve);
            }
            foreach (var fadeInTrack in fadeInTracks)
            {
                fadeInTrack.BeginFade(fadeSetting.FadeInCurve, false);
            }
        }

        /// <summary>
        /// Initiates a crossfade of two sound types that are IFadeable.
        /// </summary>
        /// <param name="faceOutTrack">Sound that will fade-out.</param>
        /// <param name="fadeInTrack">Sound that will fade-in.</param>
        /// <param name="fadeSettings">ChangeAlphaValue type containing the fade curves.</param>
        private void CrossFade(IFadeable faceOutTrack, IFadeable fadeInTrack, Fade fadeSettings)
        {
            faceOutTrack.BeginFade(fadeSettings.FadeOutCurve, true);
            fadeInTrack.BeginFade(fadeSettings.FadeInCurve, false);
        }

        //TODO: This cross fade method is deprecated. Delete once no longer in use in Room.cs
        /// <summary>
        /// Do not use! Deprecated method, will not do anything..
        /// </summary>
        /// <param name="fadeKey">Key name of the fade to look up.</param>
        /// <param name="audioTrack">Audio track to fade-in.</param>
        /// <param name="additionalTracks">Additonal audio tracks to fade-in.</param>
        public void CrossFade(string fadeKey, IFadeable audioTrack, params IFadeable[] additionalTracks)
        {


            /*ChangeAlphaValue tempFade = GetFade(fadeKey);
            if (tempFade == null)
            {
                Debug.Log("Could not find ChangeAlphaValue with that key.");
                return;
            }
            if (FadeOutCurrent != null) FadeOutCurrent.Invoke(tempFade.FadeOutCurve);
            audioTrack.BeginFade(tempFade.FadeInCurve, false);
            foreach (var track in additionalTracks)
            {
                track.BeginFade(tempFade.FadeInCurve, false);
            }*/
        }


		/// <summary>
		/// Plays the music. - Anthony
		/// </summary>
		/// <param name="music">Music.</param>

		public void PlayMusic(Music music) {
		
			StopAllMusic ();
			music.Play ();

		}

        /// <summary>
        /// Mutes all sound types by muting the master volume.
        /// </summary>
        public void MuteAll()
        {
            if (!_masterIsMuted)
            {
                _masterIsMuted = true;
            }
            else _masterIsMuted = false;
            OnVolumeChange();
        }

        public void MuteAmbient()
        {
            if (!_ambientIsMuted)
            {
                _ambientIsMuted = true;
            }
            else _ambientIsMuted = false;
            if (AmbientVolumeChange != null) AmbientVolumeChange.Invoke(AmbientVolume);
        }

        public void MuteMusic()
        {
            if (!_musicIsMuted)
            {
                _musicIsMuted = true;
            }
            else _musicIsMuted = false;
            if (MusicVolumeChange != null) MusicVolumeChange.Invoke(MusicVolume);
        }

        public void MuteSFX()
        {
            if (!_sfxIsMuted)
            {
                _sfxIsMuted = true;
            }
            else _sfxIsMuted = false;
            if (SFXVolumeChange != null) SFXVolumeChange.Invoke(SFXVolume);
        }



		/// <summary>
		/// Stops all music. - Anthony
		/// </summary>

		public void StopAllMusic() {

			_musicAudioSources [0].Stop ();
			_musicAudioSources [1].Stop ();
		
		}

        /// <summary>
        /// Gets a fade by it's key name from the collection of fades.
        /// </summary>
        /// <param name="fadeKey"></param>
        /// <returns></returns>
        public Fade GetFade(string fadeKey)
        {
            foreach (var fade in _soundFades)
            {
                if (fade.Key == fadeKey)
                {
                    return fade;
                }
            }
            return null;
        }

        /// <summary>
        /// Stops all currently playing sound types.
        /// </summary>
        /// <returns>True: if the sound type was stopped. False: if otherwise.</returns>
        public bool StopAudioPlayback()
        {
            if (StopAllCurrentPlaying != null)
            {
                StopAllCurrentPlaying.Invoke();
                return true;
            }
            return false;
        }

        public bool PauseAudioPlayback()
        {
            if (PauseAllCurrentlyPlaying != null)
            {
                PauseAllCurrentlyPlaying.Invoke();
                return true;
            }
            return false;
        }

        public bool ResumeAudioPlayback()
        {
            if (ResumeAllCurrentlyPaused != null)
            {
                ResumeAllCurrentlyPaused.Invoke();
                return true;
            }
            return false;
        }

        /*public void PauseTest()
        {
            if (PauseAllCurrentlyPlaying != null)
            {
                PauseAllCurrentlyPlaying.Invoke();
            }
        }

        public void ResumeTest()
        {
            if (ResumeAllCurrentlyPaused != null)
            {
                ResumeAllCurrentlyPaused.Invoke();
            }
        }*/

        /// <summary>
        /// Sets in game sounds volume.
        /// </summary>
        public void SetVolumes()
        {
            SgridAudioSource.BaseVolume = _masterVolume;
            /*Debug.Log("Sgrid: " + SgridAudioSource.BaseVolume);
            Debug.Log("Master: " + _masterVolume);*/
            OnVolumeChange();
        }

        /// <summary>
        /// Plays the audio type.
        /// </summary>
        /// <param name="audioType">The audio type containing the audio clip to be played.</param>
        /// <returns> True: if the audio type was played. False: if otherwise</returns>
        public bool PlayAudioTrack(ISFX audioType)
        {
            if (audioType.AudioClip == null)
            {
                Debug.Log("Audio is missing.");
                return false;
            }
            audioType.Play();
            return true;
        }

        void Update()
        {
            /*if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_toggleSoundVolumeMenu)
                {
                    _toggleSoundVolumeMenu = false;
                }
                else _toggleSoundVolumeMenu = true;
            }*/
        }

        /// <summary>
        /// Volume change UI
        /// </summary>
        /*void OnGUI()
        {
            if (_toggleSoundVolumeMenu)
            {
                GUILayout.BeginArea(new Rect(20,20,100,250), new GUIContent("Volume Settings"));
                GUILayout.Label("Master Volume");
                _masterVolume = GUILayout.HorizontalSlider(_masterVolume, 0f, 1f);
                GUILayout.Label("Music Volume");
                _musicVolume = GUILayout.HorizontalSlider(_musicVolume, 0f, 1f);
                GUILayout.Label("Ambient Volume");
                _ambientVolume = GUILayout.HorizontalSlider(_ambientVolume, 0f, 1f);
                GUILayout.Label("SFX Volume");
                _sfxVolume = GUILayout.HorizontalSlider(_sfxVolume, 0f, 1f);
                if (GUILayout.Button("Apply"))
                {
                    SetVolumes();
                    _toggleSoundVolumeMenu = false;
                }
                if (GUILayout.Button("Cancel"))
                {
                    _toggleSoundVolumeMenu = false;
                }
                GUILayout.EndArea();
            }
        }*/
        

#endregion


        //Used to reference sound manager in editor mode for sound creation purposes.
        [ExecuteInEditMode]
        public static void Initialize()
        {
            _instance = GameObject.FindObjectOfType<SoundManager>();
        }

       

    }


}