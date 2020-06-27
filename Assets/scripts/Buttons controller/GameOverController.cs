using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    public FindCard cardList;
    public GameObject ball;
    private void OnEnable()
    {
        cardList.updating = false;
        ball.tag = "Waiting";
        ball.GetComponent<Ball>().RespawnBall();
        //ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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
