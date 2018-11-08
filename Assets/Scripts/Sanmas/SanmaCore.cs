using System;
using System.Linq;
using Objects;
using Objects.Wormhole;
using UniRx;
using UniRx.Triggers;
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

        readonly FloatReactiveProperty _broiledValue = new FloatReactiveProperty();
        public IReadOnlyReactiveProperty<float> BroiledValue => _broiledValue;

        readonly ISubject<Vector2> _warp = new Subject<Vector2>();
        public IObservable<Vector2> OnWarpAsObservable => _warp;

        void Start()
        {
            this.OnTriggerStay2DAsObservable()
                .Select(x => x.GetComponent<IGravitationalField>())
                .Where(x => x != null)
                .Select(x => x.CalcGravity(transform.position))
                .Merge(this.FixedUpdateAsObservable()
                    .Select(_ => Vector2.zero))
                .BatchFrame(0, FrameCountType.FixedUpdate)
                .Select(v => v.Sum())
                .Subscribe(UpdateGravity)
                .AddTo(this);

            this.OnTriggerStay2DAsObservable()
                .Select(x => x.GetComponent<IBroiler>())
                .Where(x => x != null)
                .Select(x => x.CalcPower(transform.position) * Time.deltaTime)
                .Subscribe(Broil)
                .AddTo(this);

            this.OnTriggerEnter2DAsObservable()
                .Select(x => x.GetComponent<ISanmaEnterHandler>())
                .Subscribe(x => x?.OnEnter(this))
                .AddTo(this);

            this.OnTriggerEnter2DAsObservable()
                .TakeUntil(_freeze)
                .Select(x => x.GetComponent<Wormhole>())
                .Where(x => x != null)
                .Select(x => x.WarpPoint())
                .StartWith(Vector2.zero)
                .Buffer(2)
                .Where(x => x.Count == 2)
                .Select(x => x.Last())
                .Subscribe(Warp)
                .AddTo(this);
        }

        public void Launch(Vector2 velocity)
        {
            _launch.OnNext(velocity);
            _launch.OnCompleted();
        }

        void UpdateGravity(Vector2 gravity)
        {
            _gravity = gravity;
        }

        void Broil(float value)
        {
            _broiledValue.Value += value;
        }

        void Warp(Vector2 position)
        {
            _warp.OnNext(position);
        }

        public void Freeze()
        {
            _freeze.OnNext(Unit.Default);
            _freeze.OnCompleted();
        }
    }
}
