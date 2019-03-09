using System.Collections.Generic;
using Scripts;

namespace Units
{
    public class OccupatedPossitionsTable : Disposable, IOccupatedPossitionsTable
    {
        private List<IntVector2> _indexes = new List<IntVector2>();

        public void SetOccupied(IntVector2 index)
        {
            _indexes.Add(index);
        }
        
        public void SetVacant(IntVector2 index)
        {
            _indexes.Remove(index);
        }

        public bool IsVacantPosition(IntVector2 index)
        {
            return !_indexes.Contains(index);
        }
        
        public List<IntVector2> GetOccupiedPositions()
        {
            return _indexes;
        }

        protected override void DisposeInternal()
        {
            _indexes.Clear();
            _indexes = null;
            base.DisposeInternal();
        }
    }
}