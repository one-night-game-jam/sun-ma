using UnityEngine;
using Zenject;

namespace Scenes
{
    public class LoadTitleImmediately : MonoBehaviour
    {
        [Inject] LevelLoader levelLoader;

        void Start()
        {
            levelLoader.LoadTitle(true);
        }
    }
}
