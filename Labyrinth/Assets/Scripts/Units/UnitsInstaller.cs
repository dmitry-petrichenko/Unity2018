using Autofac;
using Scripts.AutofacExtensions;
using Units;
using Scripts.Units;
using Scripts.Units.Behaviour.UnitActions;
using Scripts.Units.Enemy;
using Scripts.Units.PathFinder;
using Scripts.Units.Player;
using Scripts.Units.Rotation;
using Scripts.Units.Settings;
using Scripts.Units.StateInfo;
using Scripts.Units.UnitActions;

public class UnitsInstaller : Module
{
    protected override void Load(ContainerBuilder builder)
    {   
        builder.RegisterType<OneUnitServices>().As<IOneUnitServices>().PropertiesAutowired();
        builder.RegisterType<UnitsController>().AsSelf().SingleInstance().AutoActivate();
        builder.RegisterType<PathFinderController>().As<IPathFinderController>().SingleInstance();
        builder.RegisterType<MoveToPositionAction>().AsSelf().InstancePerDependency();
        builder.RegisterType<OccupatedPossitionsTable>().As<IOccupatedPossitionsTable>().SingleInstance();
        builder.RegisterType<UnitsTable>().As<IUnitsTable>().SingleInstance();
        builder.RegisterType<MovingRandomizer>().As<IMovingRandomizer>().SingleInstance();
        builder.RegisterType<UnitBehaviourGenerator>().AsSelf().InstancePerDependency();

        builder.Register(c =>
        {
            InstallOneUnitSubComponents(c);
            return new EnemyController(c.Resolve<IOneUnitServices>());
        }).As<EnemyController>().InstancePerDependency();
        
        builder.Register(c =>
        {
            InstallOneUnitSubComponents(c);
            return new PlayerController(c.Resolve<IOneUnitServices>());
        }).As<IPlayerController>().SingleInstance();
    }

    private void InstallOneUnitSubComponents(IComponentContext componentContext)
    {
        ContextRegistrator contextRegistrator = new ContextRegistrator(componentContext);
        
        contextRegistrator.RegisterType<MoveController>().AsSelf().SingleInstance();
        contextRegistrator.RegisterType<SubMoveController>().As<ISubMoveController>().SingleInstance();
        contextRegistrator.RegisterType<MoveToHandlerController>().AsSelf().SingleInstance();
        contextRegistrator.RegisterType<AttackController>().AsSelf().SingleInstance();
        contextRegistrator.RegisterType<AggressiveBehaviour>().As<IAgressiveBehaviour>().SingleInstance();
        contextRegistrator.RegisterType<OneUnitAnimationController>().As<IOneUnitAnimationController>().SingleInstance();
        contextRegistrator.RegisterType<OneUnitRotationController>().As<IOneUnitRotationController>().SingleInstance();
        contextRegistrator.RegisterType<OneUnitMotionController>().As<IOneUnitMotionController>().SingleInstance();
        contextRegistrator.RegisterType<PeacefulBehaviour>().As<IPeacefulBehaviour>().SingleInstance();
        contextRegistrator.RegisterType<UnitSettings>().As<IUnitSettings>().SingleInstance();
        contextRegistrator.RegisterType<WaitMoveTurnController>().AsSelf().SingleInstance();
        contextRegistrator.RegisterType<MoveConsideringOccupatedController>().AsSelf().SingleInstance();
        contextRegistrator.RegisterType<UnitStateInfo>().As<IUnitStateInfo>().SingleInstance();
        contextRegistrator.RegisterType<TargetOvertaker>().AsSelf().SingleInstance();
        contextRegistrator.RegisterType<IdleAction>().AsSelf().InstancePerDependency();
        contextRegistrator.RegisterType<AttackAction>().AsSelf().InstancePerDependency();
        contextRegistrator.RegisterType<NoWayEventRouter>().As<INoWayEventRouter>().SingleInstance();
        contextRegistrator.RegisterType<OvertakeOccupatedPositionController>().AsSelf().SingleInstance();
        
        contextRegistrator.CreateRegistrations();
    }
}