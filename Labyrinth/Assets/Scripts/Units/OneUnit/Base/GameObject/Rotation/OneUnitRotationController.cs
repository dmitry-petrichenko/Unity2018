using System;
using DG.Tweening;
using Scripts;
using Scripts.Extensions;
using Scripts.Units.Settings;
using UnityEngine;

namespace Units.OneUnit.Base.GameObject.Rotation
{
    public class OneUnitRotationController : MyDisposable, IOneUnitRotationController
    {
        private UnityEngine.GameObject _unit;

        public OneUnitRotationController(IUnitSettings unitSettings)
        {
            _unit = unitSettings.GraphicObject.transform.Find("Unit").gameObject;
        }
        
        public double GetRotation(IntVector2 point1, IntVector2 point2)
        {
            double radians = Math.Atan2(point2.x - point1.x, point2.y - point1.y);
            double angle = radians / Math.PI * 180;
            return angle;
        }

        public void Rotate(IntVector2 point1, IntVector2 point2)
        {
            double angle = GetRotation(point1, point2);
            _unit.transform.DOLocalRotate(new Vector3(0f, (float) angle, 0f), 0.3f);
        }

        public void Dispose()
        {
            _unit = null;
        }
    }
}