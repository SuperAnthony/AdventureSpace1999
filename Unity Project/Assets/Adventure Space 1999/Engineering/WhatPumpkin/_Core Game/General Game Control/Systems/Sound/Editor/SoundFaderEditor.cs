#if UNITY_EDITOR
#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Sergio Nizama
// Date Created - June 4th, 2015
#endregion

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace WhatPumpkin.Sound
{
    /// <summary>
    /// Editor window for creating new fades and editing existing fades in the sound manager.
    /// </summary>

    public class SoundFaderEditor : EditorWindow
    {

        /// <summary>
        /// Used for loading in exisiting fade from sound manager.
        /// </summary>
        private Fade _existingFadeToEdit;

        /// <summary>
        /// Index used with collection fo Fades in sound manager.
        /// </summary>
        private int _existingFadeIndex = 0;

        /// <summary>
        /// Used to reference SoundManager from scene.
        /// </summary>
        private SoundManager _soundManager;

        /// <summary>
        /// Audio clip used for testing fade-out.
        /// </summary>
        private AudioClip _fadeOutAudioClip;

        /// <summary>
        /// Audio clip used for testing fade-in.
        /// </summary>
        private AudioClip _fadeInAudioClip;

        /// <summary>
        /// Audio source that fades sound out.
        /// </summary>
        private AudioSource _fadeOutAudioSource;

        /// <summary>
        /// Audio source that fades sound in.
        /// </summary>
        private AudioSource _fadeInAudioSource;

        /// <summary>
        /// Holds the scroll are position.
        /// </summary>
        private Vector2 _scrollArea;

        /// <summary>
        /// Contains two helper class that assist in testing out fades.
        /// </summary>
        private SoundFadeEditorHelper[] _helpers = new SoundFadeEditorHelper[2];
        

        [MenuItem("HiveSwap/Create/Sound Fades")]
        private static void Init()
        {
            /*if (EditorApplication.currentScene != "Assets/Hiveswap/Scenes/Persistent Data.unity")
            {
                Debug.Log("This is not the Persistent Data Scene.");
            }
            else
            {
                SoundFaderEditor faderEditorWindow = (SoundFaderEditor) GetWindow(typeof (SoundFaderEditor));
                faderEditorWindow.Show();
            }*/
			SoundFaderEditor faderEditorWindow = (SoundFaderEditor) GetWindow(typeof (SoundFaderEditor));
			faderEditorWindow.Show();
        }

        void OnEnable()
        {
            SoundManager.Initialize();
            _soundManager = SoundManager.Instance;
            CheckForMusicAudioSource();
            autoRepaintOnSceneChange = true;//Set to true for Update function from SoundFadeEditorHelper to be able to work properly under editor mode.
        }

        /// <summary>
        /// Removes the sound fade editor helper from the music audio source when the editor window is closed.
        /// </summary>
        private void OnDestroy()
        {
            DestroyImmediate(_soundManager.MusicAudioSources[0].gameObject.GetComponent<SoundFadeEditorHelper>());
            DestroyImmediate(_soundManager.MusicAudioSources[1].gameObject.GetComponent<SoundFadeEditorHelper>());
            _fadeOutAudioSource.Stop();
            _fadeInAudioSource.Stop();
        }

        private void OnGUI()
        {
            _soundManager.transform.position += Vector3.forward;
            EditorGUILayout.LabelField("Sound ChangeAlphaValue Editor", EditorStyles.boldLabel);
            if (_soundManager == null)
            {
                EditorGUILayout.HelpBox("Sound Manager is not present.", UnityEditor.MessageType.Warning);
                return;
            }
            _scrollArea = EditorGUILayout.BeginScrollView(_scrollArea);
            if (_soundManager.Fades.Count > 0)
            {
                List<string> fadeKeys = new List<string>();
                for (int i = 0; i < _soundManager.Fades.Count; ++i)
                {
                    fadeKeys.Add(_soundManager.Fades[i].Key);
                }
                EditorGUILayout.PrefixLabel("Created Fades", EditorStyles.boldLabel);
                _existingFadeIndex = EditorGUILayout.Popup(_existingFadeIndex, fadeKeys.ToArray());
                _existingFadeToEdit = _soundManager.Fades[_existingFadeIndex];
                EditExistingFade();
                if (GUILayout.Button("Create New ChangeAlphaValue"))
                {
                    _soundManager.Fades.Add(new Fade());
                    _existingFadeIndex = _soundManager.Fades.Count - 1;
                }
            }
            else if (GUILayout.Button("Create New ChangeAlphaValue"))
            {
                _soundManager.Fades.Add(new Fade());
            }
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Test out fade", EditorStyles.boldLabel);
            _fadeOutAudioClip =
                EditorGUILayout.ObjectField("ChangeAlphaValue-out", _fadeOutAudioClip, typeof (AudioClip), false) as AudioClip;
            _fadeInAudioClip =
                EditorGUILayout.ObjectField("ChangeAlphaValue-in", _fadeInAudioClip, typeof (AudioClip), false) as AudioClip;
            if (GUILayout.Button("Play/Stop ChangeAlphaValue Test"))
            {
                 if (_fadeOutAudioSource.isPlaying || _fadeInAudioSource.isPlaying)
                 {
                    _fadeOutAudioSource.Stop();
                    _fadeInAudioSource.Stop();
                 }
                 else
                 {
                     _helpers[0].TestFade(_existingFadeToEdit.FadeOutCurve, _fadeOutAudioClip, true);
                     _helpers[1].TestFade(_existingFadeToEdit.FadeInCurve, _fadeInAudioClip, false);
                 }
            }
            EditorGUILayout.EndScrollView();
        }

        /// <summary>
        /// Displays the most current fade added into the sound manager.
        /// </summary>
        private void EditExistingFade()
        {
            EditorGUILayout.PrefixLabel("ChangeAlphaValue-out");
            _existingFadeToEdit.FadeOutCurve = EditorGUILayout.CurveField(_existingFadeToEdit.FadeOutCurve,
                GUILayout.Height(80f));
            EditorGUILayout.PrefixLabel("ChangeAlphaValue-in");
            _existingFadeToEdit.FadeInCurve = EditorGUILayout.CurveField(_existingFadeToEdit.FadeInCurve,
                GUILayout.Height(80f));
            var tempKey = _existingFadeToEdit.Key;
            tempKey = EditorGUILayout.TextField("Key", tempKey);
            if (string.IsNullOrEmpty(tempKey)) GUILayout.Box(EditorGUIUtility.FindTexture("console.warnicon.sml"));
            EditorGUILayout.EndHorizontal();
            _existingFadeToEdit.SetKey(tempKey);
        }



        /// <summary>
        /// Checks to see if music audio source is present, if not, it'll create 2 of them and add it to the sound manager.
        /// The fade testing uses these audio sources to function.
        /// </summary>
        private void CheckForMusicAudioSource()
        {
            for (int i = 0; i < 2; ++i)
            {
                if (_soundManager.MusicAudioSources[i] == null)
                {
                    var temp = i + 1;
                    var gO = GameObject.Find("Music Aduio Source " + (temp));
                    if (gO == null)
                    {
                        SgridAudioSource musicAudioSource =
                            new GameObject("Music Audio Source " + (temp)).AddComponent<SgridAudioSource>();
                        musicAudioSource.SetAudioSourceType(1);
                        _helpers[i] = musicAudioSource.gameObject.AddComponent<SoundFadeEditorHelper>();
                        musicAudioSource.transform.SetParent(_soundManager.transform);
                        //musicAudioSource.transform.SetParent(FindObjectOfType<AudioListener>().transform);
                        _soundManager.MusicAudioSources[i] = musicAudioSource;
                        if (i == 0) _fadeOutAudioSource = musicAudioSource.gameObject.GetComponent<AudioSource>();
                        if (i == 1) _fadeInAudioSource = musicAudioSource.gameObject.GetComponent<AudioSource>();
                    }
                    else
                    {
                        SgridAudioSource musicAudioSource = gO.GetComponent<SgridAudioSource>();
                        if (musicAudioSource == null)
                        {
                            musicAudioSource = gO.AddComponent<SgridAudioSource>();
                            if (i == 0) _fadeOutAudioSource = musicAudioSource.GetComponent<AudioSource>();
                            if (i == 1) _fadeInAudioSource = musicAudioSource.GetComponent<AudioSource>();
                        }
                        _helpers[i] = gO.GetComponent<SoundFadeEditorHelper>();
                        Debug.Log("checking helpers");
                        if (_helpers[i] == null)
                        {
                            _helpers[i] = gO.AddComponent<SoundFadeEditorHelper>();
                        }
                        musicAudioSource.SetAudioSourceType(1);
                        _soundManager.MusicAudioSources[i] = musicAudioSource;
                    }
                }
                else
                {
                    _helpers[i] = _soundManager.MusicAudioSources[i].gameObject.AddComponent<SoundFadeEditorHelper>();
                    if (i == 0)
                        _fadeOutAudioSource = GameObject.Find("Music Audio Source 1").GetComponent<AudioSource>();
                    if (i == 1) _fadeInAudioSource = GameObject.Find("Music Audio Source 2").GetComponent<AudioSource>();
                }
            }
        }

        void Update()
        {
            if (_soundManager != null)
            {
                _soundManager.transform.position = Vector3.forward;//to trick Update function of SoundEditorHelper to work properly.    
            }
            //TODO: Seems to be very resource intensive. 
            /*Debug.Log(_soundManager.transform.position);*/
        }
    }
}
#endif