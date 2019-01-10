using Autofac;
using Scripts.Map.Controllers;
using Scripts.Map.Info;
using Scripts.Map.View;

namespace Scripts.Map
{
    public class MapInstaller : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                ILifetimeScope scope = context.Resolve<ILifetimeScope>().BeginLifetimeScope(InstallMapComponents);
                return new MapController(
                    scope.Resolve<MapViewUpdateController>(),
                    scope.Resolve<MapInfoUpdateController>(),
                    scope.Resolve<IMapViewController>(),
                    scope.Resolve<IMapInfoController>());
            }).As<IMapController>().SingleInstance();
        }

        private void InstallMapComponents(ContainerBuilder builder)
        {
            builder.RegisterType<MapSectorController>().As<IMapSectorController>().SingleInstance(); 
            builder.RegisterType<MapViewUpdateController>().AsSelf().SingleInstance();
            builder.RegisterType<MapInfoUpdateController>().AsSelf().SingleInstance();
            builder.RegisterType<MapViewController>().As<IMapViewController>().SingleInstance();
            builder.RegisterType<MapInfoController>().As<IMapInfoController>().SingleInstance();
            builder.RegisterType<MapInfoStoreController>().As<IMapInfoStoreController>().SingleInstance();
            builder.RegisterType<SectorLifecycleController>().AsSelf().SingleInstance();
        }
    }
}