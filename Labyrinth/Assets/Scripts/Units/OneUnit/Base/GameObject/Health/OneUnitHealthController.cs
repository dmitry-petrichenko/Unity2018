using Scripts.Units.Settings;
using UnityEngine;

namespace Units.OneUnit.Base.GameObject.Health
{
    public class OneUnitHealthController : IOneUnitHealthController
    {
        private UnityEngine.GameObject _unit;
        private LookAtCameraController _lookAtCameraController;
        
        public OneUnitHealthController(IUnitSettings unitSettings, Camera camera)
        {
            _unit = unitSettings.GraphicObject;
            _lookAtCameraController = _unit.GetComponentInChildren(typeof(LookAtCameraController)) as LookAtCameraController;
            _lookAtCameraController.main_camera = camera;
        }
    }
}