using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVidaController : MonoBehaviour
{
    public Image barra;

    // Start is called before the first frame update
    void Start()
    {
        barra = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //la imagen de la barra de vida tiene en cuenta la vida actual para estar rellenarse en el porcentaje que deba respecto a la vida máxima.
        barra.fillAmount = (float)PlayerController.instance.playerSettings.life / PlayerController.instance.playerSettings.maxLife;
    }
}
