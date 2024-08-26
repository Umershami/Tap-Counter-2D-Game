using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public GameObject MainMenu;
    public GameObject Settings;
    public GameObject Credits;
    public GameObject vlumebtn;
    public GameObject Howtoplay;
    public Animator slidesanimator;
    public GameObject objectives;
    public GameObject gamemechanics;
    public GameObject basiccontrol;
    public GameObject howtoplaybackbtn;

    public AudioSource audiotest;
    public UnityEngine.UI.Slider volumeslider;

    public GameObject objbackbtn;
    public GameObject basiccntrlbackbtn;
    public GameObject gamemechnbackbtn;

    public GameObject objectsection;
    public GameObject basicocntrolsection;
    public GameObject gamemechanicssection;

    public GameObject fadepanel;
    public bool disable = false;

    void Start()
    {

        audiotest.playOnAwake = true;
        
        fadein();
        MainMenu.SetActive(true);
        Settings.SetActive(false);
        Howtoplay.SetActive(false);
        Credits.SetActive(false);
        getvolume();
        Debug.Log("Start function activated");
    }

    public void SettingsBtnClicked()
    {
        vlumebtn.SetActive(true);
        volumeslider.gameObject.SetActive(false);

        MainMenu.SetActive(false);
        Settings.SetActive(true);
        slidesanimator.SetTrigger("slide-down");
        Debug.Log("Settings btn activated");
    }

    public void voumebtnclicked()
    {
        
        vlumebtn.SetActive(false);
        volumeslider.gameObject.SetActive(true);
        if (volumeslider != null && audiotest != null)
        {
            volumeslider.value = audiotest.volume;
        }
        volumeslider.onValueChanged.AddListener(delegate { Setvolume(volumeslider.value); });
        getvolume();
    }
    public void getvolume()
    {
    
        float volume = PlayerPrefs.GetFloat("volume", 1.0f);
        if (audiotest != null)
        {
            audiotest.volume = volume;
            if (volumeslider != null)
            {
                volumeslider.value = volume;
            }
        }
        Debug.Log("Volume loaded: " + volume);
    }
    public void Setvolume(float volume)
    {
        if (audiotest != null)
        {
            audiotest.volume = volume;
            PlayerPrefs.SetFloat("volume",audiotest.volume);
            PlayerPrefs.Save();
        }
    }

    public void CreditsClicked()
    {
        MainMenu.SetActive(false);
        Credits.SetActive(true);
        Debug.Log("Credits function activated");
    }

    public void HowToPlayBtnClicked()
    {
        MainMenu.SetActive(false);
        Howtoplay.SetActive(true);
        objectives.SetActive(true);
        gamemechanics.SetActive(true);
        basiccontrol.SetActive(true);




        Debug.Log("How To Play function activated");
    }

    public void objectivesbtnclicked()
    {
        objectsection.SetActive(true);
        howtoplaybackbtn.SetActive(false);
        gamemechanics.SetActive(false);
        basiccontrol.SetActive(false);
        Howtoplay.SetActive(false);
    }

    public void basiccntrlbtnclcikcked()
    {
        objectives.SetActive(false);
        howtoplaybackbtn.SetActive(false);

        gamemechanics.SetActive(false);
        Howtoplay.SetActive(false);
        basicocntrolsection.SetActive(true);

    }

    public void gamemechanicsbtnclicked()
    {
        objectives.SetActive(false);
        howtoplaybackbtn.SetActive(false);

        gamemechanicssection.SetActive(true);
        Howtoplay.SetActive(false);
        basiccontrol.SetActive(false);
    }

    public void BackBtn()
    {
        Debug.Log("BackbtnClickedonhowtoplay");
        MainMenu.SetActive(true);
        // Settings.SetActive(false);
        Howtoplay.SetActive(false);

        Credits.SetActive(false);

        slidesanimator.SetTrigger("slide-up");
        Invoke("DisableAnimatorAfterDelay", 1);

        Debug.Log("Back button clicked");

    }
    public void objecbackbtnclicked()
    {
        howtoplaybackbtn.SetActive(true);
        basiccontrol.SetActive(true);
        Howtoplay.SetActive(true);
        gamemechanics.SetActive(true);
        objectives.SetActive(true);
        basicocntrolsection.SetActive(false);
        gamemechanicssection.SetActive(false);
        objectsection.SetActive(false);

    }


    public void playBtnClicked()
    {
        fadeout();
        StartCoroutine(loadgameplayscene());

    }

    void Update()
    {

    }

    private void DisableAnimatorAfterDelay()
    {

        Settings.SetActive(false);

    }
    void fadein()
    {
        fadepanel.GetComponent<Animator>().SetTrigger("Fade-In");
    }
    void fadeout()
    {
        fadepanel.GetComponent<Animator>().SetTrigger("Fade-out");
    }
    IEnumerator loadgameplayscene()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(1);

    }
}
