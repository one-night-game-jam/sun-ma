using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Zenject;

using UIs.Common;

namespace Titles
{
    public class TapToStart : UIBehaviour
    {
        [SerializeField]
        string sceneName;

        [Inject]
        SceneCurtain sceneCurtain;

        protected override void Start()
        {
            this.OnPointerClickAsObservable()
                .Subscribe(async _ =>
                {
                    await sceneCurtain.Show();
                    SceneManager.LoadScene(sceneName);
                })
                .AddTo(this);
        }
    }
}