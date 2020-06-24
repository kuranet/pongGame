using UnityEngine;

public class PlayerAI : Player
{
    Rigidbody2D rb2d;
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    //public float dY;
    public bool shoulBeStopped = false;
    public override void FixedUpdate()
    {
        Vector3 ballPosition = FindObjectOfType<Ball>().gameObject.transform.position;
        //Debug.LogWarning("X: " + ballPosition.x + "  Y: " + ballPosition.y);
        //Debug.LogError("X: " + transform.position.x + "  Y: " + transform.position.y);
        float distance = Mathf.Sqrt(Mathf.Pow(transform.position.x - ballPosition.x, 2) + Mathf.Pow(transform.position.y - ballPosition.y, 2));

        if (!shoulBeStopped)
            if (distance <= 4.5f)
            {
                float dY = ballPosition.y - transform.position.y;
                //Debug.LogWarning("dY : " + dY);
                if (dY != 0)
                    dY /= Mathf.Abs(dY);
                if (data.shouldmove)
                {
                    Vector3 pos = transform.position;
                    transform.Translate(0, 1.5f * dY * PlayersData.SPEED * Time.deltaTime, 0);
                    transform.position = Vector3.Lerp(transform.position, pos, 0.5f);
                }
                else
                {
                    Vector3 pos = transform.position;
                    if (dY * (sideOfCollisison - transform.position.y) > 0)
                        transform.Translate(0, -1.5f * dY * PlayersData.SPEED * Time.deltaTime, 0);
                    else
                        transform.Translate(0, 1.5f * dY * PlayersData.SPEED * Time.deltaTime, 0);
                    transform.position = Vector3.Lerp(transform.position, pos, 0.5f);

                }
                //Debug.Log(transform.position);
            }
    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision!");
        data.shouldmove = false;
        sideOfCollisison = collision.gameObject.transform.position.y;        
    }
}

