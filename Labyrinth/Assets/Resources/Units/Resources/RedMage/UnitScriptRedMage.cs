using Units.OneUnit.StatesControllers.Base.GameObject.Animation;
using UnityEngine;

public class UnitScriptRedMage : MonoBehaviour, IUnitScript
{
	private Animator _animator;
	
	void Awake () {
		_animator = GetComponent<Animator>();
	}
	
	public void PlayIdleAnimation()
	{
		_animator.Play("Idle");
	}

	public void PlayWalkAnimation()
	{
		_animator.Play("Move");
	}

	public void PlayAttackAnimation()
	{
		_animator.Play("ATK1");
	}

	public void PlayDieAnimation()
	{
		_animator.Play("Dead");
	}
}
