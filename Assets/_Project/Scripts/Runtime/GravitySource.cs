using UnityEngine;

public class GravitySource : MonoBehaviour
{
    [SerializeField] private float _gravityStrength = 1.62f;
    [SerializeField] private float _maxRange = 500f;

    public Vector3 GetGravity(Vector3 position)
    {
        Vector3 direction = (transform.position - position).normalized;
        float distance = Vector3.Distance(transform.position, position);

        if (distance > _maxRange)
            return Vector3.zero;

        return direction * _gravityStrength;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _maxRange);
    }
}