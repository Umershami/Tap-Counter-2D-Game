using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePlayScript : MonoBehaviour
{
    public GameManager GameManager;
    public GameObject bonusrewarded;
    public GameObject nobonus;

    public GameObject TapCounterText;
    public GameObject countdowncmpnt;
    public GameObject Timer;
    public GameObject resetbtn;
    public GameObject resetbtnafterwin;
    public GameObject levell;

    public float timer;
    public bool stoptimer = false;
    public bool haswon;
    int targetcount;
    public static int highscore;
    public static int reset;
    public bool countdetect;
    public bool countdowntimerend = false;
    public float countdown;
    public GameObject targetscore;

    public AudioClip[] playbtnsound;

    int level = 1;
    int basemultiplier = 6;
    int bonusScore = 0; // Variable to track bonus score

    void Start()
    {
        nobonus.SetActive(false);   
        bonusrewarded.SetActive(false);
        countdown = 3f;

        level = PlayerPrefs.GetInt("LevelNum", 1);
        levell.GetComponent<Text>().text =level.ToString(); 
        bonusScore = PlayerPrefs.GetInt("Bonus", 0);

        if (level == 1 || bonusScore == 0)
        {
            
            GameManager.tapcounter = 0; // Reset tap counter if on level 1 or no bonus
        }
        else
        {
             
            GameManager.tapcounter += bonusScore;
         // Apply bonus score for levels greater than 1
        }

        updatetapcounter();

        targetcount = levelset(level);
        Debug.Log("Level: " + level + ", Target Count: " + targetcount);
        loadhighscore();
        timer = GameManager.defaulttimervalue;
    }

    void Update()
    {
        if (!countdowntimerend)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0)
            {
                countdowntimerend = true;
            }
            GameManager.countdowncpmnt();
        }

        if (countdowntimerend && !stoptimer)
        {
            countdowncmpnt.SetActive(false);
            timer -= Time.deltaTime;
            int roundedtimer = Mathf.RoundToInt(timer);
            Timer.GetComponent<Text>().text = roundedtimer.ToString();

            if (timer <= 0)
            {
                stoptimer = true;
                timer = 0;
                Timer.GetComponent<Text>().text = timer.ToString();

                haswon = GameManager.tapcounter >= targetcount;

                GameManager.winorlostpanel();

                if (GameManager.tapcounter > highscore)
                {
                    highscore = GameManager.tapcounter;
                }
                savehighscore();
                highscoreupdate();
            }
        }
    }

    public void bonusscore()
    {
        if (timer > 3)
        {
            nobonus.SetActive(false);
            bonusrewarded.SetActive(true);
            bonusScore = 5;
            PlayerPrefs.SetInt("Bonus", bonusScore); // Save the bonus score
            Debug.Log("Bonus Score earned: " + bonusScore);
        }
        else
        {
            nobonus.SetActive(true);
            bonusScore = 0; // No bonus if time is less than 3 seconds
            PlayerPrefs.SetInt("Bonus", bonusScore); // Save the removal of the bonus
            Debug.Log("No bonus score earned");
        }
    }

    public void increaselevel()
    {
        level++;
        PlayerPrefs.SetInt("LevelNum", level);

        // Apply the bonus score if earned
        if (bonusScore > 0)
        {
            GameManager.tapcounter += bonusScore;
        }
        else
        {
            GameManager.tapcounter = 0; // Reset tap counter if no bonus
        }

        // Reset the level with the new score
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void resetbtnclicked()
    {
        level = 1;
        PlayerPrefs.SetInt("LevelNum", level);
        PlayerPrefs.SetInt("Bonus", 0); // Reset bonus score on game reset
        resetvalues();
    }

    public void resetvalues()
    {
        GameManager.startbuttonaudio();
        level = PlayerPrefs.GetInt("LevelNum", 1);
        bonusScore = 0; // Reset bonus score
        targetcount = levelset(level);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void updatetapcounter()
    {
        TapCounterText.GetComponent<Text>().text = GameManager.tapcounter.ToString();
        playtapsound();
    }

    public void highscoreupdate()
    {
        GameManager.HighScore.GetComponent<Text>().text = highscore.ToString();
    }

    public void savehighscore()
    {
        PlayerPrefs.SetInt("HighScore", highscore);
    }

    public void loadhighscore()
    {
        highscore = PlayerPrefs.GetInt("HighScore");
    }

    public void restbtnclicked()
    {
        reset = 0;
        PlayerPrefs.SetInt("HighScore", reset);
        loadrestclicked();
    }

    public void loadrestclicked()
    {
        highscore = PlayerPrefs.GetInt("HighScore");
        GameManager.HighScore.GetComponent<Text>().text = highscore.ToString();
    }

    void playtapsound()
    {
        int randno = Random.Range(0, playbtnsound.Length);
        AudioClip clip = playbtnsound[randno];
        GameManager.audiotest.PlayOneShot(clip);
    }


    int levelset(int level)
    {
        int temp = level * basemultiplier;
        targetscore.GetComponent<Text>().text = temp.ToString();
        return temp;
    }
}
