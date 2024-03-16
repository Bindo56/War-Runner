using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Player player;
    public MainUI ui;

    public Color platformColor;
    public bool colorEntirePlatform;

    [Header("Score info")]
    public int coins;
    public float Distance;
    public float score;

    [Header("Bullet")]
    public int Bullet;


    [Header("skyBox")]
    [SerializeField] private Material[] skyBoxMat;
    private void Awake()
    {
        Time.timeScale = 1;
        Instance = this;

        SetupSkyBox(PlayerPrefs.GetInt("SkyBoxSetting"));
    }

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
    }


    public void SetupSkyBox(int i)
    {
        if (i <= 1)
        {
            RenderSettings.skybox = skyBoxMat[i];
        }
          else
        {
            RenderSettings.skybox = skyBoxMat[Random.Range(0, skyBoxMat.Length)];
        }

        PlayerPrefs.SetInt("SkyBoxSetting", i);

    }
           


    public void SaveColor(float r, float b,float g)
    {
        PlayerPrefs.SetFloat("ColorR", 1);
        PlayerPrefs.SetFloat("ColorG", 1);
        PlayerPrefs.SetFloat("ColorB", 1);
    }
    public void LoadColor()
    {
        PlayerPrefs.GetInt("Coins",5);
        SpriteRenderer sr =player.GetComponent<SpriteRenderer>();
        Color newColor = new Color (PlayerPrefs.GetFloat("colorR"),
                                    PlayerPrefs.GetFloat("colorG"),
                                    PlayerPrefs.GetFloat("colorB"),
                                    PlayerPrefs.GetFloat("colorA",1));

        sr.color = newColor;
    }


   
    public void UnlockPlayer()
    {
        player.playerUnlocked = true;
    }


    public void Update()
    {
        if (player.transform.position.x > Distance)
            Distance = player.transform.position.x;
    }



    public void Restart()
    {
        
        SceneManager.LoadScene(0);
    }

    public void save()
    {
        int savedCoins = PlayerPrefs.GetInt("Coins");

        PlayerPrefs.SetInt("Coins", savedCoins + coins);

         score = Distance * coins;

        PlayerPrefs.SetFloat("LastScore", score);

        if (PlayerPrefs.GetFloat("HighScore") < score)
        PlayerPrefs.SetFloat("HighScore", score);
    }

    public void GameEnded()
    {
        save();
        ui.OpenEndUI();
    }
          



       
}
