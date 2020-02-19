using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text activeWeapon;
    public Text nonRecargables;

    //public float granadas;
    public float chips;

    public static UIController instance;
    void Awake()
    {
        if(!instance)
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

    }

    // Update is called once per frame
    void Update()
    {
        //cambio de texto segun el arma activa(variable) dentro del codigo "WeaponSwitch"
        if (WeaponSwitch.instace.selectedWeapon == 0)
        {
            activeWeapon.text = "Normal Grenade";
        }
        if (WeaponSwitch.instace.selectedWeapon == 1)
        {
            activeWeapon.text = "Gun";
        }
        if (WeaponSwitch.instace.selectedWeapon == 2)
        {
            activeWeapon.text = "C4";
        }
        //texto para cantidad de chips actuales
        nonRecargables.text = "Chips: " + PlayerController.instance.chips.ToString();
    }
}
