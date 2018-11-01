using Scenes;
using UIs.Common;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class LevelLoaderInstaller : MonoInstaller<LevelLoaderInstaller>
    {
        [SerializeField] SceneCurtain sceneCurtainPrefab;

        public override void InstallBindings()
        {
            Container.Bind<LevelLoader>().AsSingle();
            Container.Bind<SceneCurtain>().FromComponentInNewPrefab(sceneCurtainPrefab).AsSingle();
        }
    }
}