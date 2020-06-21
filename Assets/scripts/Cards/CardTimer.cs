using TMPro;
using UnityEngine;

public class CardTimer : MonoBehaviour
{
    //float startTime;
    public Timer timer;
    CardManager parentCard;
    string status;
    [SerializeField] TextMeshProUGUI waitingTime;

    private void Awake()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = 15;
        parentCard = transform.parent.gameObject.GetComponent<CardManager>();
        waitingTime.text = "Waiting";
        
    }
    // Update is called once per frame
    void Update()
    {
        status = parentCard.status;
        if (timer.Running)
        {            
                waitingTime.color = new Color32(255, 255, 255, 255);
                waitingTime.text = (timer.Duration - Mathf.Round(timer.elapsedSeconds)).ToString();
        }
        else if (status.Equals("Updating"))
        {
            parentCard.Activation();
        }
        if (status.Equals("Waiting"))
        {
            waitingTime.text = "Waiting";
            waitingTime.color = new Color32(255, 255, 0, 255);
        }
    }
}
