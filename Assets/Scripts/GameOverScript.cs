using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOverScript : MonoBehaviour
{
    private Text htext, text;
    private Button playagaınbutton, maınmenubutton, resetbutton, exitbutton;

    private void Awake()
    {
        text = GameObject.Find("ScoreText").GetComponent<Text>();
        htext = GameObject.Find("HighestScoreText").GetComponent<Text>();
        playagaınbutton = GameObject.Find("PlayAgainButton").GetComponent<Button>();
        resetbutton = GameObject.Find("ResetButton").GetComponent<Button>();
        maınmenubutton = GameObject.Find("MainMenuButton").GetComponent<Button>();
        exitbutton = GameObject.Find("ExitButton").GetComponent<Button>();
        playagaınbutton.onClick.AddListener(yenıdenoyna);
        resetbutton.onClick.AddListener(resetle);
        maınmenubutton.onClick.AddListener(anamenu);
        exitbutton.onClick.AddListener(cıkıs);
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("Skor") > PlayerPrefs.GetInt("HSkor", 0))
        {
            PlayerPrefs.SetInt("HSkor", PlayerPrefs.GetInt("Skor"));
            PlayerPrefs.Save();
        }

        yazdır();
    }

    void yazdır()
    {
        text.text = "Score: " + PlayerPrefs.GetInt("Skor");
        htext.text = "Highest Score: " + PlayerPrefs.GetInt("HSkor");
    }

    void yenıdenoyna()
    {
        SceneManager.LoadScene(1);
    }

    void resetle()
    {
        PlayerPrefs.SetInt("HSkor", PlayerPrefs.GetInt("Skor"));
        PlayerPrefs.Save();
        yazdır();
    }

    void anamenu()
    {
        SceneManager.LoadScene(0);
    }

    void cıkıs()
    {
        Application.Quit();
    }
}