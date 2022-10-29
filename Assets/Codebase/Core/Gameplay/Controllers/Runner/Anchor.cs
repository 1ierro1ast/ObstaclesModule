using Codebase.Infrastructure.GameFlow;
using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.Input;
using Codebase.Infrastructure.Services.Settings;
using UnityEngine;

namespace Codebase.Core.Gameplay.Controllers.Runner
{
    public class Anchor : MonoBehaviour
    {
        private bool _mousePressed;
        private IInputService _inputService;
        private AnchorMoveSettings _anchorMoveSettings;
        private IEventBus _eventBus;
        private bool _isMoving;
        private float _xOffset;

        private void EventBus_OnLevelFinishedEvent()
        {
            _isMoving = false;
        }

        private void EventBus_OnGamePlayStartEvent()
        {
            _isMoving = true;
        }

        private void Awake()
        {
            _anchorMoveSettings = AllServices.Container.Single<GameSettings>().AnchorMoveSettings;
            
            _inputService = AllServices.Container.Single<IInputService>();

            _inputService.LeftMouseButtonDownEvent += InputService_OnLeftMouseButtonDownEvent;
            _inputService.LeftMouseButtonUpEvent += InputService_OnLeftMouseButtonUpEvent;

            _eventBus = AllServices.Container.Single<IEventBus>();
            
            _eventBus.GamePlayStartEvent += EventBus_OnGamePlayStartEvent;
            _eventBus.LevelFinishedEvent += EventBus_OnLevelFinishedEvent;
        }

        private void Update()
        {
            if (_mousePressed && _isMoving)
            {
                MoveAnchor();
            }
        }

        private void OnDestroy()
        {
            _inputService.LeftMouseButtonDownEvent -= InputService_OnLeftMouseButtonDownEvent;
            _inputService.LeftMouseButtonUpEvent -= InputService_OnLeftMouseButtonUpEvent;

            _eventBus.GamePlayStartEvent -= EventBus_OnGamePlayStartEvent;
            _eventBus.LevelFinishedEvent -= EventBus_OnLevelFinishedEvent;
        }

        private void InputService_OnLeftMouseButtonUpEvent()
        {
            _mousePressed = false;
        }

        private void InputService_OnLeftMouseButtonDownEvent()
        {
            _mousePressed = true;
            var normalizedAnchorXPosition = transform.localPosition.x / _anchorMoveSettings.AnchorMaxDeviation;
            _xOffset = normalizedAnchorXPosition - GetPositionWithZeroCenter(_inputService.MousePositionInViewport);
        }

        private void MoveAnchor()
        {
            var mouseXPositionPercent = (GetPositionWithZeroCenter(_inputService.MousePositionInViewport) + _xOffset) *
                                        _anchorMoveSettings.AnchorMaxDeviation;
            var localPosition = transform.localPosition;
            transform.localPosition = new Vector3(mouseXPositionPercent, localPosition.y, localPosition.z);
        }

        private float GetPositionWithZeroCenter(Vector3 mousePosition) => (mousePosition.x - 0.5f) * 2;
    }
}