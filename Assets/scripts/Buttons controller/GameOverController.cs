using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
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
    }
}
