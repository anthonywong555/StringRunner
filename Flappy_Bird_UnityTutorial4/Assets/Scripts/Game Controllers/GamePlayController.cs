using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;

    [SerializeField]
    private Text scoreText, endScore, bestScore, gameOverText;

    [SerializeField]
    private Button restartGameButton, instructionsButton;

    [SerializeField]
    private GameObject pausePanel;

    [SerializeField]
    private GameObject[] birds;

    [SerializeField]
    private Sprite[] medals;

    [SerializeField]
    private Image medalImage;

    void Awake()
    {
        MakeInstance();
        Time.timeScale = 0f; //Pause everything!
    }

	// Use this for initialization
	void Start()
    {
	
	}

    void MakeInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    //When user pauses game
    public void PauseGame()
    {
        //Check to see if we have a bird
        if(BirdScript.instance != null)
        {
            //check to see if bird is alive
            if (BirdScript.instance.isAlive)
            {
                pausePanel.SetActive(true); //Open pause panel by activating it
                gameOverText.gameObject.SetActive(false); //Dont show game over textm they are onlying pasuing
                endScore.text = BirdScript.instance.score.ToString(); //Show score
                bestScore.text = GameController.instance.GetHighScore().ToString(); //Show the high score so far
                Time.timeScale = 0f; //Pause everything!
                restartGameButton.onClick.RemoveAllListeners(); //Making sure old listerns do not runin the game
                restartGameButton.onClick.AddListener(() => ResumeGame()); // Resuming the game
            }
        }
    }

    public void GoToMenuButton()
    {
        SceneFader.instance.FadeIn("MainMenu"); //Fade in to main menu and then fade out for user to see the main menu
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false); //Turn pause panel off
        Time.timeScale = 1f; //Give the world time again!
    }

    public void RestartGame()
    {
        SceneFader.instance.FadeIn(SceneManager.GetActiveScene().name); //Re-load this level basically
    }

    public void PlayGame()
    {
        scoreText.gameObject.SetActive(true); //Start showing user there score
        birds[GameController.instance.GetSelectedBird()].SetActive(true); //Turn the users bird of choice on
        instructionsButton.gameObject.SetActive(false); //Turn it off, it is no longer needed
        Time.timeScale = 1f; //Give the world time again!
    }

    public void SetScore(int score)
    {
        scoreText.text = "" + score;
    }

    public void PlayerDiedShowScore(int score)
    {
        pausePanel.SetActive(true); //Open pause panel
        gameOverText.gameObject.SetActive(true); //Show game over text
        scoreText.gameObject.SetActive(false); //turn off score text
        endScore.text = score.ToString(); //Score player has finished with

        //If score is greater then highscore
        if(score > GameController.instance.GetHighScore())
        {
            GameController.instance.SetHighScore(score); //Set new highscore
        }

        bestScore.text = GameController.instance.GetHighScore().ToString(); //Show highscore

        //If user only get 20 or below as a score
        if(score <= 20)
        {
            medalImage.sprite = medals[0]; //Give them the bronze medal
        }
        else if(score < 20 && score < 40) //If score is between 20 and 40
        {
            medalImage.sprite = medals[1]; //Give silver medal

            checkBirdsUnlockStatus();
        }
        else if(score > 40)
        {
            medalImage.sprite = medals[2]; //Give gold medal

            checkBirdsUnlockStatus();
        }

        restartGameButton.onClick.RemoveAllListeners();
        restartGameButton.onClick.AddListener(() => RestartGame());
    }

    void checkBirdsUnlockStatus()
    {
        //If green bird is not unlocked
        if (GameController.instance.IsGreenBirdUnlocked() == 0)
        {
            GameController.instance.UnlockGreenBird(); //Unlock green bird
        }

        //If red bird is not unlocked
        if (GameController.instance.IsRedBirdUnlocked() == 0)
        {
            GameController.instance.UnlockRedBird(); //Unlock red bird
        }
    }
}
