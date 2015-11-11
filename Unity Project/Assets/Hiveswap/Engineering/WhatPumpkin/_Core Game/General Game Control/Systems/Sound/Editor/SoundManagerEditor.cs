#if UNITY_EDITOR
using UnityEditor;
using WhatPumpkin;

[CustomEditor(typeof(SoundManager))]
public class SoundManagerEditor : Editor
{

    private SerializedProperty _master;
    private SerializedProperty _music;
    private SerializedProperty _ambient;
    private SerializedProperty _sfx;

    void OnEnable()
    {
        _master = serializedObject.FindProperty("_masterVolume");
        _music = serializedObject.FindProperty("_musicVolume");
        _ambient = serializedObject.FindProperty("_ambientVolume");
        _sfx = serializedObject.FindProperty("_sfxVolume");
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();
        _master.floatValue = EditorGUILayout.Slider("Master Volume", _master.floatValue, 0f, 1f);
        _music.floatValue = EditorGUILayout.Slider("Music Volume", _music.floatValue, 0f, 1f);
        _ambient.floatValue = EditorGUILayout.Slider("Ambient Volume", _ambient.floatValue, 0f, 1f);
        _sfx.floatValue = EditorGUILayout.Slider("SFX Volume", _sfx.floatValue, 0f, 1f);
        serializedObject.ApplyModifiedProperties();
    }

}
#endif