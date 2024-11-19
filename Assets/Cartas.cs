using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartas : MonoBehaviour
{
    public GameObject Buttons;
    public bool activo;
    public bool alreadyExecuted;
  
    

    // Start is called before the first frame update
    void Start()
    {
        
        activo = false;
        Buttons.SetActive(activo);
        alreadyExecuted = false;

    }

    // Update is called once per frame
   /* void Update()
    {
        int OleCarta = EnemyWaveSpawner.currentWaveIndex;
        //WaveCartas();
   
        showCards();
        
    }




    void WaveCartas()
    {
        activo = true;
        if (!alreadyExecuted)
        {
            Debug.Log("gqaewhrg");
            alreadyExecuted = true;


            if (EnemyWaveSpawner.currentWaveIndex == 2)
            {
                Debug.Log("IJKHYIKJIKJYIJKYI");
                    
                Buttons.SetActive(activo);
                

            }

            
        }
        

    }
   */

    public void izquierda()
    {
        activo = false;
    }

    public void centro()
    {
        activo = false;
    }

    public void derecha()
    {

        activo = false;
    }

    public void showCards()
    {

        activo = true;
        Buttons.SetActive(activo);

    }

}




