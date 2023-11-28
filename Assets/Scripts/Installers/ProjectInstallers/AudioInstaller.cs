using UnityEngine;
using Zenject;

public class AudioInstaller : MonoInstaller
{
    [SerializeField] private MusicManager musicManager;

    public override void InstallBindings()
    {
        Container.Bind<MusicManager>().FromInstance(musicManager).AsSingle().NonLazy();
    }
}
