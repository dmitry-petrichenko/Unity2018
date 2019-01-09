using System;

namespace Scripts.Units
{
    public class OneUnitController : OneUnitServicesContainer, IOneUnitController
    {
        public event Action<IntVector2> PositionChanged;
        public event Action MoveToComplete;
        public event Action MoveOneStepStart;
        public event Action MoveOneStepComplete;

        private MoveController _moveController;
        private IUnitsTable _unitsTable;

        public OneUnitController(IOneUnitServices services) : base(services)
        {
            _unitsTable = services.UnitsTable;
            _moveController = services.MoveController;
        }

        protected void Initialize()
        {
            base.Initialize();            
            // Initialize behaviour
            _moveController.Initialize(this);
            _moveController.MoveOneStepStart += StartMoveHandler;
            _moveController.MoveToComplete += MoveCompleteHandler;
            _moveController.MoveOneStepComplete += MoveOneStepCompleteHandler;
            _unitsTable.AddUnit(this);
        }

        private void MoveOneStepCompleteHandler()
        {
            if (MoveOneStepComplete != null)
            {
                MoveOneStepComplete();
            }
        }

        private void StartMoveHandler()
        {
            UpdatePosition();
            if (MoveOneStepStart != null)
                MoveOneStepStart();
        }

        protected virtual void UpdatePosition()
        {
            if (PositionChanged != null)
                PositionChanged(Position);
        }

        private void MoveCompleteHandler()
        {
            if (MoveToComplete != null)
            {
                MoveToComplete();
            }
        }

        public void SetOnPosition(IntVector2 position)
        {
            _moveController.SetOnPosition(position);
        }

        public IntVector2 Position
        {
            get { return MotionController.Position; }
        }

        public void MoveTo(IntVector2 position)
        {
            _moveController.MoveTo(position);
        }

        public void Wait()
        {
            AnimationController.PlayIdleAnimation();
        }
        
        public void Wait(IntVector2 position)
        {
            RotationController.Rotate(Position, position);
            AnimationController.PlayIdleAnimation();
        }
    }
}