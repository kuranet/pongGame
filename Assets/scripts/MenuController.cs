using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void Load2Players()
    {
        SceneManager.LoadScene("2PlayersRoom");
    }
    public void Load3Players()
    {
        SceneManager.LoadScene("3PlayersRoom");
    }
}
