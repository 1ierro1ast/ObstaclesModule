using System.Collections;
using UnityEngine;

namespace Codebase.Core.Gameplay.Obstacles
{
    public class ObstacleMover : MonoBehaviour
    {
        [SerializeField] private float _moveDuration;
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private float _minBorder;
        [SerializeField] private float _maxBorder;
        [SerializeField] private MovementAxis _axis;

        private Vector3 _minPosition;
        private Vector3 _maxPosition;

        private Vector3[] _directions = new Vector3[]
        {
            Vector3.right,
            Vector3.up,
            Vector3.forward,
        };

        private void Awake()
        {
            var direction = _directions[(int)_axis];
            _minPosition = direction * _minBorder;
            _maxPosition = direction * _maxBorder;

            var localPosition = transform.localPosition;

            _minPosition.y = localPosition.y;
            _minPosition.z = localPosition.z;
            _maxPosition.y = localPosition.y;
            _maxPosition.z = localPosition.z;

            StartCoroutine(MoveTo());
        }

        private IEnumerator MoveTo(bool isRight = false)
        {
            var target = isRight ? _maxPosition : _minPosition;
            float time = 0f;
            while (time < _moveDuration)
            {
                time += Time.deltaTime;
                transform.localPosition = Vector3.Lerp(
                    transform.localPosition,
                    target,
                    _curve.Evaluate(time / _moveDuration));

                yield return null;
            }

            transform.localPosition = target;

            StartCoroutine(MoveTo(!isRight));
        }
    }
}