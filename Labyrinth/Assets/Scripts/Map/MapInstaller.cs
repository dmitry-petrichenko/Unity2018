using Autofac;
using Scripts.Map.Controllers;
using Scripts.Map.Info;
using Scripts.Map.View;
using Scripts.Units.PathFinder;

namespace Scripts.Map
{
    public class MapInstaller : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Grid>().As<IGrid>().SingleInstance();
            builder.RegisterType<MapSectorController>().As<IMapSectorController>().SingleInstance().AutoActivate();    
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