using UnityEngine;
using System.Collections;
using System.Linq;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;

namespace WhatPumpkin.FX
{
    /// <summary>
    /// Container class that contains dialogue participant info such as name, id, and corresponding text color.
    /// </summary>
    [System.Serializable]
    public class DialogueParticipantInfo
    {
        [SerializeField] private string _name;
        [SerializeField] private int _id;
        [SerializeField] private Color _color;
        [SerializeField] private Sprite _icon;
        [SerializeField] private Sprite _responseHighlight;

        public string Name
        {
            get { return _name; }
        }

        public Color Color
        {
            get { return _color; }
        }

        public int Id
        {
            get { return _id; }
        }

        public Sprite Icon
        {
            get { return _icon; }
        }

        public Sprite ResponseHighlight
        {
            get { return _responseHighlight; }
        }
    }

    /// <summary>
    /// Script used to control text colors of the dialogue UI to the corresponding speaker/listener.
    /// Attach this to the main Dialogue UI panel and specify the proper fields in the inpector.
    /// </summary>
    public class DialogueUIColorEffect : Effect
    {
        [SerializeField] private UnityEngine.UI.Text _charNameText;
        [SerializeField] private UnityEngine.UI.Text _dialogueText;
        [SerializeField] private DialogueParticipantInfo[] _participants;

        public DialogueParticipantInfo[] Participants
        {
            get { return _participants; }
        }

        public Text CharNameText
        {
            get { return _charNameText; }
        }

        public Text DialogueText
        {
            get { return _dialogueText; }
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (DialogueManager.Instance.IsConversationActive)
            {
                SetActorDialogueColor();
                
            }
        }
        
        

        private void SetActorDialogueColor()
        {
            if (_dialogueText.gameObject.activeInHierarchy)
            {
                foreach (var participant in _participants)
                {
                    if (DialogueManager.CurrentConversationState.subtitle.dialogueEntry.ActorID == participant.Id)
                    {
                        _charNameText.color = participant.Color;
                        _dialogueText.color = participant.Color;
                        return;
                    }
                }
                //_charNameText.color = Color.clear;
                //_dialogueText.color = Color.clear;
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

