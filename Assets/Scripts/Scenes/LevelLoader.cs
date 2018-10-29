using UIs.Common;
using UniRx.Async;
using UnityEngine.SceneManagement;

namespace Scenes
{
    public class LevelLoader
    {
        const string TitleSceneName = "Title";
        const string GameCoreSceneName = "GameCore";
        const string LevelSceneName = "Level";
        const string ResultSceneName = "Result";

        readonly SceneCurtain sceneCurtain;

        public LevelLoader(SceneCurtain sceneCurtain)
        {
            this.sceneCurtain = sceneCurtain;
        }

        public void LoadTitle(bool immediately = false)
        {
            LoadScene(TitleSceneName, immediately);
        }

        public void LoadScene(string sceneName, bool immediately = false)
        {
            LoadSceneWithCurtainAsync(sceneName, immediately).Forget();
        }

        public void LoadLevel(string levelSceneName = LevelSceneName)
        {
            LoadLevelAsync(levelSceneName).Forget();
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

        async UniTask LoadLevelAsync(string levelSceneName)
        {
            await sceneCurtain.Show();
            await LoadSceneAsync(GameCoreSceneName);
            await LoadSceneAsync(levelSceneName, LoadSceneMode.Additive);
            await sceneCurtain.Hide();
        }

        async UniTask LoadResultAsync()
        {
            await sceneCurtain.Show();
            await UniTask.WhenAll(
                UnloadSceneAsync(LevelSceneName),
                LoadSceneAsync(ResultSceneName, LoadSceneMode.Additive));
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
