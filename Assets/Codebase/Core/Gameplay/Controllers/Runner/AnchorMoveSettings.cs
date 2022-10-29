using System;
using UnityEngine;

namespace Codebase.Core.Gameplay.Controllers.Runner
{
    [Serializable]
    public class AnchorMoveSettings
    {
        [SerializeField] private float _forwardSpeed = 5;
        [SerializeField] private float _strafeSpeed = 10;
        [SerializeField] private float _anchorMaxDeviation = 1.5f;
        [Space]
        [SerializeField] private LayerMask _groundLayerMask;
        [SerializeField] private Vector3 _offsetFromGround;
        [SerializeField] private float _raycastDistance;


        public float ForwardSpeed => _forwardSpeed;
        public float StrafeSpeed => _strafeSpeed;
        public float AnchorMaxDeviation => _anchorMaxDeviation;
        public LayerMask GroundLayerMask => _groundLayerMask;
        public Vector3 OffsetFromGround => _offsetFromGround;
        public float RaycastDistance => _raycastDistance;
    }
}