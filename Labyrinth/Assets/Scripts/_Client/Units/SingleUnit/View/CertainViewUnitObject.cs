using Scripts._Client.Units.SingleUnit.Api;
using Scripts._Client.Units.SingleUnit.Resolver;
using Units.OneUnit.StatesControllers.Base.GameObject.Animation;
using UnityEngine;
using _Dto.Unit;

namespace Scripts._Client.Units.SingleUnit
{
    public interface ICertainViewUnitObject :
        IAnimationApi,
        IHealthBarApi,
        IMotionApi,
        IRotationApi
    {}
    
    public class CertainViewUnitObject : ICertainViewUnitObject
    {
        private IUnityUnitViewObject _unityUnitViewObject;
        
        public CertainViewUnitObject(IUnitDto unitDto, IUnitViewObjectResolver unitViewObjectResolver)
        {
            _unityUnitViewObject = unitViewObjectResolver.ResolveUnitViewObject(unitDto);
        }

        public void SetOnPosition(FloatVector2 position)
        {
            _unityUnitViewObject.GameObject.transform.position = new Vector3(position.X, 0, position.Y);
        }

        public void SetRotation(float rotation)
        {
            var u = _unityUnitViewObject.GameObject.transform.Find("Unit").gameObject;
            u.transform.Rotate(0f, rotation, 0);
        }

        public IUnitScript GetAnimationScript()
        {
            return _unityUnitViewObject.GameObject.GetComponentInChildren(typeof(IUnitScript)) as IUnitScript;
        }
    }
}