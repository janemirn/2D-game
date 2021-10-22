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

        private float _speed = 150f;
        private float _animationSpeed = 10f;
        private float _jumpSpeed = 8f;
        private float _movingTreshold = 0.1f;
        private float _jumpTreshold = 1.0f;

        private float _yVelocity;
        private float _xVelocity;

        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);


        private LevelObjectView _view;
        private SpriteAnimatorController _spriteAnimator;

        private ContactPooler _contactPooler;

        public PlayerController(LevelObjectView player, SpriteAnimatorController animator)
        {
            _view = player;
            _spriteAnimator = animator;
            _spriteAnimator.StartAnimation(_view._spriteRenderer, AnimationState.Idle, true, _animationSpeed);
            _contactPooler = new ContactPooler(_view._collider);
        }

        private void MoveTowards()
        {
            _xVelocity = Time.fixedDeltaTime * _speed * (_xAxisInput < 0 ? -1 : 1);
            _view._rigidbody.velocity = _view._rigidbody.velocity.Change(x: _xVelocity);
            _view._transform.localScale = (_xAxisInput < 0 ? _leftScale : _rightScale);
        }

        
        public void Update()
        {
            _spriteAnimator.Update();
            _contactPooler.Update();

            _xAxisInput = Input.GetAxis("Horizontal");
            _isMoving = Mathf.Abs(_xAxisInput) > _movingTreshold;
            _isJump = Input.GetAxis("Jump") > 0;
            _isCrouch = Input.GetAxis("Crouch") > 0;


            if (_isMoving)
            {
                MoveTowards();
            }

            if (_contactPooler.IsGrounded)
            {

                _spriteAnimator.StartAnimation(_view._spriteRenderer, _isMoving ? AnimationState.Run : _isCrouch? AnimationState.Crouch: AnimationState.Idle, true, _animationSpeed);
                //_spriteAnimator.StartAnimation(_view._spriteRenderer, _isMoving ? AnimationState.Run : AnimationState.Idle, true, _animationSpeed);

                if (_isJump && Mathf.Abs(_view._rigidbody.velocity.y) <= _jumpTreshold)
                {
                    _view._rigidbody.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
                }
            }

            else
            {

                if (Mathf.Abs(_view._rigidbody.velocity.y) > _jumpTreshold)
                {
                    _spriteAnimator.StartAnimation(_view._spriteRenderer, AnimationState.Jump, true, _animationSpeed);
                }
            }

        }
    }
}

