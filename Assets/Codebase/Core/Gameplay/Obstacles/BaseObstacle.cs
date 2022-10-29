using UnityEngine;

namespace Codebase.Core.Gameplay.Obstacles
{
    [RequireComponent(typeof(Collider))]
    public abstract class BaseObstacle : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IObstacleTarget obstacleTarget))
            {
                OnInteractWithTarget(obstacleTarget);
            }
        }

        protected virtual void OnInteractWithTarget(IObstacleTarget obstacleTarget)
        {
            obstacleTarget.Interact();
        }
    }
}