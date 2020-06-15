using UnityEngine;

public class Card : MonoBehaviour
{
    public string status = "Waiting";
    [SerializeField] public GameObject waiting;
    [SerializeField] GameObject activated;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        if(status.Equals("Active"))
        {
            Debug.Log("KlikitiKlik");
            //waiting.SetActive(false);
            gameObject.GetComponent<Card>().status = "Waiting";
        }

    }
}
