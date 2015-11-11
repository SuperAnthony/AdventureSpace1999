#if UNITY_EDITOR
using UnityEditor;

namespace WhatPumpkin.Sound
{
    /// <summary>
    /// Custom inspector for RandomSFXGO to hide properties from base class.
    /// </summary>
    [CustomEditor(typeof (RandomSFXGO))]
    public class CustomRandomSFXGOInspector : Editor
    {
        private SerializedProperty _audioClips;

        void OnEnable()
        {
            _audioClips = serializedObject.FindProperty("_audioClips");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_audioClips, true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif