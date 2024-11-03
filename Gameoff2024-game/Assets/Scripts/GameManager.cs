using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject playerGO;
    public Transform playerTransform;

    void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(this);

        playerGO = GameObject.FindWithTag("Player");
        playerTransform = playerGO.transform;
    }
}
