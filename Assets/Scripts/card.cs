using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card : MonoBehaviour
{
	public string cardColor;
	public string cardValue;

	public SpriteRenderer cardRenderer;  // The SpriteRenderer for the card's background
	
	public void Initialize(string color, string value)
	{
		cardColor = color;
		cardValue = value;

		UpdateCardAppearance();
	}

	private void UpdateCardAppearance()
	{
		// Construct the sprite path based on the card color and value
		string spritePath = $"UNO Cards/{cardColor}/{cardColor}_{cardValue}";

		// Load the sprite from the Resources folder (assuming your sprites are there)
		Sprite cardSprite = Resources.Load<Sprite>(spritePath);

		// Check if the sprite was found
		if (cardSprite != null)
		{
			cardRenderer.sprite = cardSprite;
		}
		else
		{
			Debug.LogError($"Sprite '{spritePath}' not found! Make sure the naming convention is correct.");
		}

	}

	private void OnMouseDown()
	{
		// Handle card selection logic
		Debug.Log("Card Selected: " + cardColor + " " + cardValue);
		// Implement the logic to play this card
	}
}

