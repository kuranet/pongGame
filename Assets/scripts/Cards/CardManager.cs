using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class CardManager : MonoBehaviour
{
    public string status = "Waiting";
    [SerializeField] public GameObject waiting;
    [SerializeField] public GameObject activated;

    [SerializeField] public List<CardInfo> activationInfoList;

    public AnimationController controller;
    public TextMeshProUGUI description;
    public TextMeshProUGUI time;

    private void OnMouseDown()
    {
        if(status.Equals("Active"))
        {
            Debug.Log("KlikitiKlik");
            waiting.SetActive(true);
            activated.SetActive(false);
            status = "Waiting";
        }

    }
    public void Activation()
    {
        status = "Active";

        CardInfo activationInfo = activationInfoList[Random.Range(0, activationInfoList.Count-1)];

        controller.anim = activationInfo.animation;
        description.text = activationInfo.description;
        time.text = activationInfo.time.ToString();

        waiting.SetActive(false);
        activated.SetActive(true);
    }
}
