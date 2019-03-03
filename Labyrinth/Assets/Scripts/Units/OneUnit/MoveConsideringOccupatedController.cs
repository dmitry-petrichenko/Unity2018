using System.Collections.Generic;
using Scripts;
using Units.OneUnit.Base;

namespace Units.OneUnit
{
    public class MoveConsideringOccupatedController : Disposable
    {
        private readonly IUnitsTable _unitsTable;
        private readonly IBaseActionController _baseActionController;
        private List<IntVector2> _occupiedPossitions;
        
        public MoveConsideringOccupatedController(
            IUnitsTable unitsTable,
            IBaseActionController baseActionController
            )
        {
            _unitsTable = unitsTable;
            _baseActionController = baseActionController;

            Initialize();
        }

        public void Initialize()
        {
            SubscribeOnEvent();            
        }

        protected override void DisposeInternal()
        {
            UnsubscribeFromEvent();
            base.DisposeInternal();
        }

        private void SubscribeOnEvent()
        {
            _baseActionController.NextTileOccupied += NextPositionOccupiedHandler;
        }
        
        private void UnsubscribeFromEvent()
        {
            _baseActionController.NextTileOccupied -= NextPositionOccupiedHandler;   
        }

        private void NextPositionOccupiedHandler(IntVector2 occupiedPosition)
        {
            _baseActionController.Cancel();
            _occupiedPossitions = _unitsTable.GetOccupiedPositions();
            RemoveCurrentUnitPosition();
            _baseActionController.MoveToAvoidingOccupiedCells(_baseActionController.Position, _occupiedPossitions);
        }

        private void RemoveCurrentUnitPosition()
        {
            List<IntVector2> copy;
            copy = CopyDictionary(_occupiedPossitions);
            _occupiedPossitions = copy;
            _occupiedPossitions.Remove(_baseActionController.Position);
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