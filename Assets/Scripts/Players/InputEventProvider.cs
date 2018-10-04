using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Players
{
    public class InputEventProvider : UIBehaviour
    {
        public IObservable<Vector2> OnPullAsObservable()
        {
            return this.OnDragAsObservable()
                .Select(x => (x.position - x.pressPosition) / Screen.dpi);
        }

        public IObservable<Vector2> OnEndPullAsObservable()
        {
            return this.OnEndDragAsObservable()
                .WithLatestFrom(this.OnPullAsObservable(), (_, x) => x);
        }
    }
}
