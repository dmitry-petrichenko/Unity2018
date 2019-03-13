using System.Collections.Generic;
using Scripts;
using Units.OccupatedMap;
using Units.OneUnit.StatesControllers.Base.GameObject;

namespace Units.OneUnit.StatesControllers.Base
{
    public class MoveConsideringOccupatedController : Disposable
    {
        private readonly IOccupatedPossitionsMap _occupatedPossitionsMap;
        private readonly IMoveStepByStepController _moveStepByStepController;
        private readonly IPathGeneratorController _pathGeneratorController;
        private readonly IUnitGameObjectController _unitGameObjectController;
        
        private List<IntVector2> _occupiedPossitions;
        
        public MoveConsideringOccupatedController(
            IOccupatedPossitionsMap occupatedPossitionsMap,
            IUnitGameObjectController unitGameObjectController,
            IPathGeneratorController pathGeneratorController,
            IMoveStepByStepController moveStepByStepController
            )
        {
            _occupatedPossitionsMap = occupatedPossitionsMap;
            _moveStepByStepController = moveStepByStepController;
            _unitGameObjectController = unitGameObjectController;
            _pathGeneratorController = pathGeneratorController;

            SubscribeOnEvent();
        }

        protected override void DisposeInternal()
        {
            UnsubscribeFromEvent();
            base.DisposeInternal();
        }

        private void SubscribeOnEvent()
        {
            _moveStepByStepController.NextTileOccupied += NextPositionOccupiedHandler;
        }
        
        private void UnsubscribeFromEvent()
        {
            _moveStepByStepController.NextTileOccupied -= NextPositionOccupiedHandler;   
        }

        private void NextPositionOccupiedHandler(IntVector2 occupiedPosition)
        {
            _moveStepByStepController.Cancel();
            _occupiedPossitions = _occupatedPossitionsMap.GetOccupiedPositions();
            RemoveCurrentUnitPosition();
            _pathGeneratorController.MoveToPositionAvoidingOccupiedCells(_pathGeneratorController.Destination, _occupiedPossitions);
        }

        private void RemoveCurrentUnitPosition()
        {
            List<IntVector2> copy;
            copy = CopyDictionary(_occupiedPossitions);
            _occupiedPossitions = copy;
            _occupiedPossitions.Remove(_unitGameObjectController.Position);
        }
        
        private List<IntVector2> CopyDictionary(List<IntVector2> value)
        {
            List<IntVector2> copy = new List<IntVector2>();
            foreach (var index in value)
            {
                copy.Add(index);
            }

            return copy;
        }
    }
}