namespace Units.OneUnit.StatesControllers.Base.GameObject.Animation
{
    public interface IUnitScript
    {
        void PlayIdleAnimation();
        void PlayWalkAnimation();
        void PlayAttackAnimation();
        void PlayDieAnimation();
    }
}