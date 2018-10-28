using Scenes;
using UIs.Common;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class LevelLoaderInstaller : MonoInstaller<LevelLoaderInstaller>
    {
        [SerializeField] LevelLoader levelLoaderPrefab;
        [SerializeField] SceneCurtain sceneCurtainPrefab;

        public override void InstallBindings()
        {
            Container.Bind<SceneCurtain>().FromComponentInNewPrefab(sceneCurtainPrefab).AsSingle();
            Container.Bind<LevelLoader>().FromComponentInNewPrefab(levelLoaderPrefab).AsSingle();
        }
    }
}