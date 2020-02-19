using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4Controller : MonoBehaviour
{

    public Transform hand;
    public GameObject C4Prefab;

    public float force;

    public bool bombOn = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //unput de disparo
        if (Input.GetButtonDown("Fire1"))
        {
            PutC4();
        }
    }
    void PutC4()
    {
        //creacion y posterior suma de fuerzas para colocar el explosivo
        GameObject C4 = Instantiate(C4Prefab, hand.transform.position, hand.transform.rotation);
        Rigidbody rb = C4.GetComponent<Rigidbody>();
        rb.AddForce(hand.position * force, ForceMode.Impulse);
        //booleano que controla si el c4 está o no activo, de manera que cambia respuesta al input(colocando un c4, o explotandolo)
        bombOn = true;
    }
}

