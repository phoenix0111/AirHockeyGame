using UnityEngine;
using UnityEngine.UI;   
using TMPro;


public class gameManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI botScoreText;
    public TextMeshProUGUI countdownText;
    
    public GameObject playerGoalText;
    public GameObject botGoalText;

    [Header("Essentials")]
    public Puck puck;
    public PlayerMovement playerMovement;
    int playerScore = 0;
    int botScore = 0;
    float timer = 3;
    bool gameStarted = false;


    void Start()
    {
        playerMovement.canMove = false;                   // disable player movement until game starts

        Debug. Log("Game Countdown Started");

        playerScoreText.text = "0";
        botScoreText.text = "0";
         
    }

    
    void Update()
    {
        
       timer -= Time.deltaTime;
       countdownText.text = timer.ToString("0");

        if (gameStarted == false  && timer <= 0)
        {
            countdownText.text = "GO!";
           
            Invoke("HideCountdownText", 0.5f);

            gameStarted = true;
        }
    }

    public void UpdatePlayerScore()
    {
        playerScore = playerScore + 1;
        playerScoreText.text = playerScore.ToString();
    }

    public void UpdateBotScore()
    {
        botScore = botScore + 1;
        playerScoreText.text = botScore.ToString();
    }

    private void HideCountdownText()
    {
        countdownText.gameObject.SetActive(false);
        playerMovement.canMove = true;
        Debug.Log("Game Started");
       
        puck.nextTurnDirection = Random.Range(0, 2) == 0 ? -1 : 1;
        Debug.Log("Initial Puck Direction: " + puck.nextTurnDirection);
        puck.NextRound();                                                 // 1st round begings  
    }
}
