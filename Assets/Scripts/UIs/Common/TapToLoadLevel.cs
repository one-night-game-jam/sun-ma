using Scenes;
using UniRx;
using UniRx.Triggers;
using UnityEngine.EventSystems;
using Zenject;

namespace UIs.Common
{
    public class TapToLoadLevel : UIBehaviour
    {
        [Inject]
        LevelLoader levelLoader;

        protected override void Start()
        {
            this.OnPointerClickAsObservable()
                .First()
                .Subscribe(_ => levelLoader.LoadLevel())
                .AddTo(this);
        }
    }
}
