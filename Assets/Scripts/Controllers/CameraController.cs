using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Platformer
{
    public class CameraController
    {
        private float x;
        private float y;

        private float offsetX = 1.5f;
        private float offsetY = 1.5f;

        private int _camSpeed = 120;

        private Transform _playerTransform;
        private Transform _cameraTransform;

        public CameraController(Transform player, Transform camera)
        {
            _playerTransform = player;
            _cameraTransform = camera;
        }
        public void Update()
        {
            x = _playerTransform.position.x;
            y = _playerTransform.position.y;

            _cameraTransform.position = Vector3.Lerp(_cameraTransform.position,
                new Vector3(x + offsetX, y + offsetY, _cameraTransform.position.z),
                Time.deltaTime * _camSpeed);
        }
    }
}

