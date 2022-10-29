using Codebase.Infrastructure.Services.Input;
using UnityEngine;

namespace Codebase.Infrastructure.Services.Settings
{
    public partial class GameSettings : ScriptableObject, IService
    {
        [SerializeField] private InputSettings _inputSettings;
        public InputSettings InputSettings => _inputSettings;
    }
}