using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GamePlayScript gamePlayScript;
    
    public GameObject gameover;
    public GameObject winpanel;
    public Text HighScore;
    public GameObject backbutton;
    public GameObject Resetbtn;
    public GameObject mainmenu;
    public GameObject pausebtn;
    public GameObject resumebtn;

    public GameObject NextLevel;

    public int tapcounter;
    public int defaulttimervalue = 20;

    public AudioSource audiotest;

    public AudioClip clip;

    void Start()
    {
        gamePlayScript.timer = defaulttimervalue;
    }

    void Update()
    {
        if (gamePlayScript.timer > 0 && gamePlayScript.countdown <= 0)
        {
            if (!gamePlayScript.countdetect && Input.GetMouseButtonDown(0))
            {
                tapcounter++;
                int targetScoreValue = int.Parse(gamePlayScript.targetscore.GetComponent<Text>().text);

                if (tapcounter == targetScoreValue)
                {
                    gamePlayScript.bonusscore();
                }

                gamePlayScript.updatetapcounter();
            }
        }
        else
        {
            if (gamePlayScript.timer <= 0)
            {
                gamePlayScript.timer = 0;
                winorlostpanel();
            }
        }
    }

    public void winorlostpanel()
    {
        if (!gamePlayScript.haswon)
        {
            gameover.SetActive(true);
        }
        else
        {
            winpanel.SetActive(true);
        }
    }

    public void Nextlevell()
    {

        gamePlayScript.increaselevel();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void restartbtnclicked()
    {
        startbuttonaudio();
        SceneManager.LoadScene(1);
    }

    public void mainmenubtnclicked()
    {
        startbuttonaudio();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void pausebtnclicked()
    {
        startbuttonaudio();
        Time.timeScale = 0;
        gamePlayScript.countdetect = true;
        resumebtn.SetActive(true);
    }

    public void resumebnclicked()
    {
        startbuttonaudio();
        Time.timeScale = 1;
        resumebtn.SetActive(false);
        gamePlayScript.countdetect = false;
    }

    public void countdowncpmnt()
    {
        int roundedCountdown = Mathf.RoundToInt(gamePlayScript.countdown);
        gamePlayScript.countdowncmpnt.GetComponent<Text>().text = roundedCountdown.ToString();
    }

    public void audioteest()
    {
        audiotest.Play();
    }

    public void startbuttonaudio()
    {
        audiotest.PlayOneShot(clip);
    }
}
