using UnityEngine;
using System.Collections.Generic;

public class LevelBuilder : MonoBehaviour
{
    public Card cardPrefab;          // Reference to Card prefab
    public CardData cardDataPool;    // Reference to scriptable object
    public Transform cardParent;     // Parent for all cards
    public float xSpacing = 2f;      // Horizontal spacing
    public float ySpacing = 2f;      // Vertical spacing


    /// <summary>
    /// Build a level grid with random card pairs from CardData
    /// </summary>
    public void BuildLevel(int rows, int columns, CardData cardDataPool)
    {
        // Clear existing cards
        foreach (Transform child in cardParent)
            Destroy(child.gameObject);

        int totalSlots = rows * columns;
        List<CardInfo> cardsToPlace = new List<CardInfo>();

        // Number of pairs needed
        int pairsNeeded = totalSlots / 2;

        // Shuffle the pool
        List<CardInfo> shuffledPool = new List<CardInfo>(cardDataPool.cards);
        for (int i = 0; i < shuffledPool.Count; i++)
        {
            CardInfo temp = shuffledPool[i];
            int randIndex = Random.Range(i, shuffledPool.Count);
            shuffledPool[i] = shuffledPool[randIndex];
            shuffledPool[randIndex] = temp;
        }

        // Pick pairsNeeded unique cards and duplicate each for matching
        for (int i = 0; i < pairsNeeded; i++)
        {
            CardInfo card = shuffledPool[i];
            cardsToPlace.Add(card); // first copy
            cardsToPlace.Add(card); // second copy
        }

        // If odd number of slots, optionally add one extra card
        if (totalSlots % 2 != 0)
        {
            cardsToPlace.Add(shuffledPool[pairsNeeded]);
        }

        // Shuffle final list to randomize positions
        for (int i = 0; i < cardsToPlace.Count; i++)
        {
            CardInfo temp = cardsToPlace[i];
            int randIndex = Random.Range(i, cardsToPlace.Count);
            cardsToPlace[i] = cardsToPlace[randIndex];
            cardsToPlace[randIndex] = temp;
        }

        // Instantiate cards in grid
        int index = 0;
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                if (index >= cardsToPlace.Count) break;

                Vector3 pos = new Vector3(c * xSpacing, -r * ySpacing, 0f);
                Card newCard = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity, cardParent);
                newCard.transform.localPosition = pos;
                newCard.Initialize(cardsToPlace[index].id, cardsToPlace[index].frontSprite);
                index++;
            }
        }

        CenterCardParent(rows, columns);
    }

    void CenterCardParent(int rows, int columns)
    {
        float totalWidth = (columns - 1) * xSpacing;
        float totalHeight = (rows - 1) * ySpacing;

        // Offset parent by half the width/height
        Vector3 offset = new Vector3(-totalWidth / 2f, totalHeight / 2f, 20f);
        cardParent.position = offset;
    }

}
