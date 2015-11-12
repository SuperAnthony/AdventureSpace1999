#region using
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
#endregion

/*
 * This is a wizard used for setting up cutscenes for testing purposes.
 */

namespace WhatPumpkin.CutScenes
{
    public class CSTesterWizard : ScriptableWizard
    {

        #region Data Members (Fields)

        public bool devMode = false;

        public bool useSingleActor = false;

        /// <summary>
        /// Name of the clip to play.
        /// </summary>
        public string clipName;

        /// <summary>
        /// Principal actor that controls scene flow.
        /// </summary>
        public TesterActors principalActor;

        /// <summary>
        /// List of secondary cutscene actors and their associated animator controller.
        /// </summary>
        public List<TesterActors> secondaryActors = new List<TesterActors>();

        /// <summary>
        /// Scene setting/environment.
        /// </summary>
        public GameObject csEnvironment;

        /// <summary>
        /// Name of next cutscene to play after the current scene.
        /// </summary>
        public string nextCutSceneName;

        



        static private CutSceneTester csTester;
        static private List<Actor> actors = new List<Actor>();
        static private GameObject environment;


        #endregion

        #region Member Functions (Methods)

        //[MenuItem("HiveSwap/Testing/CS Tester")]
        static void CreateWizard()
        {
            ScriptableWizard.DisplayWizard<CSTesterWizard>("CS Tester Wizard", "Done", "Next Cut Scene");
            csTester = MenuItems.CreateCutSceneTester();
            actors.Clear();
        }

        void OnWizardCreate()
        {
            CreateFromWizard();
        }

        void CreateFromWizard()
        {
            bool isPrincipal = true;
            bool found = true;
            if (csTester != null)
            {
                if (csTester.cutScenes.Count != 0)
                {
                    foreach (CutScene cs in csTester.cutScenes)
                    {
                        if (cs.HasActor(principalActor.csActor.gameObject.name))
                        {
                            GameObject temp = cs.GetActor(principalActor.csActor.name);
                            principalActor.csActor = temp;
                            PrepareActor(principalActor.csActor, principalActor.animatorController, isPrincipal);
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
                        PrepareActor(PrefabUtility.InstantiatePrefab(principalActor.csActor) as GameObject, principalActor.animatorController, isPrincipal);
                    }
                    if (secondaryActors.Count != 0)
                    {
                        foreach (TesterActors secondary in secondaryActors)
                        {
                            foreach (CutScene cs in csTester.cutScenes)
                            {
                                if (cs.HasActor(secondary.csActor.name))
                                {
                                    secondary.csActor = cs.GetActor(secondary.csActor.name);
                                    PrepareActor(secondary.csActor, secondary.animatorController, !isPrincipal);
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
                    foreach (CutScene cs in csTester.cutScenes)
                    {
                        if (cs.HasEnvironment(csEnvironment.gameObject.name))
                        {
                            GameObject temp = cs.GetEnvironment(csEnvironment.gameObject.name);
                            environment = temp;
                            environment.transform.SetParent(csTester.transform);
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
                        PrepareEnvironment(PrefabUtility.InstantiatePrefab(csEnvironment) as GameObject);
                    }
                }
                else
                {
                    PrepareActor(PrefabUtility.InstantiatePrefab(principalActor.csActor) as GameObject, principalActor.animatorController, isPrincipal);
                    foreach (TesterActors secondary in secondaryActors)
                    {
                        PrepareActor(PrefabUtility.InstantiatePrefab(secondary.csActor) as GameObject, secondary.animatorController, !isPrincipal);
                    }
                    environment = PrefabUtility.InstantiatePrefab(csEnvironment) as GameObject;
                    environment.transform.SetParent(csTester.transform);
                }
                List<Actor> tempActors = new List<Actor>();
                foreach (Actor actor in actors)
                {
                    tempActors.Add(actor);
                }
                AddActorsToCS(tempActors);
            }
        }

        void PrepareEnvironment(GameObject enviro)
        {
            enviro.transform.SetParent(csTester.transform);
            environment = enviro;
        }

        void PrepareActor(GameObject actor, RuntimeAnimatorController controller, bool principal)
        {
            actor.transform.SetParent(csTester.transform);
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
            actors.Add(actor.GetComponent<Actor>());
        }

        void AddActorsToCS(List<Actor> tempActorList)
        {
            CutScene temp = new CutScene(tempActorList, environment.transform, clipName, nextCutSceneName);
            csTester.cutScenes.Add(temp);
        }

        void OnWizardUpdate()
        {
            helpString = "Test out your cutscenes!";
            if (devMode == false)
            {
                if (string.IsNullOrEmpty(clipName))
                {
                    errorString = "Enter the clip name";
                    isValid = false;
                }
                else if (principalActor.animatorController == null || principalActor.csActor == null)
                {
                    errorString = "Specify the principal actor.";
                }
                else if (useSingleActor == false)
                {
                    if (secondaryActors.Count == 0) errorString = "Enter amount of secondary actors.";
                    else
                    {
                        foreach (TesterActors tester in secondaryActors)
                        {
                            if (!tester.csActor)
                            {
                                errorString = "Specify your secondary actor.";
                                isValid = false;
                                break;
                            }
                            if (!tester.animatorController)
                            {
                                errorString = "Add animator controller to your secondary actor.";
                                isValid = false;
                                break;
                            }
                            else if (!csEnvironment)
                            {
                                errorString = "Add the CS environment.";
                                isValid = false;
                            }
                            else
                            {
                                errorString = "";
                                isValid = true;
                            }
                        }
                    }
                }
                else if (!csEnvironment)
                {
                    errorString = "Add the CS environment.";
                    isValid = false;
                }
                else
                {
                    isValid = true;
                    errorString = "";
                }
            }
            else
            {
                isValid = true;
                errorString = "";
            }
            
            
        }

        /// <summary>
        /// Clears wizard field entry to repopulate with next CS info.
        /// </summary>
        void ClearWizardEntry()
        {
            clipName = nextCutSceneName;
            nextCutSceneName = "";
            principalActor.Clear();
            secondaryActors.Clear();
            csEnvironment = null;
            actors.Clear();
            isValid = false;

        }

        /// <summary>
        /// Executed when "Next Cut Scene" button is pressed.
        /// </summary>
        void OnWizardOtherButton()
        {
            CreateFromWizard();
            ClearWizardEntry();
        }
        #endregion
    }
}