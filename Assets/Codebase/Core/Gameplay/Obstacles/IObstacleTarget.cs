namespace Codebase.Core.Gameplay.Obstacles
{
    public interface IObstacleTarget
    {
        void Interact();
        void Interact(float damage);
    }
}