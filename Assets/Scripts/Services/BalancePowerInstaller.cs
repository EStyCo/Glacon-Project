using Zenject;

public class BalancePowerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<BalancePower>().FromComponentInHierarchy().AsSingle();
    }
}