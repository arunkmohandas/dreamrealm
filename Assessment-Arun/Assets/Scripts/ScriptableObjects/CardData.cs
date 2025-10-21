using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "Card Game/Card Data")]
public class CardData : ScriptableObject
{
   public CardInfo[] cards;
}

[Serializable]
public class CardInfo
{
    public int id;              // Unique card id
    public Sprite frontSprite;  // Front image of the card
}
