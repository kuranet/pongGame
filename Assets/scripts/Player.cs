using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public PlayersData data;
    protected float sideOfCollisison;
    public int Points
    {
        get
        {
            return data.score;
        }
        set
        {
            if (value > 0 && data.score != value)
            {
                data.score = value;
            }
        }
    }

    private void Start()
    {        
        transform.rotation = data.wall.transform.rotation;
        data.wall.GetComponent<WallOvner>().Ovner = this;
    }

    public virtual void FixedUpdate()
    {
        float movement;

        if (transform.rotation.z == 0)
        {
            movement = Input.GetAxis("Player1");
            Moving(movement, sideOfCollisison);
        }
        else
        {
            movement = Input.GetAxis("Player2");
            Moving(movement, sideOfCollisison);
        }
        
    }
    void Moving(float movement, float pos)
    {
        if (data.shouldmove)
            transform.Translate(0, PlayersData.SPEED * movement * Time.deltaTime, 0);
        else
        {
            Debug.Log(movement + "  :  " + (sideOfCollisison - pos));
            if (movement * (sideOfCollisison - transform.position.y) > 0)
                transform.Translate(0, -0.3f * PlayersData.SPEED * movement * Time.deltaTime, 0);
            else
                transform.Translate(0, 0.3f * PlayersData.SPEED * movement * Time.deltaTime, 0);
        }
    }
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision!");
        data.shouldmove = false;
        if (transform.rotation.z == 0)
            sideOfCollisison = collision.gameObject.transform.position.y;
        else
            sideOfCollisison = collision.gameObject.transform.position.x;
    }
    protected void OnCollisionExit2D(Collision2D collision)
    {
        data.shouldmove = true;
    }
}
