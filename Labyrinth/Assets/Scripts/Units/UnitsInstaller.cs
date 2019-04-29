using Autofac;
using ID5D6AAC.Common.EventDispatcher;
using Scripts.Extensions;
using Scripts.Units;
using Scripts.Units.Behaviour.UnitActions;
using Scripts.Units.Enemy;
using Scripts.Units.Events;
using Scripts.Units.Settings;
using Scripts.Units.UnitActions;
using Units.ExternalAPI;
using Units.OccupatedMap;
using Units.OneUnit;
using Units.OneUnit.Info;
using Units.OneUnit.State;
using Units.OneUnit.State.States;
using Units.OneUnit.StatesControllers.Base;
using Units.OneUnit.StatesControllers.Base.GameObject;
using Units.OneUnit.StatesControllers.Base.GameObject.Animation;
using Units.OneUnit.StatesControllers.Base.GameObject.Health;
using Units.OneUnit.StatesControllers.Base.GameObject.Motion;
using Units.OneUnit.StatesControllers.Base.GameObject.Rotation;
using Units.OneUnit.StatesControllers.Base.Settings;
using Units.OneUnit.StatesControllers.Hostile;
using Units.OneUnit.StatesControllers.Placid;
using Units.PathFinder;
using Units.Player;
using UnitTests.StatesControllers.Hostile;

namespace Units
{
    public class UnitsInstaller : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.CreateScopeForType<UnitsController>(InstallUnitsComponents).As<IUnitsController>().SingleInstance();
        }

        private void InstallUnitsComponents(ContainerBuilder builder)
        {
            builder.RegisterType<UnitsController>().AsSelf().SingleInstance();
            builder.RegisterType<PathFinderController>().As<IPathFinderController>().SingleInstance();
            builder.RegisterType<MoveToPositionAction>().AsSelf().InstancePerDependency();
            builder.RegisterType<OccupatedPossitionsMap>().As<IOccupatedPossitionsMap>().SingleInstance();
            builder.RegisterType<MovingRandomizer>().As<IMovingRandomizer>().SingleInstance();
            builder.RegisterType<UnitBehaviourGenerator>().AsSelf().InstancePerDependency();
        
            builder.CreateScopeForType<EnemyController>(InstallEnemyComponents).As<EnemyController>().InstancePerDependency();
            builder.CreateScopeForType<PlayerController>(InstallPlayerComponents).As<IPlayerController>().SingleInstance();
        }

        private void InstallPlayerComponents(ContainerBuilder builder)
        {
            builder.RegisterType<PlayerSettings>().As<IUnitSettings>().SingleInstance();
            InstallOneUnitComponents(builder);
        }
    
        private void InstallEnemyComponents(ContainerBuilder builder)
        {
            builder.RegisterType<EnemySettings>().As<IUnitSettings>().SingleInstance();
            InstallOneUnitComponents(builder);
        }

        private void InstallOneUnitComponents(ContainerBuilder builder)
        {
            builder.CreateScopeForType<BaseActionController>(InstallBaseMovingSubComponents)
                .As<IBaseActionController>().SingleInstance();
            
            builder.RegisterType<UnitInfo>().As<IUnitInfoExternal>().As<IUnitInfoInternal>().SingleInstance();
            builder.CreateScopeForType<StateController>(InitializeStateComponents).As<IStateControllerExternal>().As<IStateControllerInternal>().SingleInstance();
            builder.RegisterType<LifeController>().As<ILifeController>().SingleInstance();
            builder.RegisterType<HostileController>().As<IHostileController>().SingleInstance();
            builder.RegisterType<FreePositionsMap>().As<IFreePossitionsMap>().SingleInstance();
            builder.RegisterType<EventDispatcher>().As<IEventDispatcher>().SingleInstance();
            builder.RegisterType<PlacidController>().As<IPlacidController>().SingleInstance();
            builder.RegisterType<AggressiveBehaviour>().As<IAgressiveBehaviour>().SingleInstance();
            builder.RegisterType<PeacefulBehaviour>().As<IPeacefulBehaviour>().SingleInstance();
            builder.RegisterType<NoWayPlacidController>().AsSelf().SingleInstance();
            builder.RegisterType<TargetOvertaker>().AsSelf().SingleInstance();
            builder.RegisterType<IdleAction>().AsSelf().InstancePerDependency();
            builder.RegisterType<AttackAction>().AsSelf().InstancePerDependency();
            builder.RegisterType<NoWayHostileController>().AsSelf().SingleInstance();
            builder.RegisterType<UnitEvents>().As<IUnitEvents>().SingleInstance();
            builder.RegisterType<ApplyDamageController>().As<IApplyDamageController>().SingleInstance();
            builder.RegisterType<WaveEventRaiser>().As<IWaveEventRiser>().SingleInstance();
            builder.RegisterType<WayHostileController>().As<IWayHostileController>().SingleInstance();
            builder.RegisterType<FreePointToGoResolver>().As<IFreePointToGoResolver>().SingleInstance();
            builder.RegisterType<WaitObstacleController>().As<IWaitObstacleController>().SingleInstance();
            builder.RegisterType<WayHostileControllerParameters>().As<IWayHostileControllerParameters>().SingleInstance();
            
            builder.RegisterType<AdjacentPointsResolver>().As<IAdjacentPointsResolver>().SingleInstance();
            
            builder.CreateScopeForType<UnitGameObjectController>(InstallUnitGameObjectComponents).As<IUnitGameObjectController>().SingleInstance();
        }

        private void InitializeStateComponents(ContainerBuilder builder)
        {
            builder.RegisterType<HostileState>().As<IHostileState>().SingleInstance();
            builder.RegisterType<PlacidState>().As<IPlacidState>().SingleInstance();
            builder.RegisterType<DeadState>().As<IDeadState>().SingleInstance();
        }

        private void InstallUnitGameObjectComponents(ContainerBuilder builder)
        {      
            builder.RegisterType<OneUnitAnimationController>().As<IOneUnitAnimationController>().SingleInstance();
            builder.RegisterType<OneUnitRotationController>().As<IOneUnitRotationController>().SingleInstance();
            builder.RegisterType<OneUnitMotionController>().As<IOneUnitMotionController>().SingleInstance();
            builder.RegisterType<OneUnitHealthController>().As<IOneUnitHealthController>().SingleInstance();
        }

        private void InstallBaseMovingSubComponents(ContainerBuilder builder)
        {
            builder.RegisterType<ChangeDirrectionAfterMoveTileCompleteController>().AsSelf().SingleInstance();
            builder.RegisterType<MoveStepByStepController>().As<IMoveStepByStepController>().SingleInstance();
            builder.RegisterType<PathGeneratorController>().As<IPathGeneratorController>().SingleInstance();
            builder.RegisterType<MoveConsideringOccupatedController>().AsSelf().SingleInstance();
        }
    }
}