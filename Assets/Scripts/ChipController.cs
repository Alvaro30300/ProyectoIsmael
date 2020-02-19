using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider col)
    {
        //destruccion del chip a la hora de chocar con el player(cuando este lo recoje del suelo)
      if(col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Destroy(this.gameObject);
            PlayerController.instance.chips++;
        }
    }
}
