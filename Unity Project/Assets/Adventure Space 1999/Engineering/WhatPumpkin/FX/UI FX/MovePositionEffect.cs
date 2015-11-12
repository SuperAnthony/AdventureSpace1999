using UnityEngine;
using System.Collections;
using System.Text;


namespace WhatPumpkin.FX
{
    public class MovePositionEffect : Effect
    {
        //TODO: Implement ability to restrain movements on axis.
        [SerializeField]//Restrics movement to y axis.
        private bool _vertical;

        [SerializeField]//Restrics movement to x axis.
        private bool _horizontal;

        [SerializeField]//The position at which movement will end.
        private Vector2 _endingRectPosition;

        private Vector2 _originalRectPosition;

        private Vector2 _scrollAnchor;

        [SerializeField] private float _speed;

        [SerializeField] private float _moveIncrement;

        private RectTransform _rect;

//        private Effect[] _otherEffects;

        private bool _on;

        private bool _move;

        private bool _forScrolling;

        private float test;
        
        void Awake()
        {
            _rect = GetComponent<RectTransform>();
        }

        // Use this for initialization
        private void Start()
        {
            
            _originalRectPosition = _rect.anchoredPosition;
            _scrollAnchor = _originalRectPosition;
          //  _otherEffects = GetComponentsInChildren<Effect>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (_isActive)
            {
                Vector2 temp = Vector2.zero;
                if (_move)
                {
                    if (_horizontal)
                    {
                        temp = new Vector2(Time.deltaTime * _speed, 0f);
                    } else if (_vertical)
                    {
                        temp = new Vector2(0f, Time.deltaTime*_speed);
                    }
                    else
                    {
                        temp = new Vector2(Time.deltaTime * _speed, Time.deltaTime * _speed);
                    }
                    
                }
                else
                {
                    if (_horizontal)
                    {
                        temp = new Vector2((Time.deltaTime * _speed) * -1, 0f);
                    } else if (_vertical)
                    {
                        temp = new Vector2(0f, (Time.deltaTime*_speed)*-1);
                    }
                    else
                    {
                        temp = new Vector2((Time.deltaTime * _speed) * -1, (Time.deltaTime * _speed) * -1);
                    }
                }
                ChangePosition(temp);
            }
        }

        /// <summary>
        /// Moves UI element to _endingRectPosition.
        /// </summary>
        public void MovePosition()
        {
            _isActive = false;
            _move = true;
            Activate();
        }

        /// <summary>
        /// Moves UI element to _originalRectPosition.
        /// </summary>
        public void RetractPosition()
        {
            _isActive = false;
            _move = false;//set to retract
            Activate();
        }

        /// <summary>
        /// Alternates between moving to _endingRectPosition and retracting to _originalRectPosition.
        /// </summary>
        public void OnOffMovement()
        {
            //_on = !_on;
            if (_move)
            {
                if (_rect.anchoredPosition == _endingRectPosition)
                {
                    _move = false;
                    Activate();
                }
            }
            else
            {
                if (_rect.anchoredPosition == _originalRectPosition)
                {
                    _move = true;
                    Activate();
                }
            }
        }

        /// <summary>
        /// Moves UI element to the positive _moveIncrement value based on rect anchors of the UI element.
        /// </summary>
        public void MoveRight()
        {
            /*Vector2 rightIncrement = new Vector2(_moveIncrement, 0f);
            _rect.anchoredPosition += rightIncrement;*/
            _forScrolling = true;
            _move = true;
            _scrollAnchor.x += _moveIncrement;
            _isActive = true;
        }

        /// <summary>
        /// Moves UI element to the negative _moveIncrement value based on rect anchors of the UI element.
        /// </summary>
        public void MoveLeft()
        {
            /*Vector2 leftIncrement = new Vector2(_moveIncrement, 0f);
            _rect.anchoredPosition -= leftIncrement;*/
            _forScrolling = true;
            _move = false;
            _scrollAnchor.x -= _moveIncrement;
            _isActive = true;
        }

        public override void Activate()
        {
            _isActive = true;
        }

        public override void Deactivate()
        {
//            Debug.Log("Deactivated.");
            _isActive = false;
            FinishPlaying();
        }

        public override void Play()
        {
            Activate();
        }

        public override void Play(string[] parameters)
        {
            base.Play(parameters);
        }

        /// <summary>
        /// Changes the X-axis position of the UI element.
        /// </summary>
        /// <param name="positionValue">The amount to move.</param>
        private void ChangePosition(Vector2 positionValue)
        {
            _rect.anchoredPosition += positionValue;
            if (_move)
            {
                if (_forScrolling)
                {
                    if (_scrollAnchor.x <= _rect.anchoredPosition.x)
                    {
                        _rect.anchoredPosition = _scrollAnchor;
                        _forScrolling = false;
                        Deactivate();
                    }
                }
                else if (_vertical)
                {
                    if (_rect.anchoredPosition.y >= _endingRectPosition.y)
                    {
                        _rect.anchoredPosition = _endingRectPosition;
                        Deactivate();
                    }
                }
                else if (_horizontal)
                {
                    if (_rect.anchoredPosition.x >= _endingRectPosition.x)
                    {
                        _rect.anchoredPosition = _endingRectPosition;
                        Deactivate();
                    }
                }
                else if (_rect.anchoredPosition.x >= _endingRectPosition.x && _rect.anchoredPosition.y >= _endingRectPosition.y)
                {
                    _rect.anchoredPosition = _endingRectPosition;
                    Deactivate();
                }
            }
            else
            {
                if (_forScrolling)
                {
                    if (_scrollAnchor.x >= _rect.anchoredPosition.x)
                    {
                        _rect.anchoredPosition = _scrollAnchor;
                        _forScrolling = false;
                        Deactivate();
                    }
                }
                else if (_vertical)
                {
                    if (_rect.anchoredPosition.y <= _originalRectPosition.y)
                    {
                        _rect.anchoredPosition = _originalRectPosition;
                        Deactivate();
                    }
                }
                else if (_horizontal)
                {
                    if (_rect.anchoredPosition.x <= _originalRectPosition.x)
                    {
                        _rect.anchoredPosition = _originalRectPosition;
                        Deactivate();
                    }
                }
                else if (_rect.anchoredPosition.x <= _originalRectPosition.x && _rect.anchoredPosition.y <= _originalRectPosition.y)
                {
                    _rect.anchoredPosition = _originalRectPosition;
                    Deactivate();
                }
            }
            
        }
    }
}
