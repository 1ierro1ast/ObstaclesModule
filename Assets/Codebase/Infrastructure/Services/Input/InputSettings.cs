using System;
using UnityEngine;

namespace Codebase.Infrastructure.Services.Input
{
    [Serializable]
    public class InputSettings
    {
        [SerializeField] private float _observerUpdateTime = 0.0002f;
        public float ObserverUpdateTime => _observerUpdateTime;
    }
}