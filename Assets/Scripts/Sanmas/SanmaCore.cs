using System;
using Objects;
using UniRx;
using UnityEngine;

namespace Sanmas
{
    public class SanmaCore : MonoBehaviour
    {
        readonly ISubject<Vector2> _launch = new AsyncSubject<Vector2>();
        public IObservable<Vector2> OnLaunchAsObservable => _launch;

        Vector2 _gravity;
        public Vector2 Gravity => _gravity;

        readonly ISubject<Unit> _freeze = new AsyncSubject<Unit>();
        public IObservable<Unit> OnFreezedAsObservable => _freeze;

        public void Launch(Vector2 velocity)
        {
            _launch.OnNext(velocity);
            _launch.OnCompleted();
        }

        public void UpdateGravity(Vector2 gravity)
        {
            _gravity = gravity;
        }

        public void Freeze()
        {
            _freeze.OnNext(Unit.Default);
            _freeze.OnCompleted();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            other.GetComponent<ISanmaEnterHandler>()?.OnEnter(this);
        }

        void OnTriggerStay2D(Collider2D other)
        {
            other.GetComponent<ISanmaStayHandler>()?.OnStay(this);
        }

        void OnTriggerExit2D(Collider2D other)
        {
            other.GetComponent<ISanmaExitHandler>()?.OnExit(this);
        }
    }
}
