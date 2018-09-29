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
            _enter.Subscribe(x => x.Freeze())
                .AddTo(this);
        }

        void ISanmaEnterHandler.OnEnter(SanmaCore sanmaCore)
        {
            _enter.OnNext(sanmaCore);
        }
    }
}
