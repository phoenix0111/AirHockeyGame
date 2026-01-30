using UnityEngine;

public class Puck : MonoBehaviour
{
    public gameManager GameManager;
    public PlayerMovement playerMovement;
    public AIPaddle aiPaddle;
    private Rigidbody2D rb;
    public float nextTurnDirection = 0;

    [Header("Audio")]
    AudioSource ad;
    public AudioClip paddleHitSFX;
    public AudioClip boardHitSFX;
    public  AudioClip goalSFX;
    public AudioClip roundwinSFX;

    private float lastHitTime = 0f;
    public float hitCooldown = 0.2f; // 0.2 seconds between hits


    void Start()
    {
       rb =  GetComponent<Rigidbody2D>();
        playerMovement.canMove = false;
       ad = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RedGoal")
        {
            ad.PlayOneShot(goalSFX);
            Invoke("Roundwin_sfx", 0.7f);
            playerMovement.canMove = false;
            aiPaddle.aiCanMove = false;
            GameManager.UpdatePlayerScore();
            ad.PlayOneShot(roundwinSFX);
            GameManager.playerGoalText.SetActive(true);
            ResetPuck();
            nextTurnDirection = 1;
        }
        if (collision.gameObject.tag == "BlueGoal")
        {
            ad.PlayOneShot(goalSFX);
            Invoke("Roundwin_sfx", 0.7f);
            playerMovement.canMove = false;
            aiPaddle.aiCanMove = false;
            GameManager.UpdateBotScore();
          
            GameManager.botGoalText.SetActive(true);
           
            ResetPuck();
            nextTurnDirection = -1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Time.time - lastHitTime > hitCooldown)
            {
                ad.PlayOneShot(paddleHitSFX);
                ad.volume = 1;
                lastHitTime = Time.time;
            }
        }
        if (collision.gameObject.tag == "BoundaryColliders")
        {
            if (Time.time - lastHitTime > hitCooldown)
            {
                ad.PlayOneShot(boardHitSFX);
                lastHitTime = Time.time;
            }
        }
    }

    private void Roundwin_sfx()
    {
        ad.volume = 0.3f;
        ad.PlayOneShot(roundwinSFX);
        
    }

    private void ResetPuck()
    {
        transform.position = new Vector3(0, -0.24f,0);
        rb.linearVelocity = Vector2.zero;
        
        playerMovement.gameObject.transform.position = new Vector3(0, -3.07f, 0);         // reset player position
        playerMovement.targetPos = new Vector3(0, -3.07f, 0);

        aiPaddle.gameObject.transform.position = new Vector3(0, 2.79f, 0);    // reset ai position
        aiPaddle.targetPos = new Vector3(0, 2.79f, 0);
        
        Invoke("HideGoalText", 1f);
        Invoke("NextRound", 2f);
       
    }
    private void HideGoalText()
    {
        GameManager.botGoalText.SetActive(false);
        GameManager.playerGoalText.SetActive(false);

    }
   public void NextRound()
    {
        
        playerMovement.canMove = true;
        aiPaddle.aiCanMove = true;
        Debug.Log("puck direction" + nextTurnDirection);
        Vector2 direction = new Vector2(0,nextTurnDirection);
        rb.AddForce(direction * 500f);
    }
}
