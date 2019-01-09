using System;
using Autofac;
using FeatureDistri;
using UnityEngine;
using Scripts;
using Scripts.ActionDistributor;
using Scripts.GameLoop;
using Scripts.Map;
using Scripts.Map.Info;
using Scripts.Settings;

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
    }
    
    void Update()
    {
        Updated?.Invoke();
    }
}

