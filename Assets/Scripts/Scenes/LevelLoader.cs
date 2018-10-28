using System;
using UIs.Common;
using UniRx.Async;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Scenes
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField]
        string titleSceneName;
        [SerializeField]
        string gameCoreSceneName;
        [SerializeField]
        string levelSceneName;
        [SerializeField]
        string resultSceneName;

        [SerializeField]
        float playTimeSeconds;

        [Inject]
        SceneCurtain sceneCurtain;

        public void LoadTitle(bool immediately = false)
        {
            LoadScene(titleSceneName, immediately);
        }

        public void LoadScene(string sceneName, bool immediately = false)
        {
            LoadSceneWithCurtainAsync(sceneName, immediately).Forget();
        }

        public void LoadLevel()
        {
            LoadLevelAsync().Forget();
        }

        public void LoadResult()
        {
            LoadResultAsync().Forget();
        }

        async UniTask LoadSceneWithCurtainAsync(string sceneName, bool immediately)
        {
            await sceneCurtain.Show(immediately);
            await LoadSceneAsync(sceneName);
            await sceneCurtain.Hide();
        }

        async UniTask LoadLevelAsync()
        {
            await sceneCurtain.Show();
            await LoadSceneAsync(gameCoreSceneName);
            await LoadSceneAsync(levelSceneName, LoadSceneMode.Additive);
            await sceneCurtain.Hide();

            // FIXME: レベル側の終了条件みたいなやつに切り出す
            await UniTask.Delay(TimeSpan.FromSeconds(playTimeSeconds));
            await LoadResultAsync();
        }

        async UniTask LoadResultAsync()
        {
            await sceneCurtain.Show();
            await UniTask.WhenAll(
                UnloadSceneAsync(levelSceneName),
                LoadSceneAsync(resultSceneName, LoadSceneMode.Additive));
            await sceneCurtain.Hide();
        }

        static async UniTask LoadSceneAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
        {
            await SceneManager.LoadSceneAsync(sceneName, mode);
        }

        static async UniTask UnloadSceneAsync(string sceneName)
        {
            await SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}
