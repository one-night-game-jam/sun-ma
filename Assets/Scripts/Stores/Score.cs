using UniRx;
using UnityEngine;

namespace Stores
{
    public class Score : MonoBehaviour
    {
        readonly ReactiveCollection<float> _servedSanmas = new ReactiveCollection<float>();
        public IReadOnlyReactiveCollection<float> ServedSanmas => _servedSanmas;

        public void Add(float value)
        {
            _servedSanmas.Add(value);
        }
    }
}
