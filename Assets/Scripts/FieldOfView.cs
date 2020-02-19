using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    //asignacion del jugador en el script
    public Transform player;
    //variables de manejo de tamaño de rango de vision
    public float maxAngle;
    public float maxRadius;

    //debug
    public bool isInView = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isInView = inView(transform, player, maxAngle, maxRadius);
    }
    #region Deteccion de player
    public static bool inView(Transform checkingObj, Transform target, float maxAngle, float maxRadius)
    {
        //creacion de array para colocar todos los objetos que entren dentro del radio del rango de vision
        Collider[] overlaps = new Collider[10];
        int count = Physics.OverlapSphereNonAlloc(checkingObj.position, maxRadius, overlaps);

        //operaciones que controlan si el jugador esta dentro de su rango de vision 
        for (int i = 0; i < count; i++)
        {
            if (overlaps[i] != null)
            {
                if (overlaps[i].transform == target)
                {
                    //direccion entre el enemigo y el jugador
                    Vector3 directionBetween = (target.position - checkingObj.position).normalized;
                    directionBetween.y *= 0;

                    //creación de angulo de vision y comprobacion de si el jugado esta dentro de este
                    float angle = Vector3.Angle(checkingObj.forward, directionBetween);

                    if (angle <= maxAngle)
                    {
                        Ray ray = new Ray(checkingObj.position, target.position - checkingObj.position);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, maxRadius))
                        {
                            if (hit.transform == target)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }
    #endregion
    #region Dibujado de gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, maxRadius);

        Vector3 line1 = Quaternion.AngleAxis(maxAngle, transform.up) * transform.forward * maxRadius;
        Vector3 line2 = Quaternion.AngleAxis(-maxAngle, transform.up) * transform.forward * maxRadius;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, line1);
        Gizmos.DrawRay(transform.position, line2);
        if (!isInView)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }

        Gizmos.DrawRay(transform.position, (player.position - transform.position).normalized * maxRadius);

        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, transform.forward * maxRadius);
    }
    #endregion
}
