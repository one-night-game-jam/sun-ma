using System;
using Sanmas;
using UniRx;
using UnityEngine;

namespace Objects.Dishes
{
    public class DishCore : MonoBehaviour, ISanmaEnterHandler
    {
        readonly ISubject<SanmaCore> _enter = new Subject<SanmaCore>();

        void Start()
        {
            _enter.Subscribe(x =>
                {
                    x.Freeze();
                    x.transform.SetParent(transform);
                    x.transform.localPosition = Vector3.zero;
                })
                .AddTo(this);

            _enter.Pairwise()
                .Subscribe(x => Destroy(x.Previous))
                .AddTo(this);
        }

        void ISanmaEnterHandler.OnEnter(SanmaCore sanmaCore)
        {
            _enter.OnNext(sanmaCore);
        }
    }
}
