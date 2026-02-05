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
    public bool isPvPMode;


    void Start()
    {
        Debug.Log("Game Countdown Started");

        playerScoreText.text = "0";
        botScoreText.text = "0";

    }


    void Update()
    {
        if (!gameStarted)
        {


            timer -= Time.deltaTime;
            countdownText.text = timer.ToString("0");

            if (timer <= 0)
            {
                countdownText.text = "GO!";

                Invoke("HideCountdownText", 1f);

                gameStarted = true;
            }
        }

        if (playerScore == 10 || botScore == 10)
        {
            Debug.Log("Game Over");
            puck.canPlayerMove = false;
            puck.gameObject.SetActive(false);

            if (playerScore == 10)
            {
                if (isPvPMode)
                {
                    countdownText.text = "Blue Player Wins!";
                    countdownText.gameObject.SetActive(true);
                    Debug.Log("Player Wins the Game!");
                }
                else
                {


                    countdownText.text = "You Win!";
                    countdownText.gameObject.SetActive(true);
                    Debug.Log("Player Wins the Game!");
                }
            }
            else
            {
                if (isPvPMode)
                {
                    countdownText.text = "Red Player Wins!";
                    countdownText.gameObject.SetActive(true);
                    Debug.Log("Player Wins the Game!");
                }
                else
                {
                    countdownText.text = "Bot Wins!";
                    countdownText.gameObject.SetActive(true);
                    Debug.Log("Bot Wins the Game!");
                }
            }
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
        botScoreText.text = botScore.ToString();
    }

    private void HideCountdownText()
    {
        countdownText.gameObject.SetActive(false);
        puck.canPlayerMove = true;
        Debug.Log("Game Started");

        puck.nextTurnDirection = Random.Range(0, 2) == 0 ? -1 : 1;
        Debug.Log("Initial Puck Direction: " + puck.nextTurnDirection);
        puck.NextRound();                                                 // 1st round begings  
    }
}
