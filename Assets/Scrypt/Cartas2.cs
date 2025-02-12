using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartas2 : MonoBehaviour
{
    public GameObject Buttons;
    public bool activo;
    public bool cartascheck;
    public GameObject Activarcartas;


    // Start is called before the first frame update
    void Start()
    {

        activo = false;
        Buttons.SetActive(activo);
        cartascheck = false;
    }




    public void izquierdaDos()
    {
        activo = false;
        Buttons.SetActive(activo);
    }

    public void centroDos()
    {
        activo = false;
        Buttons.SetActive(activo);
    }

    public void derechaDos()
    {

        activo = false;
        Buttons.SetActive(activo);
    }

    public void showCardsDos()
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
