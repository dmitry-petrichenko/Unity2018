using Autofac;
using Autofac.Extras.AggregateService;
using ZScripts.Units;
using ZScripts.Units.PathFinder;
using ZScripts.Units.UnitActions;

public class UnitsInstaller : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAggregateService<IOneUnitServices>();
        builder.RegisterType<UnitsController>().AsSelf().SingleInstance().AutoActivate();
        builder.RegisterType<PathFinderController>().As<IPathFinderController>().SingleInstance();
        builder.RegisterType<Grid>().As<IGrid>().SingleInstance();
        builder.RegisterType<MoveToPositionAction>().AsSelf().InstancePerDependency();
        builder.RegisterType<OccupatedPossitionsTable>().As<IOccupatedPossitionsTable>().SingleInstance();
        builder.RegisterType<UnitsTable>().As<IUnitsTable>().SingleInstance();
        builder.RegisterType<MovingRandomizer>().As<IMovingRandomizer>().SingleInstance();
        builder.RegisterType<UnitBehaviourGenerator>().AsSelf().InstancePerDependency();
        
        //Container.Bind<EnemyController>().To<EnemyController>().FromSubContainerResolve().ByMethod(InstallEnemyController).AsTransient();
        //Container.Bind<IPlayerController>().To<PlayerController>().FromSubContainerResolve().ByMethod(InstallPlayerController).AsSingle();
    }
    /*
    private void InstallPlayerController(DiContainer subContainer)
    {
        subContainer.Bind<PlayerController>().AsSingle();
        InstallOneUnitSubComponents(subContainer);
    }
    
    private void InstallEnemyController(DiContainer subContainer)
    {
        subContainer.Bind<EnemyController>().AsSingle();
        InstallOneUnitSubComponents(subContainer);
    }

    private void InstallOneUnitSubComponents(DiContainer subContainer)
    {
        subContainer.Bind<MoveController>().To<MoveController>().AsSingle();
        subContainer.Bind<ISubMoveController>().To<SubMoveController>().AsSingle();
        subContainer.Bind<MoveToHandlerController>().To<MoveToHandlerController>().AsSingle();
        subContainer.Bind<AttackController>().To<AttackController>().AsSingle();
        subContainer.Bind<IAgressiveBehaviour>().To<AggressiveBehaviour>().AsSingle();
        subContainer.Bind<IOneUnitAnimationController>().To<OneUnitAnimationController>().AsSingle();
        subContainer.Bind<IOneUnitRotationController>().To<OneUnitRotationController>().AsSingle();
        subContainer.Bind<IOneUnitMotionController>().To<OneUnitMotionController>().AsSingle();
        subContainer.Bind<IPeacefulBehaviour>().To<PeacefulBehaviour>().AsSingle();
        subContainer.Bind<IUnitSettings>().To<UnitSettings>().AsSingle();
        subContainer.Bind<WaitMoveTurnController>().To<WaitMoveTurnController>().AsSingle();
        subContainer.Bind<MoveConsideringOccupatedController>().To<MoveConsideringOccupatedController>().AsSingle();
        subContainer.Bind<IUnitStateInfo>().To<UnitStateInfo>().AsSingle();
        subContainer.Bind<TargetOvertaker>().To<TargetOvertaker>().AsSingle();
        subContainer.Bind<IdleAction>().To<IdleAction>().AsTransient();
        subContainer.Bind<AttackAction>().To<AttackAction>().AsTransient();
        subContainer.Bind<INoWayEventRouter>().To<NoWayEventRouter>().AsSingle();
        subContainer.Bind<OvertakeOccupatedPositionController>().To<OvertakeOccupatedPositionController>().AsSingle();
    }*/
}