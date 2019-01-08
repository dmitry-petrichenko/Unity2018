using System;
using System.Xml.Linq;
using Autofac;
using Autofac.Builder;
using UnityEditor;
using UnityEngine;
using ZScripts;
using ZScripts.GameLoop;
using ZScripts.Map;
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

        builder.RegisterModule<MapInstaller>();
        //---------
        //builder.RegisterType<ExampleDependency>().As<IExampleDependency>().SingleInstance();
        //builder.RegisterType<ExampleClass>().AsSelf();

        builder.RegisterType<ExampleClassChild>().AsSelf();
        builder.RegisterType<ExampleClass>().AsSelf();
        builder.Register(c =>
        {
            var registration = RegistrationBuilder.ForType<ExampleDependency>()
                .As<IExampleDependency>()
                .SingleInstance()
                .CreateRegistration();
            c.ComponentRegistry.Register(registration);
            return c.Resolve<ExampleClass>();
        }).As<IExampleClass>();
        
//        builder.Register(c =>
//        {
//            c.ComponentRegistry.Register(null);
//            c.ComponentRegistry.Register(null);
//            return new GameSettings();
//        }).As<ISettings>();
        //Container.Bind<IMapSectorController>().To<MapSectorController>().AsSingle().NonLazy();
        //---------
        
        
        builder.RegisterType<MapViewController>().As<IMapViewController>();
        
        
        
        var container = builder.Build();
        var scope = container.BeginLifetimeScope();
        var ex = scope.Resolve<ExampleClass>();
        ex = scope.Resolve<ExampleClass>();
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

public class ExampleClassChild
{
    public ExampleClassChild(IExampleDependency exampleDependency)
    {
        Console.WriteLine( exampleDependency.Property + " value");
        Debug.Log( exampleDependency.Property + " value");
        exampleDependency.Property++;
    }
}

public class ExampleClass : IExampleClass
{
    public ExampleClass(ExampleClassChild exampleClassChild, ExampleClassChild exampleClassChild2)
    {
    }
}

public class IExampleClass
{
}

public interface IExampleDependency
{
    int Property { get; set; }
}

public class ExampleDependency : IExampleDependency
{
    public int Property { get; set; } = 0;
}
