using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private int cardId;
    private bool isOpen;
    [SerializeField] private SpriteRenderer cardImage;
    [SerializeField] private Animator flipAnimator;
    public int CardID { get { return cardId; } }
    public bool IsOpen { get { return isOpen; } }   

    /// <summary>
    /// To Assign sprite and id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="sprite"></param>
    public void InitializeCard(int id, Sprite sprite)
    {
        cardId = id; 
        isOpen = false;
        cardImage.sprite = sprite;
    }

    /// <summary>
    /// To Open the card
    /// </summary>
    public void FlipOpen()
    {
        isOpen = true;
        flipAnimator.SetTrigger("open");
    }

    /// <summary>
    /// To close the card
    /// </summary>
    public void FlipClose()
    {
        isOpen = false;
        flipAnimator.SetTrigger("close");
    }

}
