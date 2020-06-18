using UnityEngine;
using System.Collections.Generic;

public class Ball : MonoBehaviour
{
    Vector2 direction; // direction ball moving
    float speed = 0.1f; // moving speed
    int previousMiss = 0; // number of player who previous miss the ball
    // Spawn ball at the start of our game
    void Start()
    {
        RespawnBall();
    }
    

    void FixedUpdate()
    {
        // Don't start the game if player hasn't started it
        if (Input.GetKey(KeyCode.Space))
            gameObject.tag = "Moving";
        // Move the ball every frame
        if (gameObject.CompareTag("Moving"))
        {
            transform.Translate(direction * speed);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        float collisionAngle = collision.gameObject.transform.rotation.eulerAngles.z * Mathf.PI / 180;
        
        Vector3 collisionRotation = new Vector3(Mathf.Cos(collisionAngle),0,0);
        if (collisionAngle < Mathf.PI)
            collisionRotation.y = Mathf.Sqrt(1 - Mathf.Pow(collisionRotation.x, 2));
        else
            collisionRotation.y = -Mathf.Sqrt(1 - Mathf.Pow(collisionRotation.x, 2)); 

        //Debug.Log("New collision vector is:  " + collisionRotation.x + "  ,  " + collisionRotation.y);
        

        switch (collision.gameObject.tag)
        {
            case "Player":                
            case "IntermediateWall":
                {
                    direction = Vector3.Reflect(direction, collisionRotation);
                    break;
                }
            case "GameOverWall":
                {
                    int indexOfPlayer = collision.gameObject.GetComponent<WallOvner>().Ovner.data.numberOfPlayer;

                    Debug.Log("Missed : " + indexOfPlayer);
                    previousMiss = indexOfPlayer;

                    EventManager.Instance.Players[previousMiss].Points++;
                    EventManager.Instance.UpdateUI(previousMiss);
                    
                    RespawnBall();
                    foreach (Player player in EventManager.Instance.Players)
                        if (player.data.score >= 5)
                        {
                            this.tag = "Waiting";
                            EventManager.Instance.EndGame();
                        }                    
                    break;
                }
        }         

    }
    private void RespawnBall()
    {
        //Debug.Log("Previous is : " + previousMiss);
        // Starter position of the ball
        transform.position = new Vector3(-3, 0, 0);
        // Randomly choose the direction of the ball
        float n = EventManager.Instance.Players.Count; // number of segments
        n = 2 * Mathf.PI / n; // segment angle representation
        //Debug.Log("Max angle of segment : " + n);
        float angle = Random.Range(Mathf.PI / 2 - n, Mathf.PI / 2 ); // generate random angle from 5 to n degrees
        //Debug.Log("Angle is : " + angle);
        direction = new Vector2(Mathf.Cos((previousMiss + 1) * angle), Mathf.Sin((previousMiss + 1) * angle));
        //Debug.Log("Direction vector : " + direction.x + "  ,  " + direction.y);
    }
}
