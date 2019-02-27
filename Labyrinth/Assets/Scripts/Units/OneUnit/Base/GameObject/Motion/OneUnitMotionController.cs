using System;
using DG.Tweening;
using Scripts;
using Scripts.Extensions;
using Scripts.Units.Settings;
using UnityEngine;

namespace Units.OneUnit.Base.GameObject.Motion
{
    public class OneUnitMotionController : Disposable, IOneUnitMotionController
    {
        private readonly IUnitSettings _unitSettings;
        private UnityEngine.GameObject _unit;

        public OneUnitMotionController(IUnitSettings unitSettings)
        {
            _unitSettings = unitSettings;
            _unit = _unitSettings.GraphicObject;
        }

        public IntVector2 Position { get; private set; }
        public bool IsMoving { get; private set; }

        public void SetOnPosition(IntVector2 position)
        {
            _unit.transform.position = new Vector3(position.x, 0, position.y);
            Position = position;
            MoveStart?.Invoke();
        }

        public void MoveToPosition(IntVector2 position)
        {
            Position = position;
            IsMoving = true;
            float motionSpeed;
            
            if (IsDiagonal(Position, position))
            {
                motionSpeed = _unitSettings.MotionSpeed * 1.4f;
            }
            else
            {
                motionSpeed = _unitSettings.MotionSpeed;
            }
            
            _unit.transform.DOMove(new Vector3(position.x, 0, position.y), motionSpeed)
                .OnComplete(CompleteMoveHandler)
                .SetEase(Ease.Linear);
            
            MoveStart?.Invoke();
        }

        private bool IsDiagonal(IntVector2 position1, IntVector2 position2)
        {
            if (position1.x == position2.x || position1.y == position2.y)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void CompleteMoveHandler()
        {
            IsMoving = false;
            MoveComplete?.Invoke();
        }

        public void Wait()
        {
        }

        public event Action MoveComplete;
        public event Action MoveStart;

        protected override void DisposeInternal()
        {
            Position = IntVector2Constant.UNASSIGNET;
            MoveComplete = null;
            MoveStart = null;
            base.DisposeInternal();
        }
    }
}