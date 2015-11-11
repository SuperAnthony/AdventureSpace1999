using System;
using UnityEngine;
using System.Collections;
using System.Security.Policy;
using UnityEngine.UI;
using WhatPumpkin.Sgrid.Characters;

namespace WhatPumpkin.FX
{
    [System.Serializable]
    public class CharacterPortraitInfo
    {
        [SerializeField] private int _key;
        [SerializeField] private GameObject _name;
        [SerializeField] private Sprite _portrait;
        private bool _characterIsActive;


        public Sprite Portrait
        {
            get { return _portrait; }
        }

        public int Key
        {
            get { return _key; }
        }

        public GameObject Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }

    /// <summary>
    /// Script used to switch the character portrait depending on the character that is acitve/selected.
    /// TODO: Create a container class for each party character containing a key and a sprite.
    /// </summary>
    public class CharacterPortraitSwitcher : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Image _portraitHolder;
        [SerializeField] private CharacterPortraitInfo[] _characterPortraits;

        void Awake()
        {
            //PlayerCharacter.PCActivated += HandlePCActivated;
        }

        // Use this for initialization
        void Start()
        {
            PlayerCharacter.PCActivated += HandlePCActivated;
            SwitchPortrait(PlayerCharacter.Active.Id);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void HandlePCActivated(object sender, System.EventArgs e)
        {
			/*
            PlayerCharacter playerChar = (PlayerCharacter)sender;
            if (playerChar != null && _characterPortraits != null)
            {
                SwitchPortrait(playerChar.Id);
            }*/
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteIndex"></param>
        public void SwitchPortrait(int spriteIndex)
        {
			/*
            foreach (var portrait in _characterPortraits)
            {
                if (portrait.Key != spriteIndex)
                {
                    portrait.Name.SetActive(false);
                }
                else
                {
                    portrait.Name.SetActive(true);
                }
            }
            //_portraitHolder.GetComponent<ScaleEffect>().InstantScaleToZero();
			try{
            	_portraitHolder.sprite = _characterPortraits[spriteIndex].Portrait;
			}
			catch (Exception e){
				throw(e);

			}
            Debug.Log("Portrait Switcher: Did it changed?");
            //_portraitHolder.GetComponent<ScaleEffect>().ScaleUp();
            */
        }

        void SwitchName()
        {
            
        }

        void OnDestroy()
        {
            PlayerCharacter.PCActivated -= HandlePCActivated;
        }
    }

}


