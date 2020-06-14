using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{    
    public List<Player> Players = new List<Player>();
    [SerializeField] public GameObject endGame;
    [SerializeField] public Text score;

    #region SingleTon
    public static EventManager Instance { get; private set; }
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateUI(int raketNumber)
    {
        int points = Players[raketNumber].data.score;
        Players[raketNumber].data.ScoreUI.text = points.ToString();
    }
    public void EndGame()
    {
        endGame.SetActive(true);
        //Text[] score = endGame.GetComponentsInChildren<Text>();
        string strScore = "";
        for(int i = 0; i< Players.Count; i++)
        {
            strScore += Players[i].data.score;
            if (i != Players.Count - 1)
                strScore += " : ";
        }
        score.text = strScore;   

    }
}
