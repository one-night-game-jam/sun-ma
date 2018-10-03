using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Titles
{
    public class TapToStart : UIBehaviour
    {
        [SerializeField]
        string sceneName;

        protected override void Start()
        {
            this.OnPointerClickAsObservable()
                .Subscribe(_ =>
                {
                    SceneManager.LoadScene(sceneName);
                })
                .AddTo(this);
        }
    }
}