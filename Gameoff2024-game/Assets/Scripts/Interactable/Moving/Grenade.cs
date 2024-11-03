using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject explosion;
    [SerializeField] bool isPickedUp = false;
    [SerializeField] float timeToExplode = 2f;
    [SerializeField] float throwForce = 10f;
    Transform playerTransform;
    Rigidbody rb;

    public void Interact()
    {
        //Verificam daca playerul mai are vreo arma in mana

        if(!isPickedUp)
        {
            isPickedUp = true;
            transform.SetParent(playerTransform.GetChild(0).transform);
            rb.velocity = Vector3.zero;
            transform.localPosition = Vector3.zero;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerTransform = GameManager.instance.playerTransform;
    }

    void Update()
    {
        if(!isPickedUp) return;
        else
        {
            if(Input.GetKey(KeyCode.E))
            {
                transform.parent = null;
                rb.AddForce(playerTransform.forward * throwForce, ForceMode.VelocityChange);
                Invoke("Explode", timeToExplode);
            }
        }
    }

    void Explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
