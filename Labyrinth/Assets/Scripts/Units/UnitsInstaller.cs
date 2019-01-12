using Autofac;
using ID5D6AAC.Common.EventDispatcher;
using Scripts.Extensions;
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
using Units.ExternalAPI;

public class UnitsInstaller : Module
{
    protected override void Load(ContainerBuilder builder)
    {   
        builder.CreateScopeForType<UnitsController>(InstallUnitsComponents).As<IUnitsController>().SingleInstance();
    }

    private void InstallUnitsComponents(ContainerBuilder builder)
    {
        builder.RegisterType<OneUnitServices>().As<IOneUnitServices>().PropertiesAutowired();
        builder.RegisterType<UnitsController>().AsSelf().SingleInstance();
        builder.RegisterType<PathFinderController>().As<IPathFinderController>().SingleInstance();
        builder.RegisterType<MoveToPositionAction>().AsSelf().InstancePerDependency();
        builder.RegisterType<OccupatedPossitionsTable>().As<IOccupatedPossitionsTable>().SingleInstance();
        builder.RegisterType<UnitsTable>().As<IUnitsTable>().SingleInstance();
        builder.RegisterType<MovingRandomizer>().As<IMovingRandomizer>().SingleInstance();
        builder.RegisterType<UnitBehaviourGenerator>().AsSelf().InstancePerDependency();
        
        builder.CreateScopeForType<EnemyController>(InstallOneUnitSubComponents).As<EnemyController>().InstancePerDependency();
        builder.CreateScopeForType<PlayerController>(InstallOneUnitSubComponents).As<IPlayerController>().SingleInstance();
    }

    private void InstallOneUnitSubComponents(ContainerBuilder builder)
    {
        builder.RegisterType<EventDispatcher>().As<IEventDispatcher>().SingleInstance();
        builder.RegisterType<MoveController>().AsSelf().SingleInstance();
        builder.RegisterType<SubMoveController>().As<ISubMoveController>().SingleInstance();
        builder.RegisterType<BaseMovingController>().AsSelf().SingleInstance();
        builder.RegisterType<AttackController>().AsSelf().SingleInstance();
        builder.RegisterType<AggressiveBehaviour>().As<IAgressiveBehaviour>().SingleInstance();
        builder.RegisterType<OneUnitAnimationController>().As<IOneUnitAnimationController>().SingleInstance();
        builder.RegisterType<OneUnitRotationController>().As<IOneUnitRotationController>().SingleInstance();
        builder.RegisterType<OneUnitMotionController>().As<IOneUnitMotionController>().SingleInstance();
        builder.RegisterType<PeacefulBehaviour>().As<IPeacefulBehaviour>().SingleInstance();
        builder.RegisterType<UnitSettings>().As<IUnitSettings>().SingleInstance();
        builder.RegisterType<WaitMoveTurnController>().AsSelf().SingleInstance();
        builder.RegisterType<MoveConsideringOccupatedController>().AsSelf().SingleInstance();
        builder.RegisterType<UnitStateInfo>().As<IUnitStateInfo>().SingleInstance();
        builder.RegisterType<TargetOvertaker>().AsSelf().SingleInstance();
        builder.RegisterType<IdleAction>().AsSelf().InstancePerDependency();
        builder.RegisterType<AttackAction>().AsSelf().InstancePerDependency();
        builder.RegisterType<NoWayEventRouter>().As<INoWayEventRouter>().SingleInstance();
        builder.RegisterType<OvertakeOccupatedPositionController>().AsSelf().SingleInstance();
    }
}