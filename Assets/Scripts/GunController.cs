using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public float speed = 20;
    public float range = 100;
    public int ammo = 30;
    public int currentAmmo;

    public Transform weapon;

    public GameObject bulletPrefab;

    public static GunController instance;

    void Awake()
    {
        if(!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //municion al maximo al empezar
        currentAmmo = ammo;
    }

    // Update is called once per frame
    void Update()
    {
        //input
        if(currentAmmo >0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }

    }
    void Shoot()
    {
        //creacion y suma de fuerzas de la bala tras haber disparado
        GameObject bullet = Instantiate(bulletPrefab, weapon.position, weapon.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(weapon.forward * speed, ForceMode.Impulse);
        //resta de municion actual
        currentAmmo--;
    }
}
