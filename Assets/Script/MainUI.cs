using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    private bool pausedGame;
    private bool isMuted;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject endUI;

    [SerializeField] private TextMeshProUGUI lastScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI coinstext;

    [SerializeField] private UI_Vlolume[] slider;
    [SerializeField] private Image muteIcon;
    [SerializeField] private Image inGameMuteIcon;



    private void Start()
    {
        for (int i = 0; i < slider.Length; i++)
        {
            slider[i].SetupSlider();
        }

        

        SwitchMenuTo(mainMenu);
        

        lastScoreText.text = "Last Score:  " + PlayerPrefs.GetFloat("LastScore").ToString("# ,# ") + "  m";
        highScoreText.text = "High Score:  " + PlayerPrefs.GetFloat("HighScore").ToString("# ,# ") + "  m";
    }

    private void Update()
    {
        
    }


    public void SwitchMenuTo(GameObject uiMenu)
    {
        for (int i = 0;  i < transform.childCount; i ++)
        {
            transform.GetChild(i).gameObject.SetActive(false);

                
        }

        uiMenu.SetActive(true);
        AudioManager.instance.PlaySFX(3);

        coinstext.text = PlayerPrefs.GetInt("Coins").ToString();
    }

    public void SwitchSkyBox(int index)
    {
        AudioManager.instance.PlaySFX(3);
        GameManager.Instance.SetupSkyBox(index);
    }

    public void MuteButton()
    {
        Color mutecolor = new Color(1, 1, 1, 0.5f);
        isMuted = !isMuted; //switcher 
        if (isMuted)
        {
            muteIcon.color = mutecolor;
            AudioListener.volume = 0;
        }
        else
        {
            muteIcon.color = new Color(1,1,1,0);
            AudioListener.volume = 1;
        }
    }



    public void StartGame()
    {
        muteIcon = inGameMuteIcon;

        if (isMuted)
            muteIcon.color = new Color(1, 1, 1, 0.5f);

        GameManager.Instance.UnlockPlayer();
    }





    public void gamePaused()
    {
        if (pausedGame)
        {
            Time.timeScale = 1.0f;
            pausedGame = false;
        }
        else
        {
            Time.timeScale = 0.0f;
            pausedGame = true;
        }

    }

    public void OpenEndUI()
    {
        SwitchMenuTo(endUI);
    }

}
