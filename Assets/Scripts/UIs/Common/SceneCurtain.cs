using UnityEngine;
using UniRx;
using UniRx.Async;

namespace UIs.Common
{
    public class SceneCurtain : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        private readonly ISubject<Unit> _showAnimationEnd = new Subject<Unit>();
        private readonly ISubject<Unit> _hideAnimationEnd = new Subject<Unit>();

        public void OnShowAnimationEnd()
        {
            _showAnimationEnd.OnNext(Unit.Default);
        }

        public void OnHideAnimationEnd()
        {
            _hideAnimationEnd.OnNext(Unit.Default);
        }

        public async UniTask Show()
        {
            animator.SetBool("IsVisible", true);
            await _showAnimationEnd.First();
        }

        public async UniTask Hide()
        {
            animator.SetBool("IsVisible", false);
            await _hideAnimationEnd.First();
        }
    }
}
