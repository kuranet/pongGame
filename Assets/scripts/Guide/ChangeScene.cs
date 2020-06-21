using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class ChangeScene : MonoBehaviour
{
    public GameObject[] scene;
    int activeScene;

    public Ball ball;
    private void Awake()
    {
        ball.waiting = true;
        //scene = gameObject.transform.GetComponentsInChildren<GameObject>();
        activeScene = 0;
        scene[activeScene].SetActive(true);
    }

    // Update is called once per frame
    
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            ball.waiting = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (activeScene < scene.Length - 1)
            {
                Debug.Log(activeScene);
                scene[activeScene].SetActive(false);
                activeScene++;
                scene[activeScene].SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
                ball.waiting = false;
            }
        }


    }
}
