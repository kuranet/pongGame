using UnityEngine;
using System.Collections.Generic;

public class Ball : MonoBehaviour
{
    public bool waiting = true;
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
        if (!waiting)
        {
            // Don't start the game if player hasn't started it
            if (Input.GetKey(KeyCode.Space))
                gameObject.tag = "Moving";
            // Move the ball every frame
            if (gameObject.CompareTag("Moving"))
            {
                transform.Translate(direction * speed);
                Debug.Log("X : " + direction.x + "  Y : " + direction.y);
            }
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
            case "AI":
            case "IntermediateWall":
                {
                    float angle = Mathf.Acos((direction.x* collisionRotation.x + direction.y* collisionRotation.y)/Mathf.Sqrt(Mathf.Pow(direction.x,2)+ Mathf.Pow(direction.y, 2)));
                    
                    if ((angle > (Mathf.PI - Mathf.PI / 18) && angle < (Mathf.PI  + Mathf.PI / 18)))
                    {
                        //Debug.Log(direction.y);
                        angle = (Mathf.PI - angle) * 2 + Mathf.PI / 36;
                        if (direction.y*direction.x >= 0)
                        {
                        direction.x = -(direction.x * Mathf.Cos(angle) - direction.y * Mathf.Sin(angle));
                        direction.y = -(direction.x * Mathf.Sin(angle) + direction.y * Mathf.Cos(angle));
                        }
                        else
                        {
                            direction.x = -(direction.x * Mathf.Cos(-angle) - direction.y * Mathf.Sin(-angle));
                            direction.y = -(direction.x * Mathf.Sin(-angle) + direction.y * Mathf.Cos(-angle));
                        }
                    }
                    
                    else
                    {
                        direction = Vector3.Reflect(direction, collisionRotation);
                    }
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
        transform.position = new Vector3(-2.75f, 0, 0);
        // Randomly choose the direction of the ball
        float n = EventManager.Instance.Players.Count; // number of segments
        n = 2 * Mathf.PI / n; // segment angle representation
        //Debug.Log("Max angle of segment : " + n);
        float angle = (Mathf.PI / 2 - n + Mathf.PI / 2)/2; // generate random angle from 5 to n degrees
        angle = Random.Range(angle - Mathf.PI / 6, angle + Mathf.PI / 6);
        Debug.Log("Angle is : " + angle*180/Mathf.PI);
        angle -= previousMiss * n;

        direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        Debug.Log("Direction vector : " + direction.x + "  ,  " + direction.y);
    }
}
