using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] private float hearingRange;
    private PlayerMovement movement;
    [SerializeField] private float walkingRange = 2f, runningRange = 4f, crouchingRange = 1f;
    [SerializeField] LayerMask enemyLayer;
    Collider[] enemies;
    Vector3 prevPos;

    void Update()
    {
        if(prevPos != transform.position)
        {
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            bool isCrouching = Input.GetKey(KeyCode.LeftControl);
            bool isWalking = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
            hearingRange = isRunning ? hearingRange = runningRange : isCrouching ? hearingRange = crouchingRange : isWalking ? hearingRange = walkingRange : hearingRange = 0;
        }
        else 
        {
            hearingRange = 0;
        }
        prevPos = transform.position;

        enemies = Physics.OverlapSphere(transform.position, hearingRange, enemyLayer);
        if(enemies.Length > 0)
        {
            foreach(Collider enemy in enemies)
            {
                enemy.gameObject.TryGetComponent<EnemyScript>(out EnemyScript enemyScript);
                if(enemyScript != null) enemyScript.CheckPlace(transform.position);
            }
        }
    }    

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, hearingRange);
    }
}
