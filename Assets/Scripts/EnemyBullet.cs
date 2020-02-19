using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float t;
    public float maxT = 3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //destrucción de la bala al cabo de variable maxT
        t += Time.deltaTime;
        if (t > maxT)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        //destrucción de bala al cochar con diferentes capas
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Destroy(this.gameObject);
            PlayerController.instance.playerSettings.life = PlayerController.instance.playerSettings.life -2;
        }
    }
}
