using System;
using Autofac;
using FeatureDistri;
using Scripts;
using Scripts.ActionDistributor;
using Scripts.Extensions;
using Scripts.GameLoop;
using Scripts.Map;
using Scripts.Settings;
using Scripts._Client.Units.SingleUnit.Player;
using Units;
using Units.PathFinder;
using UnityEngine;
using Grid = Units.PathFinder.Grid;

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
        builder.RegisterType<ScopeTree>().As<IScopeTree>();
        
        builder.RegisterInstance(camera).As<Camera>();
        builder.RegisterInstance(this).As<GameInstaller>();
        builder.RegisterInstance(mapGraphicsSettings).As<MapGraphicsSettings>();
        
        builder.RegisterType<GameSettings>().As<ISettings>().SingleInstance();
        builder.RegisterType<InteractiveLocationController>().AsSelf().SingleInstance();
        builder.RegisterType<GameLoopController>().As<IGameLoopController>().SingleInstance();
        builder.RegisterType<CameraController>().As<ICameraController>().SingleInstance();    
        builder.RegisterType<GameEvents>().As<IGameEvents>().SingleInstance();
        builder.RegisterType<HeavyActionDistributor>().As<IHeavyActionDistributor>().SingleInstance();
        builder.RegisterType<HeavyActionsBunchesExecutor>().AsSelf().InstancePerDependency();
        builder.RegisterType<Grid>().As<IGrid>().SingleInstance();
        
        builder.RegisterModule<MapInstaller>();
        builder.RegisterModule<UnitsInstaller>();
        
        //-- new //--
        builder.RegisterType<PlayerViewObject>().As<PlayerViewObject>().SingleInstance();
        //-- new //--

        var container = builder.Build();
        //-- old //--
        //container.Resolve<InteractiveLocationController>();
        //-- old //--
        
        //-- new //--
        container.Resolve<PlayerViewObject>();
        //-- new //--
    }
    
    void Update()
    {
        Updated?.Invoke();
    }
}

