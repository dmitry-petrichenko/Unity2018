using Autofac;
using ID5D6AAC.Common.EventDispatcher;
using Scripts.Extensions;
using Scripts.Units;
using Scripts.Units.Behaviour.UnitActions;
using Scripts.Units.Enemy;
using Scripts.Units.Events;
using Scripts.Units.Settings;
using Scripts.Units.StateInfo;
using Scripts.Units.StateInfo.BaseState;
using Scripts.Units.StateInfo.LivingStates;
using Scripts.Units.StateInfo.LivingStates.States;
using Scripts.Units.UnitActions;
using Units.ExternalAPI;
using Units.OneUnit;
using Units.OneUnit.Base;
using Units.OneUnit.Base.GameObject;
using Units.OneUnit.Base.GameObject.Animation;
using Units.OneUnit.Base.GameObject.Health;
using Units.OneUnit.Base.GameObject.Motion;
using Units.OneUnit.Base.GameObject.Rotation;
using Units.OneUnit.Info;
using Units.PathFinder;
using Units.Player;

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
            builder.RegisterType<OccupatedPossitionsTable>().As<IOccupatedPossitionsTable>().SingleInstance();
            builder.RegisterType<UnitsTable>().As<IUnitsTable>().SingleInstance();
            builder.RegisterType<MovingRandomizer>().As<IMovingRandomizer>().SingleInstance();
            builder.RegisterType<UnitBehaviourGenerator>().AsSelf().InstancePerDependency();
        
            builder.CreateScopeForType<EnemyController>(InstallEnemyComponents).As<EnemyController>().InstancePerDependency();
            builder.CreateScopeForType<PlayerController>(InstallPlayerComponents).As<IPlayerController>().SingleInstance();
        }

        private void InstallPlayerComponents(ContainerBuilder builder)
        {
            builder.RegisterType<PlayerSettings>().As<IUnitSettings>().SingleInstance();
            builder.RegisterType<AttackController>().As<IAttackController>().SingleInstance();
            InstallOneUnitComponents(builder);
        }
    
        private void InstallEnemyComponents(ContainerBuilder builder)
        {
            builder.RegisterType<EnemySettings>().As<IUnitSettings>().SingleInstance();
            builder.RegisterType<AttackController>().As<IAttackController>().SingleInstance();
            InstallOneUnitComponents(builder);
        }

        private void InstallOneUnitComponents(ContainerBuilder builder)
        {
            builder.CreateScopeForType<BaseActionController>(InstallBaseMovingSubComponents)
                .As<IBaseActionController>().SingleInstance();
            
            builder.RegisterType<DeadLivingState>().AsSelf().SingleInstance();
            builder.RegisterType<AliveLivingState>().AsSelf().SingleInstance();
            builder.RegisterType<LivingStateController>().As<ILivingStateControllerInternal>().As<ILivingStateControllerExternal>().SingleInstance();
            
            builder.RegisterType<StateController>().As<IStateControllerExternal>().As<IStateControllerInternal>().As<IStateControllerMutator>().SingleInstance();
            builder.RegisterType<UnitInfo>().As<IUnitInfoExternal>().As<IUnitInfoInternal>().SingleInstance();
            
            
            builder.RegisterType<EventDispatcher>().As<IEventDispatcher>().SingleInstance();
            builder.RegisterType<MoveController>().AsSelf().SingleInstance();
            builder.RegisterType<AggressiveBehaviour>().As<IAgressiveBehaviour>().SingleInstance();
            builder.RegisterType<PeacefulBehaviour>().As<IPeacefulBehaviour>().SingleInstance();
            builder.RegisterType<WaitMoveTurnController>().AsSelf().SingleInstance();
            builder.RegisterType<MoveConsideringOccupatedController>().AsSelf().SingleInstance();
            builder.RegisterType<TargetOvertaker2>().AsSelf().SingleInstance();
            builder.RegisterType<IdleAction>().AsSelf().InstancePerDependency();
            builder.RegisterType<AttackAction>().AsSelf().InstancePerDependency();
            //builder.RegisterType<OvertakeOccupatedPositionController>().AsSelf().SingleInstance();
            builder.RegisterType<DeathController>().As<IDeathController>().SingleInstance();
            builder.RegisterType<UnitEvents>().As<IUnitEvents>().SingleInstance();
            builder.RegisterType<HealthController>().As<IHealthController>().SingleInstance();
            builder.RegisterType<ApplyDamageController>().As<IApplyDamageController>().SingleInstance();
            
            builder.CreateScopeForType<UnitGameObjectController>(InstallUnitGameObjectComponents).As<IUnitGameObjectController>().SingleInstance();
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
        }
    }
}