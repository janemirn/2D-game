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
        // [SerializeField] private Transform _camera;
        //[SerializeField] private Transform _back;



        private ParalaxManager _paralaxManager;
        private SpriteAnimatorController _playerAnimator;
        private SpriteAnimatorController _enemyAnimator;
        void Start()
        {
            _playerConfig = Resources.Load<SpriteAnimatorConfig>("PlayerAnimationCfg");
            _enemyConfig = Resources.Load<SpriteAnimatorConfig>("EnemyAnimationCfg");
            if (_playerConfig)
            {
                _playerAnimator = new SpriteAnimatorController(_playerConfig);
                _playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimationState.Idle, true, _animationSpeed);
            }

            if (_enemyConfig)
            {
                _enemyAnimator = new SpriteAnimatorController(_enemyConfig);
                _enemyAnimator.StartAnimation(_enemyView._spriteRenderer, AnimationState.Idle, true, _animationSpeed);
            }
            // _paralaxManager = new ParalaxManager(_camera, _back);
        }

        
        void Update()
        {
            _playerAnimator.Update();
            _enemyAnimator.Update();
            // _paralaxManager.Update();
        }
    }
}
