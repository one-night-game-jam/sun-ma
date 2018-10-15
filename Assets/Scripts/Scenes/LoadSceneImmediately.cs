using UniRx.Async;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes
{
    public class LoadSceneImmediately : MonoBehaviour
    {
        [SerializeField] string sceneName;

        async void Awake()
        {
            await SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
