using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
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
    }

    // Update is called once per frame
    void Update()
    {
        //cuenta atras de la explosion de la bomba(una vez disparado)
        countDown -= Time.deltaTime;
        if (countDown < -0)
        {
            Explosion();
            exploded = true;
        }
    }
    void Explosion()
    {
        //arraty de objetos que chocan en la explosion
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        //eliminar todos los componentes con rigidbody que estén dentro de dicho array
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
                Destroy(nearbyObject.gameObject);
            }

        }
        //destruccion de la bomba
        Destroy(this.gameObject);
    }
}
