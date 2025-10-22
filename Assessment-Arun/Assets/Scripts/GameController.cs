using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour
{
    public LevelBuilder levelBuilder;
    public CardData cardDataPool;
    public InputManager inputManager;
    public int rows = 4;
    public int columns = 3;
    public static Action<bool> OnGameOver;

    private Card firstFlippedCard = null;
    private bool canFlip = true;

    private List<Card> allCards = new List<Card>();


    //void Start()
    //{
    //    StartLevel();
    //}

    /// <summary>
    /// To Start level
    /// </summary>
    /// <param name="levelInfo"></param>
    public void StartLevel(LevelInfo levelInfo)
    {
        rows=levelInfo.rows;
        columns=levelInfo.columns;
        // Clear previous cards and build new level
        levelBuilder.BuildLevel(rows, columns, cardDataPool);

        // Get all card instances
        allCards.Clear();
        foreach (Transform child in levelBuilder.cardParent)
        {
            Card card = child.GetComponent<Card>();
            if (card != null)
                allCards.Add(card);

            // Subscribe to card click
            inputManager.OnCardClicked = OnCardClicked;
        }
    }

    /// <summary>
    /// Susbscribe to inputmanager OnCardClicked
    /// </summary>
    /// <param name="clickedCard"></param>
    private void OnCardClicked(Card clickedCard)
    {
        if (!canFlip || clickedCard.IsOpen)
            return;

        //Flip Audio
        AudioManager.Instance?.PlayFlip();

        clickedCard.FlipOpen();

        if (firstFlippedCard == null)
        {
            // First card flipped
            firstFlippedCard = clickedCard;
            //Register click on game manager
            GameManager.Instance.RegisterClick(false);
        }
        else
        {
            // Second card flipped, check match
            if (firstFlippedCard.CardID == clickedCard.CardID)
            {
                //Play match audio
                AudioManager.Instance?.PlayMatch();
                //Register click on game manager
                GameManager.Instance.RegisterClick(true);

                // Match, Leave both open
                firstFlippedCard = null;

                // Check if all cards are open
                if (CheckGameWon())
                {
                    Debug.Log("Game Won!");
                    GameManager.Instance.OnGameWon();
                }

            }
            else
            {
                StartCoroutine(CloseCards(firstFlippedCard, clickedCard));
                firstFlippedCard = null;
            }
        }
       
    }

    /// <summary>
    /// Close opend card when they mismatch
    /// </summary>
    /// <param name="card1"></param>
    /// <param name="card2"></param>
    /// <returns></returns>
    private IEnumerator CloseCards(Card card1, Card card2)
    {
        canFlip = false;
        yield return new WaitForSeconds(0.5f); // delay before closing

        //Play mismatch audio
        AudioManager.Instance?.PlayMismatch();

        card1.FlipClose();
        card2.FlipClose();
        canFlip = true;
    }

    /// <summary>
    /// Check for game over
    /// </summary>
    /// <returns></returns>
    private bool CheckGameWon()
    {
        foreach (Card card in allCards)
        {
            if (card != null)
            {
                if (!card.IsOpen)
                    return false;
            }
        }
        return true;
    }
}
