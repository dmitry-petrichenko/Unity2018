using System;
using Autofac;
using FeatureDistri;
using Scripts;
using Scripts.ActionDistributor;
using Scripts.GameLoop;
using Scripts.Map;
using Scripts.Settings;
using Scripts.Units.PathFinder;
using UnityEngine;
using Grid = Scripts.Units.PathFinder.Grid;

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
        builder.RegisterType<InputController>().AsSelf();
        builder.RegisterType<ActiveMapLocationController>().AsSelf().SingleInstance();
        builder.RegisterType<GameLoopController>().As<IGameLoopController>().SingleInstance();
        builder.RegisterType<CameraController>().As<ICameraController>().SingleInstance();    
        builder.RegisterType<GameEvents>().As<IGameEvents>().SingleInstance();
        builder.RegisterType<HeavyActionDistributor>().As<IHeavyActionDistributor>().SingleInstance();
        builder.RegisterType<HeavyActionsBunchesExecutor>().AsSelf().InstancePerDependency();
        builder.RegisterType<Grid>().As<IGrid>().SingleInstance();
        
        builder.RegisterModule<MapInstaller>();
        builder.RegisterModule<UnitsInstaller>();

        var container = builder.Build();
        container.Resolve<InputController>();
        container.Resolve<ActiveMapLocationController>();
    }
    
    void Update()
    {
        Updated?.Invoke();
    }
}

