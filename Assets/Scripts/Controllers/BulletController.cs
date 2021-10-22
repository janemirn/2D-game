using UnityEngine;

namespace Platformer
{
    public class BulletController
    {
        private Vector3 _velocity;
        private LevelObjectView _view;
        private float _angle;
        private Vector3 _axis;

        public BulletController(LevelObjectView view)
        {
            _view = view;
            Active(false);
        }

        public void Active(bool value)
        {
            _view.gameObject.SetActive(value);
        }

        private void SetVelocity(Vector3 velocity)
        {
            _velocity = velocity;
            _angle = Vector3.Angle(Vector3.down, _velocity);
            _axis = Vector3.Cross(Vector3.down, _velocity);
            _view.transform.rotation = Quaternion.AngleAxis(_angle, _axis);
        }

        public void Throw(Vector3 position, Vector3 velocity)
        {
            Active(true);
            SetVelocity(velocity);

            _view.transform.position = position;
            _view._rigidbody.velocity = Vector2.zero;
            _view._rigidbody.AddForce(velocity, ForceMode2D.Impulse);
        }

    }
}

