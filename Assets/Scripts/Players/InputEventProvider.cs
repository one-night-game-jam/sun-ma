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
                // When using a mouse the pointerId returns -1, -2, or -3.
                .Where(x => x.pointerId == -1 || x.pointerId == 0)
                .Select(x => (x.position - x.pressPosition) / Screen.dpi);
        }

        public IObservable<Vector2> OnEndPullAsObservable()
        {
            return this.OnEndDragAsObservable()
                .Where(x => x.pointerId == -1 || x.pointerId == 0)
                .WithLatestFrom(this.OnPullAsObservable(), (_, x) => x);
        }
    }
}
