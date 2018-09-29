using UniRx;
using UnityEngine;

namespace Sanmas
{
    public class SanmaRenderer : MonoBehaviour
    {
        [SerializeField] SanmaCore _core;
        [SerializeField] SpriteRenderer[] _renderers;

        [SerializeField] float _wellDoneValue;

        void Start()
        {
            _core.BroiledValue
                .Subscribe(BroiledChanged)
                .AddTo(this);
        }

        void BroiledChanged(float value)
        {
            var rate = value / _wellDoneValue;

            var color = _renderers[1].color;
            color.a = rate * 2f;
            _renderers[1].color = color;

            color = _renderers[2].color;
            color.a = rate * 2f - 1f;
            _renderers[2].color = color;
        }
    }
}
