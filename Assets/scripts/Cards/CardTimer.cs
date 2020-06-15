using System;
using TMPro;
using UnityEngine;

public class CardTimer : MonoBehaviour
{
    //float startTime;
    public Timer timer;
    Card parentCard;
    string status;
    [SerializeField] TextMeshProUGUI waitingTime;

    private void Awake()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = 5;
        parentCard = gameObject.AddComponent<Card>();
        waitingTime.text = "Waiting";
        
    }
    // Update is called once per frame
    void Update()
    {
        status = transform.parent.gameObject.GetComponent<Card>().status;
        if (timer.Running)
        {

            
                waitingTime.color = new Color32(255, 255, 255, 255);
                waitingTime.text = (timer.Duration - Mathf.Round(timer.elapsedSeconds)).ToString();
            
            //if (timer.Finished)
            //    transform.parent.gameObject.GetComponent<Card>().status = "Active";
        }
        else if (status.Equals("Updating"))
        {
            transform.parent.gameObject.GetComponent<Card>().status = "Active";
            waitingTime.text = "Klick on me";
            waitingTime.color = new Color32(0, 255, 0, 255);
        }
        if (status.Equals("Waiting"))
        {
            waitingTime.text = "Waiting";
            waitingTime.color = new Color32(255, 255, 0, 255);
        }
        //Debug.Log("Activated!");
        //parentCard.status = "Active";

    }
}
