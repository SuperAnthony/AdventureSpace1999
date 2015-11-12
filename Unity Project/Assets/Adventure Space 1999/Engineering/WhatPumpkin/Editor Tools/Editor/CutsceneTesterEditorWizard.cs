using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace WhatPumpkin.CutScenes
{

    /// <summary>
    /// Editor window disguised as a "Wizard" for cutscene testing.
    /// </summary>
    public class CutsceneTesterEditorWizard : EditorWindow
    {

        protected Vector2 ScrollPosition;

        /// <summary>
        /// Name of the clip to play.
        /// </summary>
        private string _clipName;

        /// <summary>
        /// Principal actor that controls scene flow.
        /// </summary>
        [SerializeField] private TesterActors _principalActor;

        /// <summary>
        /// List of secondary cutscene actors and their associated animator controller.
        /// </summary>
        [SerializeField] private List<TesterActors> _secondaryActors;

        /// <summary>
        /// Scene setting/environment.
        /// </summary>
        private GameObject _csEnvironment;

        /// <summary>
        /// Name of next cutscene to play after the current scene.
        /// </summary>
        private string _nextCutSceneName;

        /// <summary>
        /// Toggles whether secondary actors should be used.
        /// </summary>
        private bool _useSecondaryActors = false;

        /// <summary>
        /// Used for logic control when an element in secondary actors is null.
        /// </summary>
        private bool _missingSecondaryActors = false;

        /// <summary>
        /// Toggle used for missing clip name warning.
        /// </summary>
        private bool _clipNameMissing;

        /// <summary>
        /// Toggle used for missing principal actor data warning.
        /// </summary>
        private bool _principalActorMissing;

        /// <summary>
        /// Toggle used for missing secondary actors data warning.
        /// </summary>
        private bool _secondaryActorsMissing;

        /// <summary>
        /// Toggle used for missing envrionment data warning.
        /// </summary>
        private bool _environmentMissing;

        /// <summary>
        /// Toggle used for missing next cutscene clip name warning.
        /// </summary>
        private bool _nextCutsceneClipNameMissing;

        /// <summary>
        /// The cutscene tester.
        /// </summary>
        static private CutSceneTester _csTester;

        /// <summary>
        /// 
        /// </summary>
        static private List<Actor> _actors = new List<Actor>();

        /// <summary>
        /// 
        /// </summary>
        static private GameObject _environment;

        [MenuItem("HiveSwap/Testing/Cutscene Tester")]
        private static void Init()
        {
            CutsceneTesterEditorWizard csWizard =
                (CutsceneTesterEditorWizard) GetWindow(typeof (CutsceneTesterEditorWizard));
            csWizard.Show();
        }


        void OnEnable()
        {
            _csTester = MenuItems.CreateCutSceneTester();
            _actors.Clear();
        }

        void OnGUI()
        {
            EditorGUILayout.LabelField("Cutscene Tester Editor", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            ScrollPosition = EditorGUILayout.BeginScrollView(ScrollPosition);
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            
            //Begin clip name:
            EditorGUILayout.BeginHorizontal(GUIStyle.none);
            _clipName = EditorGUILayout.TextField("Clip Name", _clipName);
            if (string.IsNullOrEmpty(_clipName)) GUILayout.Box(EditorGUIUtility.FindTexture("console.erroricon.sml"));
            EditorGUILayout.EndHorizontal();
            //End clip name.

            //Setting up serialized properties:
            ScriptableObject target = this;
            SerializedObject sO = new SerializedObject(target);
            SerializedProperty principalActorProperty = sO.FindProperty("_principalActor");
            SerializedProperty secondaryActorsProperty = sO.FindProperty("_secondaryActors");
            //End setting up serialized properties.

            //Begin principal actor:
            EditorGUILayout.BeginHorizontal(GUIStyle.none);
            EditorGUILayout.PropertyField(principalActorProperty, true);
            if (_principalActor.csActor == null || _principalActor.animatorController == null)
            {
                GUILayout.Box(EditorGUIUtility.FindTexture("console.erroricon.sml"));
            }
            EditorGUILayout.EndHorizontal();
            //End principal actor.

            //Begin secondary actors:
            _useSecondaryActors = EditorGUILayout.BeginToggleGroup("Use Secondary Actors", _useSecondaryActors);
            EditorGUILayout.BeginHorizontal(GUIStyle.none);
            EditorGUILayout.PropertyField(secondaryActorsProperty, true);
            bool tempWarningToggle = false;
            if (_useSecondaryActors && _secondaryActors == null)
            {
                GUILayout.Box(EditorGUIUtility.FindTexture("console.erroricon.sml"));

            }
            else if (_useSecondaryActors && _secondaryActors != null)
            {
                foreach (var secondaryActor in _secondaryActors)
                {
                    if (secondaryActor.csActor == null || secondaryActor.animatorController == null)
                    {
                        tempWarningToggle = true;
                        break;
                    }
                }
                if (tempWarningToggle || _secondaryActors.Count == 0) GUILayout.Box(EditorGUIUtility.FindTexture("console.erroricon.sml"));
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndToggleGroup();
            //TODO: Show warning if secondary actors not provided when it's toggled to be used.
            //End secondary actors.

            //Begin environment:
            EditorGUILayout.BeginHorizontal(GUIStyle.none);
            _csEnvironment = EditorGUILayout.ObjectField("Environment", _csEnvironment, typeof (GameObject), false) as GameObject;
            if (_csEnvironment == null) GUILayout.Box(EditorGUIUtility.FindTexture("console.erroricon.sml"));
            EditorGUILayout.EndHorizontal();
            //End environment.

            //Begin next cutscene name:
            EditorGUILayout.BeginHorizontal(GUIStyle.none);
            _nextCutSceneName = EditorGUILayout.TextField("Next Cutscene Name", _nextCutSceneName);
            if (string.IsNullOrEmpty(_nextCutSceneName)) GUILayout.Box(EditorGUIUtility.FindTexture("console.warnicon.sml"));
            EditorGUILayout.EndHorizontal();
            //End next cutscene name.

            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            //Updating serialized properties.
            sO.ApplyModifiedProperties();

            //Begin button choices.
            EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
            if (GUILayout.Button("Done"))
            {
                OnCreate();
            }
            if (GUILayout.Button("Next"))
            {
                OnNextCutScene();
            }
            EditorGUILayout.EndHorizontal();
            //End button choices.

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            //Warnings
            ShowWarnings();
            EditorGUILayout.EndScrollView();
        }

        private void ShowWarnings()
        {
            if (_clipNameMissing)
            {
                if (string.IsNullOrEmpty(_clipName))
                {
                    EditorGUILayout.HelpBox("Please specify a clip name.", UnityEditor.MessageType.Error);
                }
            }
            if (_principalActorMissing)
            {
                if (_principalActor.csActor == null)
                {
                    EditorGUILayout.HelpBox("Please specify a principal actor.", UnityEditor.MessageType.Error);
                }
                if (_principalActor.animatorController == null)
                {
                    EditorGUILayout.HelpBox("Please specify an animator controller for the principal actor.", UnityEditor.MessageType.Error);
                }
            }
            if (_secondaryActorsMissing && _useSecondaryActors)
            {
                bool tempWarningToggle = false;
                if (_useSecondaryActors && _secondaryActors == null)
                {
                    EditorGUILayout.HelpBox("Please provide missing secondary actors data.", UnityEditor.MessageType.Error);
                }
                else if (_useSecondaryActors && _secondaryActors != null)
                {
                    foreach (var secondaryActor in _secondaryActors)
                    {
                        if (secondaryActor.csActor == null || secondaryActor.animatorController == null)
                        {
                            tempWarningToggle = true;
                            break;
                        }
                    }
                    if (tempWarningToggle || _secondaryActors.Count == 0)
                    {
                        EditorGUILayout.HelpBox("Please provide missing secondary actors data.", UnityEditor.MessageType.Error);
                    }
                }
            }
            if (_environmentMissing)
            {
                if (_csEnvironment == null)
                {
                    EditorGUILayout.HelpBox("Please specify an environment.", UnityEditor.MessageType.Error);
                }
            }
            if (_nextCutsceneClipNameMissing)
            {
                if (string.IsNullOrEmpty(_nextCutSceneName))
                {
                    EditorGUILayout.HelpBox("Please provide a clip name for the next cut scene to continue.", UnityEditor.MessageType.Warning);
                }
            }
        }
        

        void Update()
        {
            //TODO: Maybe implement dev mode on a key press?
        }

        void PrepareEnvironment(GameObject enviro)
        {
            enviro.transform.SetParent(_csTester.transform);
            _environment = enviro;
        }

        void PrepareActor(GameObject actor, RuntimeAnimatorController controller, bool principal)
        {
            actor.transform.SetParent(_csTester.transform);
            if (actor.GetComponent<Actor>() == null)
            {
                actor.AddComponent<Actor>();
            }
            if (principal == true)
            {
                Transform cameraNode = actor.GetComponent<Actor>().CameraGO;
                if (cameraNode.gameObject.GetComponent<LookAt>() == null)
                {
                    cameraNode.gameObject.AddComponent<LookAt>();
                }
                cameraNode.gameObject.GetComponent<LookAt>().target = actor.GetComponent<Actor>().CameraTarget;
            }
            actor.GetComponent<Animator>().runtimeAnimatorController = controller;
            actor.GetComponent<Animator>().cullingMode = AnimatorCullingMode.AlwaysAnimate;
            _actors.Add(actor.GetComponent<Actor>());
        }

        void AddActorsToCS(List<Actor> tempActorList)
        {
            CutScene temp = new CutScene(tempActorList, _environment.transform, _clipName, _nextCutSceneName);
            _csTester.cutScenes.Add(temp);
        }

        private void Reset()
        {
            _clipNameMissing = false;
            _principalActorMissing = false;
            _secondaryActorsMissing = false;
            _environmentMissing = false;
            _nextCutsceneClipNameMissing = false;
            _clipName = _nextCutSceneName;
            _nextCutSceneName = "";
            if (_principalActor != null) _principalActor.Clear();
            if (_secondaryActors != null) _secondaryActors.Clear();
            _csEnvironment = null;
            _actors.Clear();
            Repaint();
        }

        private void OnCreate()
        {
            if (!string.IsNullOrEmpty(_clipName) && _principalActor.csActor != null &&
                    _principalActor.animatorController != null)
            {
                if (_useSecondaryActors && _secondaryActors != null)
                {
                    if (_secondaryActors.Count == 0) _missingSecondaryActors = true;
                    else
                    {
                        foreach (var secondaryActor in _secondaryActors)
                        {
                            if (secondaryActor == null)
                            {
                                _missingSecondaryActors = true;
                                break;
                            }
                        }
                        if (_missingSecondaryActors == false)
                        {
                            ProcessForm();
                            Close();
                        }
                    }
                }
                else if (_useSecondaryActors == false)
                {
                    ProcessForm();
                    Close();
                }
            }
            else
            {
                _clipNameMissing = true;
                _principalActorMissing = true;
                if (_useSecondaryActors && _secondaryActors != null)
                {
                    if (_secondaryActors.Count == 0) _missingSecondaryActors = true;
                    else
                    {
                        foreach (var secondaryActor in _secondaryActors)
                        {
                            if (secondaryActor == null)
                            {
                                _missingSecondaryActors = true;
                                break;
                            }
                        }
                    }
                }
                _environmentMissing = true;
            }
        }

        private void OnNextCutScene()
        {
            if (!string.IsNullOrEmpty(_clipName) && _principalActor.csActor != null &&
                    _principalActor.animatorController != null && !string.IsNullOrEmpty(_nextCutSceneName))
            {
                if (_useSecondaryActors && _secondaryActors != null)
                {
                    if (_secondaryActors.Count == 0) _missingSecondaryActors = true;
                    else
                    {
                        foreach (var secondaryActor in _secondaryActors)
                        {
                            if (secondaryActor == null)
                            {
                                _missingSecondaryActors = true;
                                break;
                            }
                        }
                        if (_missingSecondaryActors == false)
                        {
                            ProcessForm();
                            Reset();
                        }
                    }
                }
                else if (_useSecondaryActors == false)
                {
                    ProcessForm();
                    Reset();
                }
            }
            else
            {
                _clipNameMissing = true;
                _principalActorMissing = true;
                if (_useSecondaryActors && _secondaryActors != null)
                {
                    if (_secondaryActors.Count == 0) _missingSecondaryActors = true;
                    else
                    {
                        foreach (var secondaryActor in _secondaryActors)
                        {
                            if (secondaryActor == null)
                            {
                                _missingSecondaryActors = true;
                                break;
                            }
                        }
                    }
                }
                _environmentMissing = true;
                _nextCutsceneClipNameMissing = true;
            }
        }

        //TODO: Someday I would like to refactor this. It's so bad...
        private void ProcessForm()
        {
            bool isPrincipal = true;
            bool found = true;
            if (_csTester != null)
            {
                if (_csTester.cutScenes.Count != 0)
                {
                    foreach (CutScene cs in _csTester.cutScenes)
                    {
                        if (cs.HasActor(_principalActor.csActor.gameObject.name))
                        {
                            GameObject temp = cs.GetActor(_principalActor.csActor.name);
                            _principalActor.csActor = temp;
                            PrepareActor(_principalActor.csActor, _principalActor.animatorController, true);
                            found = true;
                            break;
                        }
                        else
                        {
                            found = false;
                            continue;
                        }
                    }
                    if (found == false)
                    {
                        PrepareActor(PrefabUtility.InstantiatePrefab(_principalActor.csActor) as GameObject, _principalActor.animatorController, true);
                    }
                    if (_secondaryActors.Count != 0)
                    {
                        foreach (TesterActors secondary in _secondaryActors)
                        {
                            foreach (CutScene cs in _csTester.cutScenes)
                            {
                                if (cs.HasActor(secondary.csActor.name))
                                {
                                    secondary.csActor = cs.GetActor(secondary.csActor.name);
                                    PrepareActor(secondary.csActor, secondary.animatorController, false);
                                    found = true;
                                    break;
                                }
                                else
                                {
                                    found = false;
                                    continue;
                                }
                            }
                            if (found == false)
                            {
                                PrepareActor(PrefabUtility.InstantiatePrefab(secondary.csActor) as GameObject, secondary.animatorController, !isPrincipal);
                            }
                        }
                    }
                    foreach (CutScene cs in _csTester.cutScenes)
                    {
                        if (cs.HasEnvironment(_csEnvironment.gameObject.name))
                        {
                            GameObject temp = cs.GetEnvironment(_csEnvironment.gameObject.name);
                            _environment = temp;
                            _environment.transform.SetParent(_csTester.transform);
                            found = true;
                            break;
                        }
                        else
                        {
                            found = false;
                            continue;
                        }
                    }
                    if (found == false)
                    {
                        PrepareEnvironment(PrefabUtility.InstantiatePrefab(_csEnvironment) as GameObject);
                    }
                }
                else
                {
                    PrepareActor(PrefabUtility.InstantiatePrefab(_principalActor.csActor) as GameObject, _principalActor.animatorController, true);
                    foreach (TesterActors secondary in _secondaryActors)
                    {
                        PrepareActor(PrefabUtility.InstantiatePrefab(secondary.csActor) as GameObject, secondary.animatorController, false);
                    }
                    _environment = PrefabUtility.InstantiatePrefab(_csEnvironment) as GameObject;
                    _environment.transform.SetParent(_csTester.transform);
                }
                List<Actor> tempActors = new List<Actor>();
                foreach (Actor actor in _actors)
                {
                    tempActors.Add(actor);
                }
                AddActorsToCS(tempActors);
            }
        }

        
    }
}
