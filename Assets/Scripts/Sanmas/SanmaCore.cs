using System;
using UniRx;
using UnityEngine;

namespace Sanmas
{
    public class SanmaCore : MonoBehaviour
    {
        readonly AsyncSubject<Vector2> _launch = new AsyncSubject<Vector2>();
        public IObservable<Vector2> OnLaunchAsObservable => _launch;

        Vector2 _gravity;
        public Vector2 Gravity => _gravity;

        public void Launch(Vector2 velocity)
        {
            _launch.OnNext(velocity);
            _launch.OnCompleted();
        }

        public void UpdateGravity(Vector2 gravity)
        {
            _gravity = gravity;
        }
    }
}
