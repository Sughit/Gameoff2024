using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject explosion;
    [SerializeField] bool isPickedUp = false;
    [SerializeField] float timeToExplode = 2f;
    [SerializeField] float throwForce = 10f;
    GameObject player;
    Rigidbody rb;

    public void Interact()
    {
        //Verificam daca playerul mai are vreo arma in mana

        if(!isPickedUp)
        {
            isPickedUp = true;
            transform.SetParent(player.transform.GetChild(0).transform);
            rb.velocity = Vector3.zero;
            transform.localPosition = Vector3.zero;
        }
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(!isPickedUp) return;
        else
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                transform.parent = null;
                rb.AddForce(player.transform.forward * throwForce, ForceMode.VelocityChange);
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
