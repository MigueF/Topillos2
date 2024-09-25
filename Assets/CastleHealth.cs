using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleHealth : MonoBehaviour
{
    public int currenthealth;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    void Start()
    {
        currenthealth = numOfHearts;
    }
    // Update is called once per frame
    void Update()
    {

        if (currenthealth > numOfHearts)
        {
            currenthealth = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currenthealth)
            {
                hearts[i].sprite = fullHeart;
                Color color = Color.white;
                color.a = 255;
                hearts[i].color = color;
            }
            else
            {
                hearts[i].sprite = fullHeart;
                Color color = Color.red;
                color.a = 0.5f;
                hearts[i].color = color;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            currenthealth -= 1;
            Destroy(collision.gameObject);
        }
    }
}