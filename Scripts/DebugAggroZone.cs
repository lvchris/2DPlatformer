using UnityEngine;

public class DebugAggroZone : MonoBehaviour
{
    public float range = 10f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, range);
    }
}
