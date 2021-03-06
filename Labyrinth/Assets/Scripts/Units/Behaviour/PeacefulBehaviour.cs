﻿using System.Collections.Generic;
using Scripts.ActionDistributor;
using Scripts.Extensions;
using Scripts.Units.UnitActions;
using Units.OneUnit;

namespace Scripts.Units
{
    public class PeacefulBehaviour : Disposable, IPeacefulBehaviour
    {
        private IUnitAction _currentUnitAction;
        private IHeavyActionDistributor _heavyActionDistributor;
        private MoveToPositionAction.Factory _moveActionFactory;
        private IdleAction.Factory _idleActionFactory;
        private IOneUnitController _oneUnitController;
        private UnitBehaviourGenerator _unitBehaviourGenerator;

        public PeacefulBehaviour(
            IHeavyActionDistributor heavyActionDistributor, 
            MoveToPositionAction.Factory moveActionFactory,
            IdleAction.Factory idleActionFactory,
            UnitBehaviourGenerator unitBehaviourGenerator
            )
        {
            _idleActionFactory = idleActionFactory;
            _moveActionFactory = moveActionFactory;
            _heavyActionDistributor = heavyActionDistributor;
            _unitBehaviourGenerator = unitBehaviourGenerator;
        }
        
        public void Initialize(IOneUnitController oneUnitController)
        {
            _oneUnitController = oneUnitController;
        }

        public void Start()
        {
            List<IUnitAction> actions = new List<IUnitAction>();
            
            IUnitAction action;
            action = _idleActionFactory.Invoke();
            action.Initialize(_oneUnitController);
            actions.Add(action);

            action = _moveActionFactory.Invoke();
            action.Initialize(_oneUnitController);
            //actions.Add(action);
            
            _unitBehaviourGenerator.Initialize(actions);
            
            _unitBehaviourGenerator.Start();
        }

        public void Cancel()
        {
            _unitBehaviourGenerator.Stop();
        }

        public void DisposeInternal()
        {
            Cancel();
            _oneUnitController = null;
            base.DisposeInternal();
        }
    }
}