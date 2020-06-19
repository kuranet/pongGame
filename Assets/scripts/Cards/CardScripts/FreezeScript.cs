using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class FreezeScript : MonoBehaviour
{
    public float time;
    Timer activityTime;
    PlayerAI[] playerScript;
    // Start is called before the first frame update
    private void OnEnable()
    {
        activityTime = gameObject.AddComponent<Timer>();
        activityTime.Duration = 3;
        activityTime.Run();

        GameObject[] player = GameObject.FindGameObjectsWithTag("AI");
        playerScript = new PlayerAI[player.Length];
        for (int i = 0; i < player.Length; i++)
        {
            playerScript[i] = player[i].GetComponent<PlayerAI>();
        }
        Run();
    }
    public async void Run()
    {
        Debug.Log("Start");
        while (!activityTime.Finished)
        {
            foreach (PlayerAI play in playerScript)
                play.shoulBeStopped = true;
            await Task.Yield();
        }
        foreach (PlayerAI play in playerScript)
            play.shoulBeStopped = false;
        Destroy(this);
        Destroy(gameObject.GetComponent<Timer>());
        Debug.Log("End");
    }
}
