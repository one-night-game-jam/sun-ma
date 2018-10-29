using UnityEngine;
using Zenject;

namespace Scenes
{
    public class DebugLevelSelect : MonoBehaviour
    {
        [SerializeField]
        string[] levels;

        [Inject]
        LevelLoader levelLoader;

        Vector2 scrollPosition;

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        void OnGUI()
        {
            GUILayout.Space(100);
            using (var scrollViewScope = new GUILayout.ScrollViewScope(scrollPosition, GUILayout.Width(200), GUILayout.Height(200)))
            {
                scrollPosition = scrollViewScope.scrollPosition;

                foreach (var level in levels)
                {
                    if (GUILayout.Button(level, GUILayout.Height(50)))
                    {
                        levelLoader.LoadLevel(level);
                    }
                }
            }
        }
    }
}
