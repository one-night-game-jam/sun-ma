using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToggleTutorial : UIBehaviour
{
    [SerializeField]
    GameObject tutorialRoot;

    protected override void Start()
    {
        this.OnPointerDownAsObservable()
            .Subscribe(_ => tutorialRoot.SetActive(false))
            .AddTo(this);
    }
}
