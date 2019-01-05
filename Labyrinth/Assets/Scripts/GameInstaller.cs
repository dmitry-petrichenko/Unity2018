using Autofac;
using UnityEngine;
using ZScripts;
using ZScripts.Map.View;

public class GameInstaller : MonoBehaviour
{
    public GameSettings gameSettings;

    void Start()
    {
        InstallRegistrations();
    }

    private void InstallRegistrations()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<MapViewController>().As<IMapViewController>();
        builder.RegisterInstance(gameObject).As<GameObject>();
        builder.RegisterInstance(gameSettings).As<GameSettings>();
        var container = builder.Build();

        var scope = container.BeginLifetimeScope();
        var mapViewController = scope.Resolve<IMapViewController>();
        mapViewController.InitializeCube(new IntVector2(0, 0));
        mapViewController.InitializeSquare(new IntVector2(2, 0));
        mapViewController.InitializeCube(new IntVector2(2, 2));
    }
}
