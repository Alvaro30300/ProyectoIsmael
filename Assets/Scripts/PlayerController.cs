using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSettings
{
    public float life;
    public float maxLife;
    public float speed;
    public float sprint;

}
public class PlayerController : MonoBehaviour
{


    public PlayerSettings playerSettings;

    [Header("Other Settings")]
    public float climbSpeed;
    public int chips;

    [Space(10)]
    public GameObject shield;
    public Transform respawn;

    [Header("Debug")]
    [SerializeField]
    private bool isGrounded;


    public static PlayerController instance;
    void Awake()
    {
        if (!instance)
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
        isGrounded = true;
        chips = 0;
        shield.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerSettings.life <= 0)
        {
            playerSettings.life = 5;
            this.gameObject.transform.position = respawn.position;
        }
        if (playerSettings.life < 5)
        {
            playerSettings.life = playerSettings.life + 1 * Time.deltaTime;
        }
        Movimiento();
    }
    #region Triggers
    void OnTriggerEnter(Collider col)
    {
        //cambio de variable(devuelve el estado actual del player, en el suelo o en una pared) si esta en la capa de escalada
        if (col.gameObject.layer == LayerMask.NameToLayer("Escalada"))
        {
            isGrounded = false;
        }
        //controla que si el jugador tiene la llave, este sera capaz de abrir la puerta
        if (col.gameObject.layer == LayerMask.NameToLayer("Puerta") && chips > 0)
        {
            chips--;
            Destroy(col.gameObject);

        }
    }
    void OnTriggerExit(Collider col)
    {
        //vuelta al suelo si sale de la capa de escalada
        if (col.gameObject.layer == LayerMask.NameToLayer("Escalada"))
        {
            isGrounded = true;
        }
    }
    #endregion
    public void Movimiento()
    {
        //input para el movimiento del personaje
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.position += -transform.right * playerSettings.speed * Time.deltaTime;
        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.position += transform.right * playerSettings.speed * Time.deltaTime;
        }
        if (isGrounded)
        {
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                transform.position += -transform.forward * playerSettings.speed * Time.deltaTime;
            }
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                transform.position += transform.forward * playerSettings.speed * Time.deltaTime;
            }
            //Sprint
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerSettings.speed = playerSettings.speed * playerSettings.sprint;
                if (playerSettings.speed > 20)
                {
                    playerSettings.speed = 20;
                }
            }
            else
            {
                playerSettings.speed = 10;
            }
        }
        //Escalada
        else
        {
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                transform.position += Vector3.down * playerSettings.speed * Time.deltaTime;
            }
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                transform.position += Vector3.up * playerSettings.speed * Time.deltaTime;
            }
        }
    }
}
