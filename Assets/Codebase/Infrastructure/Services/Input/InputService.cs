using System;
using System.Collections;
using Codebase.Infrastructure.Services.Settings;
using UnityEngine;

namespace Codebase.Infrastructure.Services.Input
{
    public class InputService : IInputService
    {
        private readonly InputSettings _inputSettings;
        private readonly WaitForSeconds _waiting;
        private Camera _camera;

        public event Action LeftMouseButtonDownEvent;
        public event Action LeftMouseButtonUpEvent;
        
        public event Action<float> MouseWheelScrollDownEvent;
        public event Action<float> MouseWheelScrollUpEvent;

        public float HorizontalSpeed => UnityEngine.Input.GetAxis("Horizontal");
        public float VerticalSpeed => UnityEngine.Input.GetAxis("Vertical");

        public Vector3 MousePositionInScreen => GetMousePosition();
        public Vector3 MousePositionInViewport => GetMousePosition(PositionSpace.Viewport);
        public Vector3 MousePositionInWorld => GetMousePosition(PositionSpace.World);

        public InputService(ICoroutineRunner coroutineRunner, GameSettings gameSettings)
        {
            _inputSettings = gameSettings.InputSettings;
            _waiting = new WaitForSeconds(_inputSettings.ObserverUpdateTime);
            coroutineRunner.StartCoroutine(InputObserveCoroutine());
        }

        private IEnumerator InputObserveCoroutine()
        {
            while (true)
            {
                if(UnityEngine.Input.GetMouseButtonDown(0)) LeftMouseButtonDownEvent?.Invoke();
                if(UnityEngine.Input.GetMouseButtonUp(0)) LeftMouseButtonUpEvent?.Invoke();
                
                if(UnityEngine.Input.mouseScrollDelta.y > 0) MouseWheelScrollUpEvent?.Invoke(
                    UnityEngine.Input.mouseScrollDelta.y);
                if(UnityEngine.Input.mouseScrollDelta.y < 0) MouseWheelScrollDownEvent?.Invoke(
                    UnityEngine.Input.mouseScrollDelta.y);
                
                yield return _waiting;
            }
        }

        private Vector3 GetMousePosition(PositionSpace positionSpace = PositionSpace.Screen)
        {
            if(_camera == null) _camera = Camera.main;
            switch (positionSpace)
            {
                case PositionSpace.World:
                    return _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                case PositionSpace.Viewport:
                    return _camera.ScreenToViewportPoint(UnityEngine.Input.mousePosition);
                case PositionSpace.Screen:
                    return UnityEngine.Input.mousePosition;
                default:
                    return UnityEngine.Input.mousePosition;
            }
        }
    }
}