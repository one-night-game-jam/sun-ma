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

        private void Start()
        {
            LaunchPowerAsObservable()
                .Zip(SanmaLane(), (power, sanma) => (power, sanma))
                .Subscribe(x => x.sanma.Launch(x.power))
                .AddTo(this);
        }

        private IObservable<SanmaCore> SanmaLane()
        {
            return Observable.ReturnUnit()
                .Merge(inputEventProvider.OnEndPullAsObservable().AsUnitObservable())
                .Select(_ => factory.Create());
        }

        private IObservable<Vector2> LaunchPowerAsObservable()
        {
            return inputEventProvider.OnEndPullAsObservable()
                .Select(x => (Vector2.zero - x) * launchPowerMultiplier);
        }
    }
}
