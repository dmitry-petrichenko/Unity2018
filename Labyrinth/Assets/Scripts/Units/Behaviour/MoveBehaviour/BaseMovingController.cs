﻿using System.Collections.Generic;

namespace Scripts.Units
{
    public class BaseMovingController : IBaseMovingController
    {
        private readonly ChangeDirrectionAfterMoveTileCompleteController _changeDirrectionAfterMoveTileCompleteController;
        private readonly ISubMoveController _subMoveController;

        public BaseMovingController(
            ChangeDirrectionAfterMoveTileCompleteController changeDirrectionAfterMoveTileCompleteController,
            ISubMoveController subMoveController)
        {
            _subMoveController = subMoveController;
            _changeDirrectionAfterMoveTileCompleteController = changeDirrectionAfterMoveTileCompleteController;
        }

        public void Attack(IntVector2 position) => _subMoveController.Attack(position);

        public void MoveTo(IntVector2 position) => _changeDirrectionAfterMoveTileCompleteController.MoveTo(position);

        public void MoveTo(List<IntVector2> path) => _subMoveController.MoveTo(path);
        
        public void Wait() => _subMoveController.Wait();
        
        public void Wait(IntVector2 position) => _subMoveController.Wait(position);
        
        public void Cancel() => _subMoveController.Cancel();
        
        public void SetOnPosition(IntVector2 position) => _subMoveController.SetOnPosition(position);

        public IntVector2 Position => _subMoveController.Position;
        public IntVector2 Destination => _subMoveController.Destination;
        public bool IsMoving => _subMoveController.IsMoving;
    }
}