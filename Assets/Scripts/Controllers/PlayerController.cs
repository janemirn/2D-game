using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PlayerController
    {
        private bool _isJump;
        private bool _isMoving;
        private bool _isCrouch;

        private float _xAxisInput;

        private float _speed = 3f;
        private float _animationSpeed = 10f;
        private float _jumpSpeed = 9f;
        private float _movingTreshold = 0.1f;
        private float _jumpTreshold = 1.0f;

        private float _g = -9.8f;
        private float _yVelocity;
        private float _groundLevel = 0;

        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);


        private LevelObjectView _view;
        private SpriteAnimatorController _spriteAnimator;

        public PlayerController(LevelObjectView player, SpriteAnimatorController animator)
        {
            _view = player;
            _spriteAnimator = animator;
            _spriteAnimator.StartAnimation(_view._spriteRenderer, AnimationState.Idle, true, _animationSpeed);
        }

        private void MoveTowards()
        {
            _view._transform.position += Vector3.right * (Time.deltaTime * _speed * (_xAxisInput < 0 ? -1 : 1));
            _view._transform.localScale = (_xAxisInput < 0 ? _leftScale : _rightScale);
        }

        private bool IsGrounded()
        {
            return _view._transform.position.y <= _groundLevel + float.Epsilon && _yVelocity <= 0;
        }



        public void Update()
        {
            _spriteAnimator.Update();

            _xAxisInput = Input.GetAxis("Horizontal");
            _isMoving = Mathf.Abs(_xAxisInput) > _movingTreshold;


            _isJump = Input.GetAxis("Jump") > 0;
            _isCrouch = Input.GetAxis("Crouch") > 0;


            if (_isMoving)
            {
                MoveTowards();
            }

            if (IsGrounded())
            {

                _spriteAnimator.StartAnimation(_view._spriteRenderer, _isMoving ? AnimationState.Run : _isCrouch? AnimationState.Crouch: AnimationState.Idle, true, _animationSpeed);

               

                if(_isJump && _yVelocity <= 0)
                {
                    _yVelocity = _jumpSpeed;
                }
                else if (_yVelocity <= 0)
                {
                    _yVelocity = float.Epsilon;
                    _view._transform.position = _view._transform.position.Change(y: _groundLevel);
                }
            }

            else
            {
                _yVelocity += _g * Time.deltaTime;

                if (Mathf.Abs(_yVelocity) > _jumpTreshold)
                {
                    _spriteAnimator.StartAnimation(_view._spriteRenderer, AnimationState.Jump, true, _animationSpeed);
                }
                _view._transform.position += Vector3.up * (_yVelocity * Time.deltaTime);
            }

        }
    }
}

