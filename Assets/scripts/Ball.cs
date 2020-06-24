using UnityEngine;
using System.Collections.Generic;

public class Ball : MonoBehaviour
{
    public bool waiting = true;
   // Vector2 direction; // direction ball moving
    float speed = 0.1f; // moving speed
    int previousMiss = 0; // number of player who previous miss the ball
    Rigidbody2D rb2d;
    // Spawn ball at the start of our game
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        RespawnBall();
    }
    

    void FixedUpdate()
    {
        //if (!waiting)
        //{
        // Don't start the game if player hasn't started it
        //if (Input.GetKey(KeyCode.Space))
        //    gameObject.tag = "Moving";
        //// Move the ball every frame
        //if (gameObject.CompareTag("Moving"))
        //{
        float speed = Mathf.Sqrt(Mathf.Pow(rb2d.velocity.x, 2) + Mathf.Pow(rb2d.velocity.y, 2));
        if(speed<50)
            rb2d.velocity += 3* Time.deltaTime*rb2d.velocity/1000;
        Debug.Log("X : " + rb2d.velocity.x + "  Y : " + rb2d.velocity.y);
            //}
        //}
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        float collisionAngle = collision.gameObject.transform.rotation.eulerAngles.z * Mathf.PI / 180;
        
        Vector3 collisionRotation = new Vector3(Mathf.Cos(collisionAngle),Mathf.Sin(collisionAngle),0);
        
        Debug.Log("New collision vector is:  " + collisionRotation.x + "  ,  " + collisionRotation.y);
        

        switch (collision.gameObject.tag)
        {
            //case "Player":
            //case "AI":
            //case "IntermediateWall":
            //    {
            //        float angle = Mathf.Acos((rb2d.velocity.x * collisionRotation.x + rb2d.velocity.y * collisionRotation.y) / Mathf.Sqrt(Mathf.Pow(rb2d.velocity.x, 2) + Mathf.Pow(rb2d.velocity.y, 2)));
            //        Debug.Log("Angle : " + angle * 180 / Mathf.PI);
            //        if ((angle > (Mathf.PI - Mathf.PI / 18) && angle < (Mathf.PI + Mathf.PI / 18)))
            //        {
            //            Debug.LogWarning("Change direction!");
            //            angle = (Mathf.PI - angle) * 2 + Mathf.PI / 36;
            //            if (rb2d.velocity.y * rb2d.velocity.x >= 0)
            //            {
            //                Debug.LogWarning("X before : " + rb2d.velocity.x);
            //                Debug.LogError("X first : " + rb2d.velocity.x * Mathf.Cos(angle));
            //                Debug.LogError("X second : -" + rb2d.velocity.y * Mathf.Sin(angle));
            //                Debug.LogWarning("X : " + (rb2d.velocity.x * Mathf.Cos(angle) - rb2d.velocity.y * Mathf.Sin(angle)));
            //                Debug.LogWarning("Y before : " + rb2d.velocity.y);
            //                Debug.LogError("Y first : " + rb2d.velocity.x * Mathf.Sin(angle));
            //                Debug.LogError("Y second : " + rb2d.velocity.y * Mathf.Cos(angle));
            //                Debug.LogWarning("Y : " + (rb2d.velocity.x * Mathf.Sin(angle) + rb2d.velocity.y * Mathf.Cos(angle)));
            //                rb2d.velocity = new Vector2(-(rb2d.velocity.x * Mathf.Cos(angle) - rb2d.velocity.y * Mathf.Sin(angle)), rb2d.velocity.x * Mathf.Sin(angle) + rb2d.velocity.y * Mathf.Cos(angle));

            //            }
            //            else
            //            {
            //                rb2d.velocity = new Vector2(-(rb2d.velocity.x * Mathf.Cos(-angle) - rb2d.velocity.y * Mathf.Sin(-angle)), rb2d.velocity.x * Mathf.Sin(-angle) + rb2d.velocity.y * Mathf.Cos(-angle));

            //            }
            //        }

            //        else
            //        {
            //            rb2d.velocity = -Vector3.Reflect(rb2d.velocity, collisionRotation);
            //        }
            //        break;
            //    }
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
        ////Debug.Log("Previous is : " + previousMiss);
        //// Starter position of the ball
        transform.position = new Vector3(-2.75f, 0, 0);
        // Randomly choose the direction of the ball
        float n = EventManager.Instance.Players.Count; // number of segments
        n = 2 * Mathf.PI / n; // segment angle representation
        //Debug.Log("Max angle of segment : " + n);
        float angle = (Mathf.PI / 2 - n + Mathf.PI / 2) / 2; // generate random angle from 5 to n degrees
        angle = Random.Range(angle - Mathf.PI / 6, angle + Mathf.PI / 6);
        Debug.Log("Angle is : " + angle * 180 / Mathf.PI);
        angle -= previousMiss * n;

        rb2d.velocity = 3*new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        //rb2d.velocity = 2*new Vector2(0.8721476f, 0.4892429f);
        Debug.Log("Direction vector : " + rb2d.velocity.x + "  ,  " + rb2d.velocity.y);
    }
}
