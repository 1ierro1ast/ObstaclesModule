using Codebase.Infrastructure.GameFlow;
using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.Settings;
using UnityEngine;

namespace Codebase.Core.Gameplay.Controllers.Runner
{
    public class ForwardMover : MonoBehaviour
    {
        private AnchorMoveSettings _anchorMoveSettings;
        private IEventBus _eventBus;
        private bool _isMoving;
        
        private void Awake()
        {
            _anchorMoveSettings = AllServices.Container.Single<GameSettings>().AnchorMoveSettings;
            _eventBus = AllServices.Container.Single<IEventBus>();

            _eventBus.GamePlayStartEvent += EventBus_OnGamePlayStartEvent;
            _eventBus.LevelFinishedEvent += EventBus_OnLevelFinishedEvent;
        }

        private void Update()
        {
            if (!_isMoving) return;
            var forwardSpeed = Vector3.forward * (_anchorMoveSettings.ForwardSpeed * Time.deltaTime);
            transform.Translate(forwardSpeed);
        }

        private void OnDestroy()
        {
            _eventBus.GamePlayStartEvent -= EventBus_OnGamePlayStartEvent;
            _eventBus.LevelFinishedEvent -= EventBus_OnLevelFinishedEvent;
        }

        private void EventBus_OnLevelFinishedEvent()
        {
            _isMoving = false;
        }

        private void EventBus_OnGamePlayStartEvent()
        {
            _isMoving = true;
        }
    }
}
