using UnityEngine;
using UnityEngine.UI;

public class PlayersData : MonoBehaviour
{
    [SerializeField] public const float SPEED = 6f;
    [SerializeField] public int score;
    [SerializeField] public int numberOfPlayer;
    [SerializeField] public GameObject wall;
    [SerializeField] public Text ScoreUI;
    public bool shouldmove = true;

}
