using UnityEngine;

public class Puck : MonoBehaviour
{
    public gameManager GameManager;
    public PlayerMovement playerMovement;
    private Rigidbody2D rb;
    public float nextTurnDirection = 0;
   
    
    void Start()
    {
       rb =  GetComponent<Rigidbody2D>();
        NextRound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RedGoal")
        {
           playerMovement.canMove = false;
            GameManager.UpdatePlayerScore();
            GameManager.playerGoalText.SetActive(true);
            ResetPuck();
            nextTurnDirection = 1;
        }
        else if (collision.gameObject.tag == "BlueGoal")
        {
            playerMovement.canMove = false;
            GameManager.UpdateBotScore();
            GameManager.botGoalText.SetActive(true);
           
            ResetPuck();
            nextTurnDirection = -1;
        }
    }

    private void ResetPuck()
    {
        transform.position = new Vector3(0, -0.24f,0);
        rb.linearVelocity = Vector2.zero;
        GameManager.botGoalText.SetActive(false);
        GameManager.playerGoalText.SetActive(false);
        Invoke("NextRound", 2f);
    }

   public void NextRound()
    {
        playerMovement.canMove = true;
        Debug.Log("puck direction" + nextTurnDirection);
        Vector2 direction = new Vector2(0,nextTurnDirection);
        rb.AddForce(direction * 500f);
    }
}
