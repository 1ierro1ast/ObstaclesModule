using System;
using Codebase.Infrastructure.GameFlow;
using Codebase.Infrastructure.Services;
using UnityEngine;

namespace Codebase.Core.Gameplay.Obstacles
{
    public class ObstacleTarget : MonoBehaviour, IObstacleTarget
    {
        private IEventBus _eventBus;

        private void Awake()
        {
            _eventBus = AllServices.Container.Single<IEventBus>();
        }

        public void Interact()
        {
            _eventBus.BroadcastPlayerLose();
        }

        public void Interact(float damage)
        {
            
        }
    }
}