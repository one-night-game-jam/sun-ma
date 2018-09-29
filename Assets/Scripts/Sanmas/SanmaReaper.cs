using System;
using UniRx;
using UnityEngine;

namespace Sanmas
{
    public class SanmaReaper : MonoBehaviour
    {
        [SerializeField] float _lifetimeSeconds;

        void Start()
        {
            Observable.Timer(TimeSpan.FromSeconds(_lifetimeSeconds))
                .Subscribe(_ => Destroy(gameObject))
                .AddTo(this);
        }
    }
}
