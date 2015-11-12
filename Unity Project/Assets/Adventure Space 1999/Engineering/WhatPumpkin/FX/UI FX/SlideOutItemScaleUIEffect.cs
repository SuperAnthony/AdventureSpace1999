using UnityEngine;
using System.Collections;


namespace WhatPumpkin.FX
{

    public class SlideOutItemScaleUIEffect : MonoBehaviour
    {

        [SerializeField] private ScaleEffect _thisScaleEffect;

        void Awake()
        {
            
        }

        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log(this.name + ": Triggering.");
            _thisScaleEffect.OnOffScale();
        }
    }

}
