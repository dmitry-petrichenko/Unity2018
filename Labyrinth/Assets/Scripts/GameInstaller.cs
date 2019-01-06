using System;
using Autofac;
using UnityEngine;
using ZScripts;
using ZScripts.GameLoop;
using ZScripts.Map.View;
using ZScripts.Settings;

public class GameInstaller : MonoBehaviour
{
    public MapGraphicsSettings mapGraphicsSettings;
    public Camera camera;
    
    public event Action Updated;

    void Start()
    {
        InstallRegistrations();
    }

    private void InstallRegistrations()
    {
        var builder = new ContainerBuilder();
        builder.RegisterInstance(camera).As<Camera>();
        builder.RegisterInstance(this).As<GameInstaller>();
        builder.RegisterInstance(gameObject).As<GameObject>();
        builder.RegisterInstance(mapGraphicsSettings).As<MapGraphicsSettings>();
        builder.RegisterType<GameSettings>().As<ISettings>().SingleInstance();
        builder.RegisterType<InputController>().AsSelf().AutoActivate();
        builder.RegisterType<ActiveMapLocationController>().AsSelf().SingleInstance().AutoActivate();
        builder.RegisterType<GameLoopController>().As<IGameLoopController>().SingleInstance();
        builder.RegisterType<CameraController>().As<ICameraController>().SingleInstance();

        //Container.Bind<IMapSectorController>().To<MapSectorController>().AsSingle().NonLazy();
        
        builder.RegisterType<MapViewController>().As<IMapViewController>();
        
        
        
        var container = builder.Build();
        var scope = container.BeginLifetimeScope();
        var mapViewController = scope.Resolve<IMapViewController>();
        mapViewController.InitializeCube(new IntVector2(0, 0));
        mapViewController.InitializeSquare(new IntVector2(2, 0));
        mapViewController.InitializeCube(new IntVector2(2, 2));
    }
    
    void Update()
    {
        Updated?.Invoke();
    }
}
