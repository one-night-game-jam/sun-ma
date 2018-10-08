using UniRx;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;

namespace UIs.Common
{
    public class CloseButton : MonoBehaviour
    {
        [SerializeField]
        Button button;

        [SerializeField]
        GameObject target;

        void Start()
        {
            button.OnClickAsObservable()
                .First()
                .Subscribe(_ => Destroy(target))
                .AddTo(this);
        }
    }
}
