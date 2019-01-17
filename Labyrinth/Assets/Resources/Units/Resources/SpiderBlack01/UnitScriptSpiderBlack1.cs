using Units.OneUnit.Base.GameObject.Animation;
using UnityEngine;

public class UnitScriptSpiderBlack1 : MonoBehaviour, IUnitScript
{
	private Animator _animator;
	
	void Awake () {
		_animator = GetComponent<Animator>();
	}
	
	public void PlayIdleAnimation()
	{
		_animator.Play("idle");
	}

	public void PlayWalkAnimation()
	{
		_animator.Play("walk");
	}

	public void PlayAttackAnimation()
	{
		_animator.Play("attack");
	}
}