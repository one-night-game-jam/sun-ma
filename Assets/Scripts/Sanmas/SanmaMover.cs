using UniRx;
using UnityEngine;

namespace Sanmas
{
    public class SanmaMover : MonoBehaviour
    {
        [SerializeField] SanmaCore _core;
        [SerializeField] Rigidbody2D _rigidbody;

        void Start()
        {
            _core.OnLaunchAsObservable
                .ObserveOn(Scheduler.MainThreadFixedUpdate)
                .Subscribe(Launch)
                .AddTo(this);

            _core.OnFreezedAsObservable
                .Subscribe(_ => _rigidbody.bodyType = RigidbodyType2D.Static)
                .AddTo(this);
        }

        void Launch(Vector2 force)
        {
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            _rigidbody.AddForce(force, ForceMode2D.Impulse);
        }

        void FixedUpdate()
        {
            _rigidbody.AddForce(_core.Gravity * Time.deltaTime, ForceMode2D.Force);
            if (0.1f < _rigidbody.velocity.sqrMagnitude)
            {
                var dir = _rigidbody.velocity;
                _rigidbody.rotation = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
            }
        }
    }
}
