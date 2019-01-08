using Autofac;
using ZScripts.Map.Controllers;
using ZScripts.Map.Info;
using ZScripts.Map.View;

namespace ZScripts.Map
{
    public class MapInstaller : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MapController>().As<IMapController>().AutoActivate();
            builder.RegisterType<MapViewUpdateController>().AsSelf().SingleInstance();
            builder.RegisterType<MapInfoUpdateController>().AsSelf().SingleInstance();
            builder.RegisterType<MapViewController>().As<IMapViewController>().SingleInstance();
            builder.RegisterType<MapInfoController>().As<IMapInfoController>().SingleInstance();
            builder.RegisterType<MapInfoStoreController>().As<IMapInfoStoreController>().SingleInstance();
            builder.RegisterType<SectorLifecycleController>().AsSelf().SingleInstance();
        }
    }
}