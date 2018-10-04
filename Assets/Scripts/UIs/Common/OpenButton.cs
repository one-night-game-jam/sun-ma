using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UIs.Common
{
    public class OpenButton : MonoBehaviour
    {
        [SerializeField]
        Button button;

        [SerializeField]
        GameObject prefab;

        void Start()
        {
            button.OnClickAsObservable()
                .Subscribe(_ => Instantiate(prefab))
                .AddTo(this);
        }
    }
}
