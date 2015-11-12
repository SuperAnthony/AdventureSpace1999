using System;
using UnityEngine;

namespace WhatPumpkin.FX
{
    public enum Direction
    {
        Up, Down, Left, Right
    }

    /*public class TravelEventArgs : EventArgs
    {
        public float Distance;
        public float Speed;
        public float Destination;
        public float RatioToDestination;

        public TravelEventArgs(float distance, float speed, float destination, float ratioToDestination)
        {
            Distance = distance;
            Speed = speed;
        }
    }*/

    public class MoveFX : Effect
    {
        /// <summary>
        /// Direction the object will move to.
        /// </summary>
        [SerializeField] private Direction _direction;
        /// <summary>
        /// The gameobject that will be moved.
        /// </summary>
        [SerializeField] private GameObject _movingObject;
        /// <summary>
        /// Initial local position of the moving gameobject.
        /// </summary>
        private Vector3 _originalLocalPosition;
        /// <summary>
        /// Amount in pixels that the gameobject will move.
        /// </summary>
        [SerializeField] private float _distance;
        /// <summary>
        /// The speed at which the gameobject will be moved.
        /// </summary>
        [SerializeField] private float _speed;
        /// <summary>
        /// Toggle for activating moving gameobject to move back to original location after moving to destination.
        /// </summary>
        [SerializeField] private bool _movesBack;
        /// <summary>
        /// Pause time between initial move and the moving back.
        /// </summary>
        [SerializeField] private float _backDelay;
        /// <summary>
        /// Used for calculating in between time of initial move and moving back.
        /// </summary>
        private float _elapsedPause;
        /// <summary>
        /// Original local position + the distance amount.
        /// </summary>
        private float _destination;
        /// <summary>
        /// Ratio between current gameobject position and the destination.
        /// </summary>
        private float _ratioToDestination;
        /// <summary>
        /// Toggle for switching between moving to destination and moving back to original position.
        /// </summary>
        private bool _retreat;
        /// <summary>
        /// Gets the ratio to the destination.
        /// </summary>
        public float RatioToDestination
        {
            get { return _ratioToDestination; }
        }
        /// <summary>
        /// Gets whether the effect is currently returning back to the original position.
        /// </summary>
        public bool IsSetToRetreat
        {
            get { return _retreat; }
        }

        /// <summary>
        /// Gets the _speed multiplied by 100.
        /// </summary>
        public float Speed
        {
            get { return _speed * 100; }
        }

        void Start()
        {
            //If GameObject was not specified in the inspector, the gameobject that this scipt is attached to becomes the moving object.
            if (_movingObject == null) _movingObject = this.gameObject;
            _originalLocalPosition = _movingObject.transform.localPosition;
        }

        void Update()
        {
            if (_isActive)
            {
                float currentDistance = 0;
                switch (_direction)
                {
                    case Direction.Up:
                        _destination = _originalLocalPosition.y + _distance;
                        currentDistance = _movingObject.transform.localPosition.y - _originalLocalPosition.y;
                        if (_movingObject.transform.localPosition.y < _destination && !_retreat)
                        {
                            Move(_direction);
                            EffectPlaying();
                        }
                        else if (_movesBack &&
                                 _movingObject.transform.localPosition.y > _originalLocalPosition.y)
                        {
                            _elapsedPause += Time.smoothDeltaTime;
                            if (_elapsedPause > _backDelay)
                            {
                                Retreat(_direction);
                                EffectPlaying();
                            }
                        }
                        else Deactivate();
                        break;
                    case Direction.Down:
                        _destination = _originalLocalPosition.y - _distance;
                        currentDistance = _movingObject.transform.localPosition.y - _originalLocalPosition.y;
                        if (_movingObject.transform.localPosition.y > _destination && !_retreat)
                        {
                            Move(_direction);
                            EffectPlaying();
                        } else if (_movesBack && _movingObject.transform.localPosition.y < _originalLocalPosition.y)
                        {
                            _elapsedPause += Time.smoothDeltaTime;
                            if (_elapsedPause > _backDelay)
                            {
                                Retreat(_direction);
                                EffectPlaying();
                            }
                        }
                        else Deactivate();
                        break;
                    case Direction.Right:
                        _destination = _originalLocalPosition.x + _distance;
                        currentDistance = _movingObject.transform.localPosition.x - _originalLocalPosition.x;
                        if (_movingObject.transform.localPosition.x < _destination && !_retreat)
                        {
                            Move(_direction);
                            EffectPlaying();
                        } else if (_movingObject.transform.localPosition.x > _originalLocalPosition.x)
                        {
                            _elapsedPause += Time.smoothDeltaTime;
                            if (_elapsedPause > _backDelay)
                            {
                                Retreat(_direction);
                                EffectPlaying();
                            }
                        }
                        else Deactivate();
                        break;
                    case Direction.Left:
                        _destination = _originalLocalPosition.x - _distance;
                        currentDistance = _movingObject.transform.localPosition.x - _originalLocalPosition.x;
                        if (_movingObject.transform.localPosition.x > _destination && !_retreat)
                        {
                            Move(_direction);
                            EffectPlaying();
                        } else if (_movingObject.transform.localPosition.x < _originalLocalPosition.x)
                        {
                            _elapsedPause += Time.smoothDeltaTime;
                            if (_elapsedPause > _backDelay)
                            {
                                Retreat(_direction);
                                EffectPlaying();
                            }
                        }
                        else Deactivate();
                        break;
                }
                _ratioToDestination = currentDistance/_distance;
            }
        }

        /// <summary>
        /// Moves back to opposite direction until it meets it's original position.
        /// </summary>
        /// <param name="direction">The original direction.</param>
        private void Retreat(Direction direction)
        {
            _retreat = true;
            var moveAmount = Speed * Time.smoothDeltaTime;
            switch (direction)
            {
                case Direction.Up:
                    _movingObject.transform.Translate(Vector3.down * moveAmount);
                    break;
                case Direction.Down:
                    _movingObject.transform.Translate(Vector3.up * moveAmount);
                    break;
                case Direction.Right:
                    _movingObject.transform.Translate(Vector3.left * moveAmount);
                    break;
                case Direction.Left:
                    _movingObject.transform.Translate(Vector3.right * moveAmount);
                    break;
            }
        }
        /// <summary>
        /// Moves object to the corresponding direction.
        /// </summary>
        /// <param name="direction">The direction the object will be moving to.</param>
        private void Move(Direction direction)
        {
            var moveAmount = Speed*Time.smoothDeltaTime;
            switch (direction)
            {
                case Direction.Up:
                    _movingObject.transform.Translate(Vector3.up * moveAmount);
                    break;
                case Direction.Down:
                    _movingObject.transform.Translate(Vector3.down * moveAmount);
                    break;
                case Direction.Right:
                    _movingObject.transform.Translate(Vector3.right * moveAmount);
                    break;
                case Direction.Left:
                    _movingObject.transform.Translate(Vector3.left * moveAmount);
                    break;
            }
        }
        /// <summary>
        /// Restores the original position of the moving object.
        /// </summary>
        private void ResetLocalPosition()
        {
            _movingObject.transform.localPosition = _originalLocalPosition;
        }
        /// <summary>
        /// Activates the effect.
        /// </summary>
        public override void Activate()
        {
            ResetLocalPosition();
            _isActive = true;
        }
        /// <summary>
        /// Deactivates the effect.
        /// </summary>
        public override void Deactivate()
        {
            _isActive = false;
            _elapsedPause = 0f;
            if (_retreat) ResetLocalPosition();
            FinishPlaying();
            _retreat = false;
        }
    }
}