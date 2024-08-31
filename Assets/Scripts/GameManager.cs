using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public DeckManager deckManager;
    public GameObject discardPile;
    private string currentColor;
    private string currentValue;

    private int currentPlayer = 0;
    private int turnDirection = 1; // 1 for clockwise, -1 for counter-clockwise

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        // Draw the first card from the deck to start the game
        PlayCard(deckManager.DrawCard());
    }

    public void PlayCard(GameObject card)
    {
        Card cardComponent = card.GetComponent<Card>();

        if (IsValidPlay(cardComponent))
        {
            currentColor = cardComponent.cardColor;
            currentValue = cardComponent.cardValue;

            // Move card to the discard pile
            card.transform.SetParent(discardPile.transform);
            card.transform.position = discardPile.transform.position;

            // Handle special card effects
            HandleSpecialCards(cardComponent);

            // Proceed to the next player's turn
            NextTurn();
        }
        else
        {
            Debug.Log("Invalid play!");
            // Handle invalid play, e.g., return the card to the player's hand
        }
    }

    bool IsValidPlay(Card card)
    {
        // Check if the card matches the current color, value, or is a Wild card
        return card.cardColor == currentColor || card.cardValue == currentValue || card.cardColor == "Special";
    }

    void HandleSpecialCards(Card card)
    {
        switch (card.cardValue)
        {
            case "Skip":
                NextTurn(); // Skip the next player's turn
                break;
            case "Reverse":
                ReverseTurnOrder(); // Reverse the direction of play
                break;
            case "DrawTwo":
                DrawCards(2); // For Draw Two cards
                break;
            case "Change":
                // Implement logic for choosing a new color
                break;
            case "DrawFour":
                DrawCards(4); // For Wild Draw Four cards
                break;
        }
    }

    void NextTurn()
    {
        // Move to the next player's turn based on the current direction
        currentPlayer = (currentPlayer + turnDirection + deckManager.playerHands.Length) % deckManager.playerHands.Length;

        // Implement turn switching logic
        Debug.Log("Next Turn: Player " + currentPlayer);
    }

    void ReverseTurnOrder()
    {
        // Reverse the direction of play
        turnDirection *= -1;
    }

    void DrawCards(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject drawnCard = deckManager.DrawCard();
            if (drawnCard != null)
            {
                // Add the drawn card to the current player's hand
                drawnCard.SetActive(true);
                drawnCard.transform.SetParent(deckManager.playerHands[currentPlayer]);

                // Adjust position of drawn card
                float spacing = 0.5f; // Adjust as needed
                float startX = -((deckManager.playerHands[currentPlayer].childCount - 1) * spacing) / 2; // Center the cards horizontally
                drawnCard.transform.localPosition = new Vector3(startX + (deckManager.playerHands[currentPlayer].childCount - 1) * spacing, 0, 0);
            }
        }
    }

    public void DrawFromDeck()
    {
        GameObject drawnCard = deckManager.DrawCard();

        if (drawnCard != null)
        {
            // Add the drawn card to the player's hand
            drawnCard.SetActive(true);
            drawnCard.transform.SetParent(deckManager.playerHands[currentPlayer]);
            drawnCard.transform.position = deckManager.playerHands[currentPlayer].position;
        }
    }
}
