using Scenes;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace UIs.Common
{
    public class TapToLoadScene : UIBehaviour
    {
        [SerializeField]
        string sceneName;

        [Inject]
        LevelLoader levelLoader;

        protected override void Start()
        {
            this.OnPointerClickAsObservable()
                .First()
                .Subscribe(_ => levelLoader.LoadScene(sceneName))
                .AddTo(this);
        }
    }
}