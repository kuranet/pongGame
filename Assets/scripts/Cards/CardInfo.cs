using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newCard", menuName = "Card")]
public class CardInfo : ScriptableObject
{
    public string cardName;
    public AnimationClip animation;
    public string description;
    public float time;
}
