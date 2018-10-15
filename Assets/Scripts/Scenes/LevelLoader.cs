using System;
using System.Collections.Generic;
using System.Linq;
using UniRx.Async;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using UIs.Common;

namespace Scenes
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField]
        string levelSceneName;

        [SerializeField]
        string resultSceneName;

        [SerializeField]
        float playTimeSeconds;

        [Inject]
        SceneCurtain sceneCurtain;

        async void Start()
        {
            var loaded = Enumerable
                .Select(Enumerable.Range(0, SceneManager.sceneCount), SceneManager.GetSceneAt)
                .Select(s => s.name);
            if (!loaded.Contains(levelSceneName))
            {
                await LoadSceneAdditive(levelSceneName);
            }

            await UniTask.Delay(TimeSpan.FromSeconds(playTimeSeconds));
            await sceneCurtain.Show();
            await UnloadScene(levelSceneName);
            await LoadSceneAdditive(resultSceneName);
            await sceneCurtain.Hide();
        }

        static async UniTask LoadSceneAdditive(string sceneName)
        {
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

        static async UniTask UnloadScene(string sceneName)
        {
            await SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}
