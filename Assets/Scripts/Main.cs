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

        private CameraController _cameraController;
        // [SerializeField] private Transform _camera;
        //[SerializeField] private Transform _back;



        private ParalaxManager _paralaxManager;
        private SpriteAnimatorController _playerAnimator;
        private SpriteAnimatorController _enemyAnimator;
        private PlayerController _playerController;


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
            // _paralaxManager = new ParalaxManager(_camera, _back);

            _cameraController = new CameraController(_playerView.transform, Camera.main.transform);
        }

        
        void Update()
        {
            _enemyAnimator.Update();
            _cameraController.Update();
            // _paralaxManager.Update();
            _playerController.Update();
        }
    }
}
