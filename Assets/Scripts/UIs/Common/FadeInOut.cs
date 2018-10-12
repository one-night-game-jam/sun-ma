using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UniRx;
using UniRx.Async;

namespace UIs.Common
{
    public class FadeInOut : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        private ISubject<Unit> _showAnimatonEnd = new Subject<Unit>();
        private ISubject<Unit> _hideAnimatonEnd = new Subject<Unit>();

        public void OnShowAnimationEnd()
        {
            _showAnimatonEnd.OnNext(Unit.Default);
        }

        public void OnHideAnimationEnd()
        {
            _hideAnimatonEnd.OnNext(Unit.Default);
        }

        public async UniTask FadeIn()
        {
            animator.SetBool("IsVisible", true);
            await _showAnimatonEnd.First();
        }

        public async UniTask FadeOut()
        {
            animator.SetBool("IsVisible", false);
            await _hideAnimatonEnd.First();
        }
    }
}
