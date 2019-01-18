using Scripts.Units.Settings;
using UnityEngine;

namespace Units.OneUnit.Base.GameObject.Health
{
    public class OneUnitHealthController : IOneUnitHealthController
    {
        private UnityEngine.GameObject _unit;
        private LookAtCameraController _lookAtCameraController;
        private HealthBarController _healthBarController;
        
        public OneUnitHealthController(IUnitSettings unitSettings, Camera camera)
        {
            _unit = unitSettings.GraphicObject;
            _lookAtCameraController = _unit.GetComponentInChildren(typeof(LookAtCameraController)) as LookAtCameraController;
            _lookAtCameraController.main_camera = camera;
            
            _healthBarController = _unit.GetComponentInChildren(typeof(HealthBarController)) as HealthBarController;
            _healthBarController.Set(0.7f);
        }
    }
}