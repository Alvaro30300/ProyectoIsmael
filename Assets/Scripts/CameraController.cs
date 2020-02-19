using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //cambios de rotacion y velocidad
    public float rotationSpeed;
    float mouseX;
    float mouseY;

    //transform a modificar con las variables del script
    public Transform target;
    public Transform player;

    public static CameraController instance;
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
        //ocultar y bloquear el raton en inicio de partida
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        CameraControl();
    }
    public void CameraControl()
    {
        //input de la camara, con limitaciones en el eje Y
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        //seguimiento en movimiento y rotacion al target(player)
        transform.LookAt(target);

        target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        player.rotation = Quaternion.Euler(0, mouseX, 0);
    }
}
