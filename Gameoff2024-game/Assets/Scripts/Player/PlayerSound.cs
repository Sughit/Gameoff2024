using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] private float hearingRange;
    private PlayerMovement movement;
    [SerializeField] private float walkingRange = 2f, runningRange = 4f, crouchingRange = 1f;

    void Update()
    {
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        bool isCrouching = Input.GetKey(KeyCode.LeftControl);
        bool isWalking = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        hearingRange = isRunning ? hearingRange = runningRange : isCrouching ? hearingRange = crouchingRange : isWalking ? hearingRange = walkingRange : hearingRange = 0;
    }    

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, hearingRange);
    }
}
