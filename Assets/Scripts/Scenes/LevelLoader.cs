using System.Collections.Generic;
using System.Linq;
using UniRx.Async;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField]
        string[] sceneNames;

        async void Start()
        {
            var loaded = Enumerable
                .Select(Enumerable.Range(0, SceneManager.sceneCount), SceneManager.GetSceneAt)
                .Select(s => s.name);
            await LoadScenesAdditive(sceneNames.Except(loaded));
        }

        static async UniTask LoadScenesAdditive(IEnumerable<string> sceneNames)
        {
            await UniTask.WhenAll(sceneNames.Select(LoadSceneAdditive));
        }

        static async UniTask LoadSceneAdditive(string sceneName)
        {
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
    }
}
