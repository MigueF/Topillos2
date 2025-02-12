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

    [SerializeField] List<CardSO> deck;


    // Currently randomized cards
    GameObject cardOne, cardTwo, cardThree;


    List<CardSO> alreadySelectedCards = new List<CardSO>();


    void RandomizeNewCards()
    {
        if(cardOne != null) Destroy(cardOne);
        if(cardTwo != null) Destroy(cardTwo);
        if(cardThree != null) Destroy(cardThree);

        List<CardSO> randomizedCards = new List<CardSO>();

        List<CardSO> availableCards = new List<CardSO>();
        availableCards.RemoveAll(card =>
            card.isUnique && alreadySelectedCards.Contains(card) 
            // || card.unlockLevel > GameManager.Instance.GetCurrentLevel()
        );

        if(availableCards.Count < 3)
        {
            Debug.Log("Not enough available cards");
        }

        while (randomizedCards.Count < 3) 
        { 
            CardSO randomCard = availableCards[Random.Range(0, availableCards.Count)];
            if(!alreadySelectedCards.Contains(randomCard))
            {
                randomizedCards.Add(randomCard);
            }
        }

        cardOne = InstantiateCard(randomizedCards[0], cardPositionOne);
        cardTwo = InstantiateCard(randomizedCards[1], cardPositionTwo);
        cardThree = InstantiateCard(randomizedCards[2], cardPositionThree);

    }

    GameObject InstantiateCard(CardSO cards, Transform position)
    {
        GameObject cardGO = Instantiate(cardPrefab, position.position, Quaternion.identity, position);
        Card card = cardGO.GetComponent<Card>();
        card.Setup(CardSO);
        return cardGO;
    }
}
