using UnityEngine;

namespace WhatPumpkin.CutScenes
{
    /// <summary>
    /// Cutscene actor and its associated animator controller. Mainly for grouping purposes on the wizard window.
    /// </summary>
    [System.Serializable]
    public class TesterActors
    {
        public GameObject csActor;
        public RuntimeAnimatorController animatorController;
        public void Clear()
        {
            csActor = null;
            animatorController = null;
        }
    }
}