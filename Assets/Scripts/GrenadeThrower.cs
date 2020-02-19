using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour
{
    public float force;
    public Transform hand;
    public GameObject grenadePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //input de disparo
        if(Input.GetButtonDown("Fire1"))
        {
            Throw();
        }
    }
    void Throw()
    {
        //lanzamiento de la bomba mas la fuerza de lanzamiento
        GameObject grenade = Instantiate(grenadePrefab,hand.transform.position, hand.transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(hand.forward * force, ForceMode.Impulse);
    }
}
