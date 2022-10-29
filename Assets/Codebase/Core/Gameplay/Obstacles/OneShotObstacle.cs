namespace Codebase.Core.Gameplay.Obstacles
{
    public class OneShotObstacle : BaseObstacle
    {
        protected override void OnInteractWithTarget(IObstacleTarget obstacleTarget)
        {
            base.OnInteractWithTarget(obstacleTarget);
            Destroy(gameObject);
        }
    }
}