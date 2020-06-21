using System.Collections.Generic;
using UnityEngine;

public class FindCard : MonoBehaviour
{
    [SerializeField] public List<GameObject> cardsInQueue;
    int indexOfUpdatingCard;
    public bool updating = false;
    // Start is called before the first frame update
    void Start()
    {
        indexOfUpdatingCard = 0;
        //CardManager thisCard = cardsInQueue[indexOfUpdatingCard].GetComponent<CardManager>();
        //thisCard.status = "Updating";
        //RunTimer(thisCard);
    }

    // Update is called once per frame
    void Update()
    {
        if (updating)
        {
            CardManager thisCard;
            thisCard = cardsInQueue[indexOfUpdatingCard].GetComponent<CardManager>();            
            if (thisCard.waiting.GetComponent<CardTimer>().timer.Finished)
            {
                for (int i = 0; i < cardsInQueue.Count; i++)
                {
                    thisCard = cardsInQueue[i].GetComponent<CardManager>();
                    if (thisCard.status.Equals("Waiting"))
                    {
                        thisCard.status = "Updating";
                        RunTimer(thisCard);
                        indexOfUpdatingCard = i;
                        Debug.Log(i);
                        break;
                    }
                }
            }
            else if (indexOfUpdatingCard == 0 && !thisCard.waiting.GetComponent<CardTimer>().timer.Running)
            {
                thisCard.status = "Updating";
                RunTimer(thisCard);
            }
        }    
    }
    void RunTimer(CardManager card)
    {
        card.waiting.GetComponent<CardTimer>().timer.Run();
    }
    
}
