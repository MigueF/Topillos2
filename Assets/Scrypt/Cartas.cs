using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartas : MonoBehaviour
{
    public GameObject Buttons;
    public bool activo;
    public bool cartascheck;
    bool solounavez;
    public GameObject Activarcartas;
    

    // Start is called before the first frame update
    void Start()
    {
        
        activo = false;
        Buttons.SetActive(activo);
        cartascheck = false;
        solounavez = false;

    }

    


    public void izquierda()
    {
        activo = false;
        Buttons.SetActive(activo);
    }

    public void centro()
    {
        activo = false;
        Buttons.SetActive(activo);
    }

    public void derecha()
    {

        activo = false;
        Buttons.SetActive(activo);
    }

    public void showCards()
    {
        if (!cartascheck)
        {
            activo = true;
            Buttons.SetActive(activo);
            cartascheck = true;
            Activarcartas.SetActive(false);

        }
       

    }

}




