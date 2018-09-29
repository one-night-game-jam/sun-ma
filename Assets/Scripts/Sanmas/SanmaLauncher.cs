using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

using Players;

namespace Sanmas
{
    public class SanmaLauncher : MonoBehaviour
    {
        [SerializeField]
        private float launchPowerMultiplier;

        [SerializeField]
        private SanmaFactory factory;

        [SerializeField]
        private GameObject laserRoot;
        [SerializeField]
        private SpriteRenderer laser;

        [SerializeField]
        private float laserPowerMultiplier;

        [Inject]
        private InputEventProvider inputEventProvider;

        private ISubject<SanmaCore> sanma = new ReplaySubject<SanmaCore>();

        private void Start()
        {
            SpawnSanma();

            inputEventProvider.OnEndPullAsObservable()
                .Subscribe(_ =>
                {
                    SetLaserAngle(0);
                    SetLaserLength(0);
                    SpawnSanma();
                })
                .AddTo(this);

            LaunchPowerAsObservable()
                .Zip(sanma, (power, sanma) => (power, sanma))
                .Subscribe(x => x.sanma.Launch(x.power))
                .AddTo(this);

            LaunchAngleAsObservable()
                .WithLatestFrom(sanma, (angle, sanma) => (angle, sanma))
                .Subscribe(x =>
                {
                    x.sanma.transform.rotation = Quaternion.Euler(0, 0, x.angle);
                })
                .AddTo(this);

            LaunchAngleAsObservable()
                .Subscribe(x => SetLaserAngle(x))
                .AddTo(this);

            LaserPowerAsObservable()
                .Subscribe(x => SetLaserLength(x))
                .AddTo(this);
        }

        private void SetLaserAngle(float angle)
        {
            laserRoot.transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        private void SetLaserLength(float length)
        {
            var scale = laser.transform.localScale;
            scale.y = length;
            laser.transform.localScale = scale;
            var position = laser.transform.localPosition;
            position.y = length * laser.sprite.bounds.size.y / 2;
            laser.transform.localPosition = position;
        }

        private void SpawnSanma()
        {
            sanma.OnNext(factory.Create());
        }

        private IObservable<Vector2> LaunchPowerAsObservable()
        {
            return inputEventProvider.OnEndPullAsObservable()
                .Select(x => (Vector2.zero - x) * launchPowerMultiplier);
        }

        private IObservable<float> LaunchAngleAsObservable()
        {
            return inputEventProvider.OnPullAsObservable()
                .Select(x => Vector2.SignedAngle(Vector2.down, x));
        }

        private IObservable<float> LaserPowerAsObservable()
        {
            return inputEventProvider.OnPullAsObservable()
                .Select(x => x.magnitude * laserPowerMultiplier);
        }
    }
}
