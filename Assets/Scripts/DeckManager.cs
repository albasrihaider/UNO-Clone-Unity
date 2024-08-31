using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform[] playerHands;

    private List<GameObject> deck = new List<GameObject>();

    void Start()
    {
        InitializeDeck();
        ShuffleDeck();
        DealCards();
    }

    void InitializeDeck()
    {
        string[] colors = { "Red", "Green", "Blue", "Yellow" };
        string[] values = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "Skip", "Flip", "Draw" };

        foreach (string color in colors)
        {
            foreach (string value in values)
            {
                for (int i = 0; i < 2; i++)  // Two of each card except '0'
                {
                    GameObject newCard = Instantiate(cardPrefab);
                    newCard.GetComponent<Card>().Initialize(color, value);
                    newCard.transform.SetParent(transform);
                    newCard.SetActive(false);
                    deck.Add(newCard);
                }
            }
        }

        // Add Wild cards separately
        for (int i = 0; i < 4; i++)
        {
            GameObject wildCard = Instantiate(cardPrefab);
            wildCard.GetComponent<Card>().Initialize("Special", "Change");
            wildCard.transform.SetParent(transform);
            wildCard.SetActive(false);
            deck.Add(wildCard);

            GameObject wildDrawFourCard = Instantiate(cardPrefab);
            wildDrawFourCard.GetComponent<Card>().Initialize("Special", "Draw");
            wildDrawFourCard.transform.SetParent(transform);
            wildDrawFourCard.SetActive(false);
            deck.Add(wildDrawFourCard);
        }
    }

    public GameObject DrawCard()
    {
        if (deck.Count == 0)
        {
            Debug.LogError("No more cards in the deck!");
            return null;
        }

        // Get the top card from the deck
        GameObject drawnCard = deck[0];
        deck.RemoveAt(0);

        // Optionally: Set the parent of the drawn card to a temporary holding position
        drawnCard.transform.SetParent(null);  // Remove from deck manager

        return drawnCard;
    }

    void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            GameObject temp = deck[i];
            int randomIndex = Random.Range(0, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    void DealCards()
    {
        for (int i = 0; i < 7; i++)  // Deal 7 cards to each player
        {
            foreach (Transform hand in playerHands)
            {
                GameObject cardToDeal = deck[0];
                deck.RemoveAt(0);
                cardToDeal.transform.SetParent(hand);
                cardToDeal.SetActive(true);
                // Adjust position with spacing
                float spacing = 0.5f; // Adjust as needed
                float startX = -((7 - 1) * spacing) / 2; // Center cards horizontally
                cardToDeal.transform.localPosition = new Vector3(startX + i * spacing, 0, 0);
            }
        }
    }
}
