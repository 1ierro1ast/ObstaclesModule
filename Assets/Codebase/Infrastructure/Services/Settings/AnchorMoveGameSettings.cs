using Codebase.Core.Gameplay.Controllers;
using Codebase.Core.Gameplay.Controllers.Runner;
using UnityEngine;

namespace Codebase.Infrastructure.Services.Settings
{
    public partial class GameSettings
    {
        [SerializeField] private AnchorMoveSettings _anchorMoveSettings;

        public AnchorMoveSettings AnchorMoveSettings => _anchorMoveSettings;
    }
}