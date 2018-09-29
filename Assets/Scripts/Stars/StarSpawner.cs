using Stores;
using UniRx;
using UnityEngine;
using Zenject;

namespace Stars
{
    public class StarSpawner : MonoBehaviour
    {
        [SerializeField] GameObject prefab;

        [Inject]
        void Initialize(Score score)
        {
            score.ServedSanmas.ObserveAdd()
                .Subscribe(x => Create(x.Value))
                .AddTo(this);
        }

        void Create(float value)
        {
            var star = Instantiate(prefab,
                Random.insideUnitCircle * 4 + Vector2.up * 2,
                Quaternion.identity,
                transform);
            var scale = Mathf.Max(1f - Mathf.Abs(value - 0.5f), 0.5f);
            star.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
