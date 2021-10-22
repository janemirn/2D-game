using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private SpriteAnimatorConfig _playerConfig;
        [SerializeField] private SpriteAnimatorConfig _enemyConfig;
        [SerializeField] private int _animationSpeed = 10;

        [SerializeField] private LevelObjectView _playerView;
        [SerializeField] private LevelObjectView _enemyView;
        [SerializeField] private CannonView _cannonView;

        private CameraController _cameraController;
        private SpriteAnimatorController _playerAnimator;
        private SpriteAnimatorController _enemyAnimator;
        private PlayerController _playerController;
        private CannonController _cannonController;
        private BulletEmitterController _bulletEmitterController;


        void Start()
        {
            _playerConfig = Resources.Load<SpriteAnimatorConfig>("PlayerAnimationCfg");
            _enemyConfig = Resources.Load<SpriteAnimatorConfig>("EnemyAnimationCfg");
            if (_playerConfig)
            {
                _playerAnimator = new SpriteAnimatorController(_playerConfig);
                _playerController = new PlayerController(_playerView, _playerAnimator);
            }

            if (_enemyConfig)
            {
                _enemyAnimator = new SpriteAnimatorController(_enemyConfig);
                _enemyAnimator.StartAnimation(_enemyView._spriteRenderer, AnimationState.Idle, true, _animationSpeed);
            }

            _cameraController = new CameraController(_playerView.transform, Camera.main.transform);

            _cannonController = new CannonController(_cannonView._muzzleTransform, _playerView.transform);
            _bulletEmitterController = new BulletEmitterController(_cannonView._bullets, _cannonView._emitterTransform);
        }

        
        void Update()
        {
            _enemyAnimator.Update();
            _cameraController.Update();
            _playerController.Update();
            _cannonController.Update();
            _bulletEmitterController.Update();
        }
    }
}
