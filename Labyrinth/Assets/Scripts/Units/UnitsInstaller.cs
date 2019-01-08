using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Units;
using ZScripts.Units;
using ZScripts.Units.Behaviour.UnitActions;
using ZScripts.Units.Enemy;
using ZScripts.Units.PathFinder;
using ZScripts.Units.Player;
using ZScripts.Units.Rotation;
using ZScripts.Units.Settings;
using ZScripts.Units.StateInfo;
using ZScripts.Units.UnitActions;

public class UnitsInstaller : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<OneUnitServices>().As<IOneUnitServices>().PropertiesAutowired();
        builder.RegisterType<UnitsController>().AsSelf().SingleInstance().AutoActivate();
        builder.RegisterType<PathFinderController>().As<IPathFinderController>().SingleInstance();
        builder.RegisterType<Grid>().As<IGrid>().SingleInstance();
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
        IComponentRegistration registration;
        registration =
            RegistrationBuilder.ForType<MoveController>().AsSelf().SingleInstance().CreateRegistration();
        componentContext.ComponentRegistry.Register(registration);

        registration =
            RegistrationBuilder.ForType<SubMoveController>().As<ISubMoveController>().SingleInstance()
                .CreateRegistration();
        componentContext.ComponentRegistry.Register(registration);

        registration =
            RegistrationBuilder.ForType<MoveToHandlerController>().AsSelf().SingleInstance().CreateRegistration();
        componentContext.ComponentRegistry.Register(registration);

        registration =
            RegistrationBuilder.ForType<AttackController>().AsSelf().SingleInstance().CreateRegistration();
        componentContext.ComponentRegistry.Register(registration);

        registration =
            RegistrationBuilder.ForType<AggressiveBehaviour>().As<IAgressiveBehaviour>().SingleInstance()
                .CreateRegistration();
        componentContext.ComponentRegistry.Register(registration);

        registration =
            RegistrationBuilder.ForType<OneUnitAnimationController>().As<IOneUnitAnimationController>().SingleInstance()
                .CreateRegistration();
        componentContext.ComponentRegistry.Register(registration);

        registration =
            RegistrationBuilder.ForType<OneUnitRotationController>().As<IOneUnitRotationController>().SingleInstance()
                .CreateRegistration();
        componentContext.ComponentRegistry.Register(registration);

        registration =
            RegistrationBuilder.ForType<OneUnitMotionController>().As<IOneUnitMotionController>().SingleInstance()
                .CreateRegistration();
        componentContext.ComponentRegistry.Register(registration);

        registration =
            RegistrationBuilder.ForType<PeacefulBehaviour>().As<IPeacefulBehaviour>().SingleInstance()
                .CreateRegistration();
        componentContext.ComponentRegistry.Register(registration);

        registration =
            RegistrationBuilder.ForType<UnitSettings>().As<IUnitSettings>().SingleInstance().CreateRegistration();
        componentContext.ComponentRegistry.Register(registration);

        registration =
            RegistrationBuilder.ForType<WaitMoveTurnController>().AsSelf().SingleInstance().CreateRegistration();
        componentContext.ComponentRegistry.Register(registration);

        registration =
            RegistrationBuilder.ForType<MoveConsideringOccupatedController>().AsSelf().SingleInstance()
                .CreateRegistration();
        componentContext.ComponentRegistry.Register(registration);

        registration =
            RegistrationBuilder.ForType<UnitStateInfo>().As<IUnitStateInfo>().SingleInstance().CreateRegistration();
        componentContext.ComponentRegistry.Register(registration);

        registration =
            RegistrationBuilder.ForType<TargetOvertaker>().AsSelf().SingleInstance().CreateRegistration();
        componentContext.ComponentRegistry.Register(registration);

        registration =
            RegistrationBuilder.ForType<IdleAction>().AsSelf().InstancePerDependency().CreateRegistration();
        componentContext.ComponentRegistry.Register(registration);

        registration =
            RegistrationBuilder.ForType<AttackAction>().AsSelf().InstancePerDependency().CreateRegistration();
        componentContext.ComponentRegistry.Register(registration);

        registration =
            RegistrationBuilder.ForType<NoWayEventRouter>().As<INoWayEventRouter>().SingleInstance()
                .CreateRegistration();
        componentContext.ComponentRegistry.Register(registration);

        registration =
            RegistrationBuilder.ForType<OvertakeOccupatedPositionController>().AsSelf().SingleInstance()
                .CreateRegistration();
        componentContext.ComponentRegistry.Register(registration);
    }
}