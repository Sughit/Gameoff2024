using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float fov = 30f, viewDistance = 10f;
    Transform playerTransfrom;

    void Update()
    {
        if(playerTransfrom == null) playerTransfrom = GameManager.instance.playerTransform;
        else TrySeePlayer();
    }

    void TrySeePlayer()
    {
        if(Vector3.Distance(transform.position, playerTransfrom.position) < viewDistance)
        {
            Vector3 dirToPlayer = (playerTransfrom.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, dirToPlayer) < fov / 2f)
            {
                if(Physics.Raycast(transform.position, dirToPlayer, out RaycastHit hit, viewDistance))
                {
                    if(hit.collider.gameObject.tag == "Player")
                    {
                        Debug.Log("Vedem player");
                    }
                }
            }
        }
    }

    public void CheckPlace(Vector3 pos)
    {
        Debug.Log(pos);
    }

    void OnDrawGizmosSelected()
    {
        Quaternion leftRayRotation = Quaternion.AngleAxis(-(fov / 2f), Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(fov / 2f, Vector3.up);
        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;
        Gizmos.DrawRay(transform.position, leftRayDirection * viewDistance);
        Gizmos.DrawRay(transform.position, rightRayDirection * viewDistance);
    }
}
