using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float fov = 30f, viewDistance = 10f;
    Transform playerTransfrom;
    public enum EnemyState
    {
        idle,
        patroling, 
        following,
        attacking
    };
    EnemyState enemyState;
    NavMeshAgent nav;

    public float attackDis = 1f, attackTime = .5f;
    float attackTimeCurr;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        attackTimeCurr = attackTime;
    }

    void Update()
    {
        if(playerTransfrom == null) playerTransfrom = GameManager.instance.playerTransform;
        else TrySeePlayer();

        //if(CheckIfDestinationReached() && enemyState != EnemyState.attacking) enemyState = EnemyState.idle;
        
        switch(enemyState)
        {
            case EnemyState.idle:
                //Invoke("Patrol", 3f);
            break;
            case EnemyState.patroling:  

            break;  
            case EnemyState.following:
                if(Vector3.Distance(playerTransfrom.position, transform.position) <= attackDis) enemyState = EnemyState.attacking;
            break;
            case EnemyState.attacking:
                Debug.Log("Suntem aici");
                nav.Stop();
                Attack();
                if(Vector3.Distance(playerTransfrom.position, transform.position) > attackDis) enemyState = EnemyState.following;
            break;
        }
    }

    void Attack()
    {
        Debug.Log("Chemam atac");
        if(attackTimeCurr <= 0)
        {
            AttackPlayer();
            attackTimeCurr = attackTime;
        }
        else attackTimeCurr -= Time.deltaTime;
    }

    public virtual void AttackPlayer()
    {
        Debug.Log("Attack");
    }

    void TrySeePlayer()
    {
        if(Vector3.Distance(transform.position, playerTransfrom.position) < viewDistance)
        {
            Debug.Log("E aproape");
            Vector3 dirToPlayer = (playerTransfrom.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, dirToPlayer) < fov / 2f)
            {
                Debug.Log("E in unghi");
                if(Physics.Raycast(transform.position, dirToPlayer, out RaycastHit hit, viewDistance))
                {
                    Debug.Log(hit.collider.gameObject.name);
                    if(hit.collider.gameObject.tag == "Player")
                    {
                        enemyState = EnemyState.following;
                        nav.destination = playerTransfrom.position;
                    }
                }
            }
        }
    }

    public void CheckPlace(Vector3 pos)
    {
        nav.destination = pos;
        enemyState = EnemyState.following;
    }

    bool CheckIfDestinationReached()
    {
        if (!nav.pathPending)
        {
            if (nav.remainingDistance <= nav.stoppingDistance)
            {
                if (!nav.hasPath || nav.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void OnDrawGizmosSelected()
    {
        //Ceea ce vede
        Quaternion leftRayRotation = Quaternion.AngleAxis(-(fov / 2f), Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(fov / 2f, Vector3.up);
        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;
        Gizmos.DrawRay(transform.position, leftRayDirection * viewDistance);
        Gizmos.DrawRay(transform.position, rightRayDirection * viewDistance);

        //Distanta de attack
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDis);
    }
}
