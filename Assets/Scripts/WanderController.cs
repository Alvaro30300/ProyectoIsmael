using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderController : MonoBehaviour
{
    [Header("EnemySettings")]
    public int life = 2;
    public float speed;
    public float chaseSpeed;
    private float walkTime = 5;
    private float  walkWait = 0.5f;

    [Header("OtherSettings")]
    public float shootDistance;
    public float attackRate;
    public float startAttackRate;
    public float shotForce;

    [Header("WanderSettings")]
    public float rotSpeed;
    private float rotTime = 1f;
    private float rotWait = 0.5f;
    public bool canWonder = true;
    private bool isWandering = false;
    private bool isRotLeft = false;
    private bool isRotRight = false;
    private bool isWalking = false;

    [Header("Debug")]
    public bool checkPointed;
    private bool rotateRight = true;

    [Header("Respawn")]
    public Vector3 respawn;
    public Vector3 checkPointRespawn;

    [Header("PlayerDetection")]
    public Transform player;
    public Transform target;

    public GameObject shootingPrefab;

    FieldOfView fieldOfView;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.position = respawn;
        fieldOfView = GetComponent<FieldOfView>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vida
        if(life <= 0)
        {
            this.gameObject.SetActive(false);
        }
        #region Movimiento
        //control de variables para manejar los diferentes comportamientos
        if (!fieldOfView.isInView)
        {
            //dentro del circulo, pero fuera del rango de vision del enemigo, continua la patrulla
            if (!isWandering)
            {
                StartCoroutine(Wander());
            }

            //movimiento durante la patrulla
            if (isRotLeft)
            {
                transform.Rotate(-transform.up * Time.deltaTime * rotSpeed);
            }
            if (isRotRight)
            {
                transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
            }
            if (isWalking)
            {
                transform.position += transform.forward * Time.deltaTime * speed;
            }
        }
        else if(fieldOfView.isInView)
        //control de movimiento una vez el jugador entra en el rango de vision del enemigo, comienza la persecucion
        {
            if (Vector3.Distance(transform.position, target.position) > shootDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, chaseSpeed * Time.deltaTime);
                transform.rotation = Quaternion.LookRotation((target.position - transform.position) * Time.deltaTime);
            }
            else
            {
                if(attackRate<= 0)
                {
                    GameObject shoot = Instantiate(shootingPrefab,transform.position, transform.rotation);
                    Rigidbody rb = shoot.GetComponent<Rigidbody>();
                    rb.AddForce(transform.forward * shotForce, ForceMode.Impulse);
                    attackRate = startAttackRate;
                }
                else
                {
                    attackRate -= Time.deltaTime;
                }

            }

        }
        #endregion

    }
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
        {
            life--;
        }
    }
    //corrutina que controla el movimiento del enemigos a la hora de patrullar, si el jugador entra en el rango de vision de este, salida de esta corrutina
    IEnumerator Wander()
    {

        isWandering = true;
        yield return new WaitForSeconds(walkWait);

        isWalking = true;
        yield return new WaitForSeconds(walkTime);

        isWalking = false;
        yield return new WaitForSeconds(rotWait);

        if(rotateRight)
        {
            isRotRight = true;
            yield return new WaitForSeconds(rotTime);
            isRotRight = false;
        }
        if (!rotateRight)
        {
            isRotLeft = true;
            yield return new WaitForSeconds(rotTime);
            isRotLeft = false ;
        }
        isWandering = false;
    }
}
