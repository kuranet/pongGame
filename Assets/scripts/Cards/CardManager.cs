using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] List<GameObject> cardsInQueue;
    int indexOfUpdatingCard;
    // Start is called before the first frame update
    void Start()
    {
        indexOfUpdatingCard = 0;
        cardsInQueue[indexOfUpdatingCard].GetComponent<Card>().status = "Updating";
        RunTimer(indexOfUpdatingCard);
    }

    // Update is called once per frame
    void Update()
    {
        if(cardsInQueue[indexOfUpdatingCard].GetComponent<Card>().waiting.GetComponent<CardTimer>().timer.Finished)
        {
            for(int i = 0; i< cardsInQueue.Count;i++)
                if (cardsInQueue[i].GetComponent<Card>().status.Equals("Waiting"))
                {
                    cardsInQueue[i].GetComponent<Card>().status = "Updating";
                    RunTimer(i);
                    indexOfUpdatingCard = i;
                    Debug.Log(i);
                    break;
                }
            //bool findToUpdate = false;
            //int i = indexOfUpdatingCard;
            //if (i == cardsInQueue.Count - 1)
            //    i = 0;
            //else
            //    i++;
            //while (!findToUpdate) 
            //{
            //    if (cardsInQueue[i].GetComponent<Card>().status.Equals("Waiting"))
            //    {
            //        cardsInQueue[i].GetComponent<Card>().status = "Updating";
            //        RunTimer(i);
            //        indexOfUpdatingCard = i;
            //        findToUpdate = true;
            //        Debug.Log(i);
            //    }
            //    else 
            //    {
            //        Debug.Log(i);
            //        if (i == cardsInQueue.Count - 1)
            //            i = 0;
            //        else if (i == indexOfUpdatingCard)
            //            findToUpdate = true;
            //        else
            //            i++;
            //        Debug.LogWarning(i);
            //    }
            //}
        }        
    }
    void RunTimer(int i)
    {
        cardsInQueue[i].GetComponent<Card>().waiting.GetComponent<CardTimer>().timer.Run();
    }
    
}
