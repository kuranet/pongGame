using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    public FindCard cardList;
    private void Start()
    {
        cardList.updating = false;
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void PlayAgain()
    {
        for(int i = 0; i < EventManager.Instance.Players.Count; i++)
        {
            EventManager.Instance.Players[i].data.score = 0;
            EventManager.Instance.UpdateUI(i);
        }
        GameObject canvas = transform.parent.gameObject;
        canvas.SetActive(false);
        
        foreach (GameObject card in cardList.cardsInQueue)
            card.GetComponent<CardManager>().RespawnCard();
        cardList.updating = true;
    }
}
