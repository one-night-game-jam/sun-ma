using UnityEngine;
using UniRx;
using UniRx.Async;

namespace UIs.Common
{
    public class SceneCurtain : MonoBehaviour
    {
        [SerializeField]
        Animator animator;

        readonly ISubject<Unit> _showAnimationEnd = new Subject<Unit>();
        readonly ISubject<Unit> _hideAnimationEnd = new Subject<Unit>();

        public void OnShowAnimationEnd()
        {
            _showAnimationEnd.OnNext(Unit.Default);
        }

        public void OnHideAnimationEnd()
        {
            _hideAnimationEnd.OnNext(Unit.Default);
        }

        public async UniTask Show(bool immediately = false)
        {
            animator.SetTrigger(immediately ? "ShowImmediately" : "Show");
            await _showAnimationEnd.First();
        }

        public async UniTask Hide()
        {
            animator.SetTrigger("Hide");
            await _hideAnimationEnd.First();
        }
    }
}
