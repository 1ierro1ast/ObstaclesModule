using System;
using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.Settings;
using UnityEngine;

namespace Codebase.Core.Gameplay.Controllers.Runner
{
    public class ToAnchorMover : MonoBehaviour
    {
        private AnchorMoveSettings _anchorMoveSettings;
        private Anchor _anchor;
        private Vector3 _anchorPositionExcludeY;
        private const string Tag = "[ToAnchorMover]";

        private void Awake()
        {
            _anchorMoveSettings = AllServices.Container.Single<GameSettings>().AnchorMoveSettings;
            _anchor = FindObjectOfType<Anchor>();
            
            var anchorTransformPosition = _anchor.transform.position;
            _anchorPositionExcludeY = new Vector3(
                anchorTransformPosition.x, 
                transform.position.y, 
                anchorTransformPosition.z);
        }

        private void LateUpdate()
        {
            if (_anchor == null)
            {
                throw new Exception($"{Tag}: Anchor not assigned!");
            }

            var anchorTransformPosition = _anchor.transform.position;

            transform.position = Vector3.Lerp(transform.position, anchorTransformPosition,
                _anchorMoveSettings.StrafeSpeed * Time.deltaTime);
        }
    }
}