using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//https://github.com/Xemicolon1/Unity-Random-AI-movement/blob/main/Scripts/GetPoint.cs
public class get_point_ghost : MonoBehaviour
{
    public static get_point_ghost Instance;

    public float Range;

    private void Awake()
    {
        Instance = this;
    }

    bool random_point(Vector3 center, float range, out Vector3 result)
    {

        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }

        result = Vector3.zero;

        return false;
    }

    public Vector3 get_random_point_ghost(Transform point = null, float radius = 0)
    {
        Vector3 _point;

        if (random_point(point == null ? transform.position : point.position, radius == 0 ? Range : radius, out _point))
        {
            Debug.DrawRay(_point, Vector3.up, Color.black, 1);

            return _point;
        }

        return point == null ? Vector3.zero : point.position;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos ()
    {
        Gizmos.DrawWireSphere (transform.position, Range);
    }

#endif
}
