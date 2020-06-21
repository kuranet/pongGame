using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class CardManager : MonoBehaviour
{
    public string status = "Waiting";
    [SerializeField] public GameObject waiting;
    [SerializeField] public GameObject activated;

    [SerializeField] public List<CardInfo> activationInfoList;

    CardInfo currentCard;

    public AnimationController controller;
    public TextMeshProUGUI description;
    public TextMeshProUGUI time;

    private void OnMouseDown()
    {
        if(status.Equals("Active"))
        {
            Debug.Log("KlikitiKlik");
            waiting.SetActive(true);

            switch (currentCard.cardName)
            {
                case "Freeze":
                    {
                        FreezeScript scriptSettings = activated.transform.parent.gameObject.AddComponent<FreezeScript>();
                        scriptSettings.time = currentCard.time;
                        break;
                    }
                case "Wall": {
                        WallScript scriptSettings = activated.transform.parent.gameObject.AddComponent<WallScript>();
                        scriptSettings.time = currentCard.time;
                        break; }
            }

            activated.SetActive(false);
            status = "Waiting";
        }
    }
    public void Activation()
    {
        status = "Active";
        
        CardInfo activationInfo = activationInfoList[Random.Range(0, activationInfoList.Count )];
        //activationInfo = activationInfoList[1];
        currentCard = activationInfo;

        controller.anim = activationInfo.animation;
        description.text = activationInfo.description;
        time.text = activationInfo.time.ToString();

        waiting.SetActive(false);
        activated.SetActive(true);
    }
    public void RespawnCard()
    {
        waiting.SetActive(true);
        waiting.GetComponent<CardTimer>().timer.running = false; 
        activated.SetActive(false);
        status = "Waiting";
    }
}
