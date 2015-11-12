using UnityEngine;
using System.Collections;

namespace WhatPumpkin.CutScenes
{

    public class LookAt : MonoBehaviour
    {

        public Transform target;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            transform.LookAt(target);
        }

    }

}
