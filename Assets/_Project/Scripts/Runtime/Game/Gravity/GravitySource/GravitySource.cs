using UnityEngine;

namespace OnTheMoon.Runtime.Game
{
    public class GravitySource : MonoBehaviour, IGravitySource
    {
        [SerializeField] private float _gravityStrength = 1.62f;
        [SerializeField] private float _maxRange = 500f;

        public Vector3 GetDirection(Vector3 position) =>
            (transform.position - position).normalized;

        public Vector3 GetGravity(Vector3 position)
        {
            float distance = Vector3.Distance(transform.position, position);

            if (distance > _maxRange)
                return Vector3.zero;

            return GetDirection(position) * _gravityStrength;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _maxRange);
        }
    }
}