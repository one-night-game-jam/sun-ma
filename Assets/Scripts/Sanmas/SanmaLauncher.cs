using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Serialization;
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

        [SerializeField, FormerlySerializedAs("laserRoot")]
        private GameObject estimateLineRoot;
        [SerializeField, FormerlySerializedAs("laser")]
        private SpriteRenderer estimateLine;

        [SerializeField, FormerlySerializedAs("laserPowerMultiplier")]
        private float estimateLineLengthMultiplier;

        [Inject]
        private InputEventProvider inputEventProvider;

        private readonly ISubject<SanmaCore> sanma = new ReplaySubject<SanmaCore>();

        private void Start()
        {
            SpawnSanma();

            inputEventProvider.OnEndPullAsObservable()
                .Subscribe(_ =>
                {
                    SetEstimateLineAngle(0);
                    SetEstimateLineLength(0);
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
                .Subscribe(SetEstimateLineAngle)
                .AddTo(this);

            EstimateLineLengthAsObservable()
                .Subscribe(SetEstimateLineLength)
                .AddTo(this);
        }

        private void SetEstimateLineAngle(float angle)
        {
            estimateLineRoot.transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        private void SetEstimateLineLength(float length)
        {
            var scale = estimateLine.transform.localScale;
            scale.y = length;
            estimateLine.transform.localScale = scale;
            var position = estimateLine.transform.localPosition;
            position.y = length * estimateLine.sprite.bounds.size.y / 2;
            estimateLine.transform.localPosition = position;
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

        private IObservable<float> EstimateLineLengthAsObservable()
        {
            return inputEventProvider.OnPullAsObservable()
                .Select(x => x.magnitude * estimateLineLengthMultiplier);
        }
    }
}
