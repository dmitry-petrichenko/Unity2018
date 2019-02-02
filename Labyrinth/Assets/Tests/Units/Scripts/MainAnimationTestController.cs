using Tests.Map.Scripts;
using Tests.Units.Scripts;
using Units.OneUnit.Base.GameObject.Animation;
using UnityEngine;

namespace Tests.Animation
{
	public class MainAnimationTestController : MonoBehaviour
	{
		public GameObject UnitPrefab;
		private OneUnitAnimationController unitAnimationController;
		private GameLoopController _gameLoopController;
		
		// Use this for initialization
		void Start ()
		{
			_gameLoopController = new GameLoopController(this);
			var settings = new TestUnitSettings();
			settings.SetGraphicObject(UnitPrefab);
			unitAnimationController = new OneUnitAnimationController(settings, _gameLoopController);
		}

		public void PlayIdleAnimation()
		{
			unitAnimationController.PlayIdleAnimation();
		}
        
		public void PlayAttackAnimation()
		{
			unitAnimationController.PlayAttackAnimation();
		}
        
		public void PlayWalkAnimation()
		{
			unitAnimationController.PlayWalkAnimation();
		}

	}
}