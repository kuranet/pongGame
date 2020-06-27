using UnityEngine;
using System.Threading.Tasks;
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
        Waiting();
    }
    

    void FixedUpdate()
    {
        if (!waiting)
        {
            //Don't start the game if player hasn't started it
            if (Input.GetKey(KeyCode.Space))
            {
                gameObject.tag = "Moving";
                RandomVelovity();
            }
            // Move the ball every frame
            if (gameObject.CompareTag("Moving"))
            {
                float speed = Mathf.Sqrt(Mathf.Pow(rb2d.velocity.x, 2) + Mathf.Pow(rb2d.velocity.y, 2));
                if (speed < 50)
                    rb2d.velocity += Time.deltaTime * rb2d.velocity / 100;
                Debug.DrawLine(transform.position,new Vector3(transform.position.x + rb2d.velocity.x, transform.position.y + rb2d.velocity.y,0));
                Debug.Log("X : " + rb2d.velocity.x + "  Y : " + rb2d.velocity.y);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        float collisionAngle = collision.gameObject.transform.rotation.eulerAngles.z * Mathf.PI / 180;
        
        Vector3 collisionRotation = new Vector3(Mathf.Cos(collisionAngle),Mathf.Sin(collisionAngle),0);
        
        Debug.Log("New collision vector is:  " + collisionRotation.x + "  ,  " + collisionRotation.y);       

        switch (collision.gameObject.tag)
        {       
            
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
            default: {
                    int rnd = Random.Range(-1, 1);
                    float angle = Mathf.PI / 36 * rnd;
                    rb2d.velocity = new Vector2(rb2d.velocity.x * Mathf.Cos(angle) - rb2d.velocity.y * Mathf.Sin(angle), rb2d.velocity.x * Mathf.Sin(angle) + rb2d.velocity.y * Mathf.Cos(angle));
                    break;
                }
        }         

    }
    private async void Waiting()
    {
        while (waiting)
        {            
            await Task.Yield();
        }
        RespawnBall();
    }
    public void RespawnBall()
    {
        ////Debug.Log("Previous is : " + previousMiss);
        //// Starter position of the ball
        transform.position = new Vector3(-2.75f, 0, 0);
        // Randomly choose the direction of the ball
        if (gameObject.CompareTag("Moving"))
            RandomVelovity();
        else
            rb2d.velocity = Vector2.zero;
    }
    private void RandomVelovity()
    {
        float n = EventManager.Instance.Players.Count; // number of segments
        n = 2 * Mathf.PI / n; // segment angle representation
        //Debug.Log("Max angle of segment : " + n);
        float angle = (Mathf.PI / 2 - n + Mathf.PI / 2) / 2; // generate random angle from 5 to n degrees
        angle = Random.Range(angle - Mathf.PI / 6, angle + Mathf.PI / 6);
        //Debug.Log("Angle is : " + angle * 180 / Mathf.PI);
        angle -= previousMiss * n;

        rb2d.velocity = 3 * new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        //rb2d.velocity = 2*new Vector2(0.8721476f, 0.4892429f);
        //Debug.Log("Direction vector : " + rb2d.velocity.x + "  ,  " + rb2d.velocity.y);
    }
}
