using UnityEngine;

public class Puck : MonoBehaviour
{
    [Header("References")]
    public gameManager GameManager;
    public PlayerMovement playerMovement;
    public AIPaddle aiPaddle;
    public CameraShake cameraShake;

    public GameObject PVP_Player1;
    public GameObject PVP_Player2;

    [Header("Puck Settings")]
    private Rigidbody2D rb;
    public float nextTurnDirection = 0;
    public GameObject hitvfx;
    public float maxSpeed = 18f;

    [Header("Audio")]
    AudioSource ad;
    public AudioClip paddleHitSFX;
    public AudioClip boardHitSFX;
    public AudioClip goalSFX;
    public AudioClip roundwinSFX;

    private float lastHitTime = 0f;
    public float hitCooldown = 0.2f;
    public bool canPlayerMove;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ad = GetComponent<AudioSource>();

    }

    void FixedUpdate()
    {
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RedGoal")
        {

            ad.PlayOneShot(goalSFX);
            cameraShake.Shake(0.2f, 0.07f);
#if UNITY_ANDROID 
            Handheld.Vibrate();
#endif 

            Invoke("Roundwin_sfx", 0.7f);
            //    playerMovement.canMove = false;
            canPlayerMove = false;
            // aiPaddle.aiCanMove = false;
            GameManager.UpdatePlayerScore();

            GameManager.playerGoalText.SetActive(true);
            ResetPuck();
            nextTurnDirection = 1;
        }

        if (collision.gameObject.tag == "BlueGoal")
        {
            ad.PlayOneShot(goalSFX);
            cameraShake.Shake(0.2f, 0.07f);

#if UNITY_ANDROID 
            Handheld.Vibrate();
#endif            

            Invoke("Roundwin_sfx", 0.7f);
            // playerMovement.canMove = false;
            canPlayerMove = false;
            // aiPaddle.aiCanMove = false;
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
            GameObject hitVFXObject = Instantiate(hitvfx, collision.contacts[0].point, Quaternion.identity);
            Destroy(hitVFXObject, 1f);


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
        transform.position = new Vector3(0, -0.24f, 0);
        rb.linearVelocity = Vector2.zero;

        if (playerMovement != null)
        {
            playerMovement.gameObject.transform.position = new Vector3(0, -3.07f, 0);         // reset player position
            playerMovement.targetPos = new Vector3(0, -3.07f, 0);

            aiPaddle.gameObject.transform.position = new Vector3(0, 2.6f, 0);    // reset ai position
            aiPaddle.targetPos = new Vector3(0, 2.6f, 0);
        }
        else
        {
            PVP_Player1.GetComponent<PvP>().gameObject.transform.position = new Vector3(0, -3.07f, 0);         // reset player1 position
            PVP_Player1.GetComponent<PvP>().targetPos = new Vector3(0, -3.07f, 0);

            PVP_Player2.GetComponent<PvP>().gameObject.transform.position = new Vector3(0, 2.6f, 0);    // reset player2 position
            PVP_Player2.GetComponent<PvP>().targetPos = new Vector3(0, 2.6f, 0);
        }


        Invoke("HideGoalText", 1.5f);
        Invoke("NextRound", 2.5f);

    }
    private void HideGoalText()
    {
        GameManager.botGoalText.SetActive(false);
        GameManager.playerGoalText.SetActive(false);

    }
    public void NextRound()
    {

        // playerMovement.canMove = true;
        //aiPaddle.aiCanMove = true;

        canPlayerMove = true;
        Debug.Log("puck direction" + nextTurnDirection);
        Vector2 direction = new Vector2(0, nextTurnDirection);
        rb.AddForce(direction * 500f);
    }
}
