using UnityEngine;
using System.Collections;
using System.Text;

namespace WhatPumpkin.FX
{
    public class SylladexOpenEffect : Effect
    {
        [SerializeField] private Switch _playerInventoryCanvas;

        [SerializeField] private GameObject _sylladexPanel;

        [SerializeField] private float _speed;

        private RectTransform _rectTransform;

//        private Vector2 _originalAnchoredPosition;

        private float _originalTopRectValue;

        private float _originalBottomRectValue;

        private bool _open;

        private bool _isOpen;

        private bool _initialOpen;

        private bool _hasBeenIntiallyOpenedBefore;

        void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
//            _originalAnchoredPosition = _rectTransform.anchoredPosition;
        }

        // Use this for initialization
        void Start()
        {
            _rectTransform.anchoredPosition = new Vector2(0f, Screen.height);
            _initialOpen = true;
            Open();
        }

        void FixedUpdate()
        {
            if (_isActive)
            {
                Vector3 temp = Vector3.zero;
                if (_open)
                {
                    temp = new Vector3(0f, (Time.fixedDeltaTime*_speed)*-1f, 0f);
                    
                }
                else
                {
                    temp = new Vector3(0f, Time.fixedDeltaTime * _speed, 0f);
                }
                MovePosition(temp);
            }
        }

        public override void Activate()
        {
            _isActive = true;
        }

        public override void Deactivate()
        {
            //Debug.Log("Deactivated.");
            _isActive = false;
            if (!_open)
            {
                _playerInventoryCanvas.SwitchActiveState();
            }
        }

        public void ToggleSylladex()
        {
            if (_isOpen)
            {
                Close();
            }
            else
            {
                Open();
            }
        }

        public void Open()
        {
            if (!_initialOpen)
            {
                _playerInventoryCanvas.SwitchActiveState();
                if (!_hasBeenIntiallyOpenedBefore) {
                    _hasBeenIntiallyOpenedBefore = true;
                    return;
                }
            }
            _open = true;
            Activate();
        }

        public void Close()
        {
            _open = false;
            Activate();
            //_rectTransform.anchoredPosition = new Vector2(0f, Screen.height);
            _initialOpen = false;
            _isOpen = false;
            //_playerInventoryCanvas.SwitchActiveState();
        }

        private void MovePosition(Vector3 newPositionInfo)
        {
            this.transform.localPosition += newPositionInfo;
            if (!_open)
            {
                if (_rectTransform.anchoredPosition.y >= Screen.height)
                {
                    _rectTransform.anchoredPosition = new Vector2(0f, Screen.height);
                    Deactivate();
                }
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            //Debug.Log("Touched");
            _isOpen = true;
            _rectTransform.anchoredPosition = Vector2.zero;
            Deactivate();
        }
    }

}


