using Units.OneUnit.StatesControllers.Base.Settings;
using UnityEngine;

namespace Units.OneUnit.StatesControllers.Base.GameObject.Health
{
    public class OneUnitHealthController : Disposable, IOneUnitHealthController
    {
        private UnityEngine.GameObject _unit;
        private LookAtCameraController _lookAtCameraController;
        private HealthBarController _healthBarController;
        private UnityEngine.GameObject _healthBarGameObject;
        
        public OneUnitHealthController(IUnitSettings unitSettings, Camera camera)
        {
            _unit = unitSettings.GraphicObject;
            _lookAtCameraController = _unit.GetComponentInChildren(typeof(LookAtCameraController)) as LookAtCameraController;
            _lookAtCameraController.main_camera = camera;
            
            _healthBarController = _unit.GetComponentInChildren(typeof(HealthBarController)) as HealthBarController;
            _healthBarController.Set(1.0f);

            _healthBarGameObject = _unit.transform.Find("healthbar").gameObject;
        }

        public void SetHealthBarValue(float value)
        {
            _healthBarController.Set(value);
        }

        public void SetHealthBarVisible(bool value)
        {
            _healthBarGameObject.SetActive(value);
        }
        
        protected override void DisposeInternal()
        {
            _lookAtCameraController = null;
            _healthBarGameObject = null;
            base.DisposeInternal();
        }
    }
}