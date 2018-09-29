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

        [Inject]
        private InputEventProvider inputEventProvider;

        private ISubject<SanmaCore> sanma = new ReplaySubject<SanmaCore>();

        private void Start()
        {
            SpawnSanma();

            inputEventProvider.OnEndPullAsObservable()
                .Subscribe(_ => SpawnSanma())
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
    }
}
