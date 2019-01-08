using System;
using Autofac;
using FeatureDistri;
using UnityEngine;
using ZScripts;
using ZScripts.ActionDistributor;
using ZScripts.GameLoop;
using ZScripts.Map;
using ZScripts.Map.Info;
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
        builder.RegisterType<MapSectorController>().As<IMapSectorController>().SingleInstance().AutoActivate();        
        builder.RegisterType<GameEvents>().As<IGameEvents>().SingleInstance();
        builder.RegisterType<HeavyActionDistributor>().As<IHeavyActionDistributor>().SingleInstance();
        builder.RegisterType<HeavyActionsBunchesExecutor>().AsSelf().InstancePerDependency();
        
        builder.RegisterModule<MapInstaller>();
        builder.RegisterModule<UnitsInstaller>();

        var container = builder.Build();
        var scope = container.BeginLifetimeScope();
//        var mapViewController = scope.Resolve<IMapViewController>();
//        mapViewController.InitializeCube(new IntVector2(0, 0));
//        mapViewController.InitializeSquare(new IntVector2(2, 0));
//        mapViewController.InitializeCube(new IntVector2(2, 2));
    }
    
    void Update()
    {
        Updated?.Invoke();
    }
}

