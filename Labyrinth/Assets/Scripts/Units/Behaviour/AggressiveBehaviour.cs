﻿using System;
using Scripts.Units.Events;
using Scripts.Units.StateInfo;
using Units.OneUnit;

namespace Scripts.Units
{
    public class AggressiveBehaviour : IAgressiveBehaviour
    {
        public event Action Complete;

        private IOneUnitController _target;
        
        private readonly IAttackController _attackController;
        private readonly IStateInfo _stateInfo;
        private readonly IUnitEvents _unitEvents;

        public AggressiveBehaviour(
            IAttackController attackController,
            IStateInfo stateInfo,
            IUnitEvents unitEvents
            )
        {
            _attackController = attackController;
            _stateInfo = stateInfo;
            _unitEvents = unitEvents;
        }
        
        public void Initialize(IOneUnitController oneUnitController)
        {
            _unitEvents.AttackComplete += AttackCompleteHandler;
        }

        private void AttackCompleteHandler()
        {
            _attackController.Attack(_target.Position);
        }

        public void Start(IOneUnitController target)
        {
            _stateInfo.AttackTarget = target;
            _target = target;
            _attackController.Attack(_target.Position);
        }

        public void Cancel()
        {
            _attackController.Cancel();
        }
    }
}