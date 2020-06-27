using UnityEngine;
using System.Threading.Tasks;

public class WallScript : MonoBehaviour
{
    public float time;
    public GameObject wall;
    Timer activityTime;
    GameObject player;
    private void OnEnable()
    {
        activityTime = gameObject.AddComponent<Timer>();
        activityTime.Duration = 5;
        activityTime.Run();

        player = GameObject.FindGameObjectWithTag("Player");        
        Run();
    }
    public async void Run()
    {
        Debug.Log("Start");
        wall = GameObject.FindGameObjectWithTag("IntermediateWall");
        wall = Instantiate(wall);
        if (player.transform.rotation.z == 0)
            wall.transform.position = new Vector3(-7f, 0, 0);
        else
            wall.transform.position = new Vector3(-2.75f, -2.75f, 0);
        wall.transform.rotation = player.transform.rotation;
        wall.transform.localScale = new Vector3(0.1f, 7.1f, 0);
       
        while (!activityTime.Finished)
        {            
            await Task.Yield();
        }
        
        Destroy(wall);
        Destroy(this);
        Destroy(gameObject.GetComponent<Timer>());
        Debug.Log("End");
    }
}
