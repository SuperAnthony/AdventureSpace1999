using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using WhatPumpkin;
using WhatPumpkin.Sound;
using MessageType = UnityEditor.MessageType;

public class SoundTypeListEditorWindow : EditorWindow
{
    /// <summary>
    /// Holds the scroll position of the scroll bar.
    /// </summary>
    private Vector2 _scrollPosition;

    /// <summary>
    /// Reference to the scene's sound manager.
    /// </summary>
    private SoundManager _soundManager;

    /// <summary>
    /// Used for displaying corresponding sound list.
    /// </summary>
    private int _choice;

    /// <summary>
    /// Used for toggling errors.
    /// </summary>
    private bool _errorToggle = false;

    /// <summary>
    /// Used as reference to list of audio clips per random SFX.
    /// </summary>
    private AudioClip[] _randomSfxgoAudioClips;

    [MenuItem("HiveSwap/List of Scene Audio")]
    private static void Init()
    {
        SoundTypeListEditorWindow _soundList = (SoundTypeListEditorWindow)GetWindow(typeof(SoundTypeListEditorWindow));
        _soundList.Show();
    }

    void OnEnable()
    {
        //SoundManager.Initialize();
        _soundManager = FindObjectOfType<SoundManager>();

    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("In scene sounds.", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        //Begin main area.
        EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
        DrawSoundTypeButtons();
        ToggleSoundList(_choice);
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        //End main area.
        ShowErrors();
    }

    /// <summary>
    /// Shows the list of sound types.
    /// </summary>
    /// <typeparam name="T">The sound type to pass restricted to ISFX</typeparam>
    /// <param name="soundTypes">The collection of sound type to display.</param>
    void DrawSoundTypeList<T>(IEnumerable<T> soundTypes) where T : ISFX
    {
        Repaint();
        _errorToggle = false;
        DrawHeader();
        var enumerable = soundTypes as T[] ?? soundTypes.ToArray();
        if (soundTypes == null || !enumerable.Any())
        {
            _errorToggle = true;
            return;
        }
        //Begin property area.
        foreach (var soundType in enumerable)
        {
            
            EditorGUILayout.BeginHorizontal(GUIStyle.none);
            if (soundType == null)
            {
                EditorGUILayout.HelpBox("This entry is missing.", MessageType.Error);
                //TODO: Improve on this.
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                string key = soundType.Key;
                AudioClip audioClip = soundType.AudioClip;
                key = EditorGUILayout.TextField(GUIContent.none, key);
                audioClip = EditorGUILayout.ObjectField(audioClip, typeof(AudioClip), false) as AudioClip;
                soundType.SetKey(key);
                soundType.SetAudioClip(audioClip);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
            }
            
        }
        //End property area.
        EditorGUILayout.Space();
        if (GUILayout.Button("Add"))
        {
            OpenCustomSoundEditor();
        }
    }

    /// <summary>
    /// Displays the list of random SFX in the scene.
    /// This is used instead of the generic one above because ISFX does not handle collections of audio clip.
    /// </summary>
    void DrawRandomSFXGOList()
    {
        _errorToggle = false;
        DrawHeader();
        if (_soundManager.RandomSfxgoList == null || _soundManager.RandomSfxgoList.Count == 0)
        {
            _errorToggle = true;
            return;
        }
        //Begin random sfx property area.
        foreach (var randomSfxgo in _soundManager.RandomSfxgoList)
        {
            EditorGUILayout.BeginHorizontal(GUIStyle.none);
            if (randomSfxgo == null)
            {
                EditorGUILayout.HelpBox("This entry is missing.", MessageType.Error);
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                string key = randomSfxgo.Key;
                _randomSfxgoAudioClips = randomSfxgo.AudioClips;
                key = EditorGUILayout.TextField(GUIContent.none, key);
                randomSfxgo.SetKey(key);
                EditorGUILayout.BeginVertical(GUIStyle.none);
                for (int i = 0; i < _randomSfxgoAudioClips.Length; i++)
                {
                    _randomSfxgoAudioClips[i] = EditorGUILayout.ObjectField(_randomSfxgoAudioClips[i],
                        typeof (AudioClip), false) as AudioClip;
                    randomSfxgo.AddAudioClips(_randomSfxgoAudioClips);
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
            }
        }
        EditorGUILayout.Space();
        //End random sfx property area.
        if (GUILayout.Button("Add"))
        {
            OpenCustomSoundEditor();
        }
    }

    /// <summary>
    /// Draws the labels "Key Name" and "Audio Clip"
    /// </summary>
    void DrawHeader()
    {
        EditorGUILayout.Space();
        if (_choice == 1) EditorGUILayout.LabelField("Music:", EditorStyles.boldLabel);
        if (_choice == 3) EditorGUILayout.LabelField("Ambient Sound:", EditorStyles.boldLabel);
        if (_choice == 2) EditorGUILayout.LabelField("SFX:", EditorStyles.boldLabel);
        if (_choice == 4) EditorGUILayout.LabelField("Random SFX:", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal(GUIStyle.none);
        EditorGUILayout.LabelField("Key Name");
        EditorGUILayout.LabelField("Audio Clip");
        EditorGUILayout.EndHorizontal();
    }

    /// <summary>
    /// Draws the buttons that displays the corresponding sound list.
    /// </summary>
    void DrawSoundTypeButtons()
    {
        EditorGUILayout.BeginHorizontal(GUIStyle.none);
        EditorGUILayout.BeginVertical(GUIStyle.none);
        if (GUILayout.Button("Music")) { _choice = 1; }
        if (GUILayout.Button("SFX")) { _choice = 2; }
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical(GUIStyle.none);
        if (GUILayout.Button("Ambient")) { _choice = 3; }
        if (GUILayout.Button("Random SFX")) { _choice = 4; }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
    }

    /// <summary>
    /// Handles which list to display based on the value of _choice.
    /// </summary>
    /// <param name="choice"></param>
    private void ToggleSoundList(int choice)
    {
        switch (choice)
        {
            case 1:
                DrawSoundTypeList(_soundManager.Musics);
                break;
            case 2:
                DrawSoundTypeList(_soundManager.Sfxs);
                break;
            case 3:
                DrawSoundTypeList(_soundManager.AmbientSounds);
                break;
            case 4:
                DrawRandomSFXGOList();
                break;
            default:
                EditorGUILayout.HelpBox("Choose a sound list.", MessageType.Info);
                break;
        }
    }

    /// <summary>
    /// Shows error based on _choice.
    /// </summary>
    void ShowErrors()
    {
        if (_errorToggle)
        {
            if (_choice == 1) EditorGUILayout.HelpBox("No Music in the GameData or GameData missing.", MessageType.Warning);
            if (_choice == 2) EditorGUILayout.HelpBox("No SFXs in the GameData or GameData missing.", MessageType.Warning);
            if (_choice == 3) EditorGUILayout.HelpBox("No Ambient sounds in GameData or GameData missing.", MessageType.Warning);
            if (_choice == 4) EditorGUILayout.HelpBox("No Random SFX in the GameData or GameData missing.", MessageType.Warning);
        }
    }

    /// <summary>
    /// Launches the custome sound editor when the Add button is pressed.
    /// </summary>
    void OpenCustomSoundEditor()
    {
        EditorApplication.ExecuteMenuItem("HiveSwap/Create/Custom Sounds");
    }
}
