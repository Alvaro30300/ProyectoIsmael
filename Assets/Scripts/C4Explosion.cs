using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4Explosion : MonoBehaviour
{
    [Header("ExplosionSettings")]
    public float radius;
    public float force;

    public static C4Explosion instance;
    private void Awake()
    {
        if(!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //input de disparo
        if(Input.GetButtonDown("Fire2"))
        {
            Boom();
        }
    }
    public void Boom()
    {
        //creacion de array para que la explosion detecte con todo lo que ha chocado dentro de su radio
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        //destrucción de todos los objetos con el componente "rigidbody" dentro del array anterior
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
                Destroy(nearbyObject.gameObject);
            }
        }
        //eliminacion de bomba
        Destroy(this.gameObject);
    }
}
