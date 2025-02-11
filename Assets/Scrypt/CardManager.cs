using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] GameObject cardSelectionUI;

    [SerializeField] GameObject cardPrefab;

    [SerializeField] Transform cardPositionOne;

    [SerializeField] Transform cardPositionTwo;

    [SerializeField] Transform cardPositionThree;

    [SerializeField] List<Cards> deck;


    // Currently randomized cards
    GameObject cardOne, cardTwo, cardThree;


    List<Cards> alreadySelectedCards = new List<Cards>();


    void RandomizeNewCards()
    {
        if(cardOne != null) Destroy(cardOne);
        if(cardTwo != null) Destroy(cardTwo);
        if(cardThree != null) Destroy(cardThree);

        List<Cards> selectedCards = new List<Cards>();

        List<Cards> availableCards = new List<Cards>();
        availableCards.RemoveAll(card =>
            card.isUnique && alreadySelectedCards.Contains(card) 
            // || card.unlockLevel > GameManager.Instance.GetCurrentLevel()
        );

        if(availableCards.Count < 3)
        {
            Debug.Log("Not enough available cards");
        }


    }
}
