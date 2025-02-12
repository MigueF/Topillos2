using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class CardSO : ScriptableObject
{
    public Sprite cardImage; // The image of the card

    public string cardText; // The text on the card

    public CardEffect effectType; // The effect

    public float effectValue; // The value of the effect
    
    public bool isUnique; // If unique, the card will not be randomized again if it's already selected.

    public int unlockLevel;

}

public enum CardEffect 
{ 
    MoneyIncrease




}
