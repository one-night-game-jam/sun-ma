using Sanmas;
using Stores;
using UniRx;
using UnityEngine;
using Zenject;

namespace Objects.Dishes
{
    public class DishCore : MonoBehaviour, ISanmaEnterHandler
    {
        readonly ISubject<SanmaCore> _enter = new Subject<SanmaCore>();
        Score score;

        [Inject]
        void Initialize(Score score)
        {
            this.score = score;
        }

        void Start()
        {
            _enter.Subscribe(x =>
                {
                    score.Add(x.BroiledValue.Value);
                    x.Freeze();
                    x.transform.SetParent(transform);
                    x.transform.localPosition = Vector3.zero;
                })
                .AddTo(this);

            _enter.Pairwise()
                .Select(x => x.Previous?.gameObject)
                .Where(x => x != null)
                .Subscribe(Destroy)
                .AddTo(this);
        }

        void ISanmaEnterHandler.OnEnter(SanmaCore sanmaCore)
        {
            _enter.OnNext(sanmaCore);
        }
    }
}
