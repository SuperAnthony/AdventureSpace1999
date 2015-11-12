using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;


namespace WhatPumpkin.FX
{
    /// <summary>
    /// Script used in conjuction with DialogueUIColorEffect to change response color according to the participant responding.
    /// TODO: Possibly also change participant icon.
    /// TODO: Find way to figure out who is responding WITHOUT checking the responder's name in the name label UI since there is a possibility of having multiple responder.
    /// </summary>
    public class DialogueUIResponseEffect : Effect
    {
        [SerializeField] private GameObject _mainDialoguePanel;
        [SerializeField] private GameObject _messagePanel;
        [SerializeField] private UnityUIResponseButton _responseButton;
        [SerializeField] private UnityEngine.UI.Text _responseText;
        [SerializeField] private UnityEngine.UI.Image _responderIconHolder;
        private DialogueParticipantInfo _thisParticipant;
        private UnityEngine.UI.Button _thisResponseButton;
//        private UnityUIDialogueUI _dialogueUI;
        private DialogueUIColorEffect _dialogueUiColorEffect;
//        private bool _highlight = false;
        //private static 

        // Use this for initialization
        void Start()
        {
            _thisResponseButton = GetComponent<UnityEngine.UI.Button>();
//            _dialogueUI = _mainDialoguePanel.GetComponent<UnityUIDialogueUI>();
            _dialogueUiColorEffect = _mainDialoguePanel.GetComponent<DialogueUIColorEffect>();
            SetResponseColor();
        }

        // Update is called once per frame
        /*void Update()
        {

            if (DialogueManager.Instance.IsConversationActive)
            {
                if (DialogueManager.Instance.CurrentConversationState.HasPCResponses)
                {
                    SetResponseColor();
                    //_dialogueUIScript.
                    //Debug.Log(DialogueManager.CurrentConversationState.pcResponses[0].destinationEntry); 
                }
                
            }
        }*/

        /// <summary>
        /// Sets the highlight overlay for the reponse button.
        /// </summary>
        /// <param name="participant"></param>
        private void SetResponseButtonHighlight(DialogueParticipantInfo participant)
        {
            UnityEngine.UI.SpriteState tempState = new SpriteState();
            tempState.highlightedSprite = participant.ResponseHighlight;
            _thisResponseButton.spriteState = tempState;
        }

        /// <summary>
        /// Sets the icon for the responder choice.
        /// </summary>
        /// <param name="participant"></param>
        private void SetResponderIcon(DialogueParticipantInfo participant)
        {
            _responderIconHolder.sprite = participant.Icon;
        }

        /// <summary>
        /// To be called on PointerEnter and PointerExit Event Trigger.
        /// </summary>
        /// <param name="toggle">Set to true for PointerEnter and set to false for PointerExit</param>
        public void ToggleHighlight(bool toggle)
        {
            if (toggle) _responseText.color = Color.white;
            else _responseText.color = _thisParticipant.Color;
        }

        private void SetResponseColor()
        {
            if (!_messagePanel.activeInHierarchy)
            {
                foreach (var participant in _dialogueUiColorEffect.Participants)
                {
                    if (participant.Id == _responseButton.response.destinationEntry.ActorID)
                    {
                        SetResponderIcon(participant);
                        SetResponseButtonHighlight(participant);
                        _responseText.color = participant.Color;
                        _mainDialoguePanel.GetComponent<UnityUIDialogueUI>().dialogue.responseMenu.pcName.color =
                            participant.Color;
                        _thisParticipant = participant;
                        return;
                    }
                }
                /*_dialogueUiColorEffect.CharNameText.color = Color.clear;
                _dialogueUiColorEffect.DialogueText.color = Color.clear;*/
            }
            
        }

        public override void Activate()
        {
            throw new System.NotImplementedException();
        }

        public override void Deactivate()
        {
            throw new System.NotImplementedException();
        }
    }
}

