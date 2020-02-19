using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunGrenade : MonoBehaviour
{
    WanderController wanderController;
    [Header("ExplosionSettings")]
    public float radius = 5f;
    public float force = 5f;
    public float delay = 3f;
    float countDown;

    public bool exploded = false;
    // Start is called before the first frame update
    void Start()
    {
        countDown = delay;
        wanderController = GetComponent<WanderController>();
    }

    // Update is called once per frame
    void Update()
    {
        //cuenta atras de la explosion, tras el disparo
        countDown -= Time.deltaTime;
        if (countDown < -0)
        {
            Explosion();
            exploded = true;
        }
    }
    void Explosion()
    {
        //creacion de array para meter todos los objetos con los que choque la explosion dentro de este
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        //suma de fuerzas a los objetos con rigidbody dentro del array
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }
        //destruccion de la granada
        Destroy(this.gameObject);
        //stun a los enemigos
        wanderController.speed = 0;
    }
}
