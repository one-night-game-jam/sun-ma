using System;
using Scenes;
using UniRx.Async;
using UnityEngine;
using Zenject;

namespace Levels
{
    public class TimeLimiter : MonoBehaviour
    {
        [SerializeField]
        float numberOfTimeSeconds;

        [Inject]
        LevelLoader levelLoader;

        async void Start()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(numberOfTimeSeconds));
            levelLoader.LoadResult();
        }
    }
}
