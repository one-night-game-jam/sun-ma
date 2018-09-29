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
            inputEventProvider.OnPullAsObservable()
                .Subscribe(x =>
                {
                    // TODO: Update pulling effect
                    Debug.Log(x);
                })
                .AddTo(this);

            LaunchPowerAsObservable()
                .Select(power =>
                {
                    var sanma = factory.Create();
                    return (power, sanma);
                })
                .Subscribe(x => x.sanma.Launch(x.power))
                .AddTo(this);
        }

        private IObservable<Vector2> LaunchPowerAsObservable()
        {
            return inputEventProvider.OnEndPullAsObservable()
                .Select(x => (Vector2.zero - x) * launchPowerMultiplier);
        }
    }
}
