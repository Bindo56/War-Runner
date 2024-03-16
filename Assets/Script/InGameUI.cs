using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    private Player player;

    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI BulletText;

    [SerializeField] private Image heartEmpty;
    [SerializeField] private Image heartfull;


    private float distance;
    private float coins;
    float bullets;


    void Start()
    {
        player = GameManager.Instance.player;
       // InvokeRepeating("UpdateInfo", 0, .2f);
    }

    private void Update()
    {
        UpdateInfo();
    }

    // Update is called once per frame
    void UpdateInfo()
    {
        distance = GameManager.Instance.Distance;
        coins = GameManager.Instance.coins;
        bullets = GameManager.Instance.Bullet;

        if(distance > 0)
        distanceText.text = GameManager.Instance.Distance.ToString("#,#") + "   m";

        if(coins > 0)
        coinsText.text = GameManager.Instance.coins.ToString("#,#");
        
        if(bullets > 0)
        BulletText.text = GameManager.Instance.Bullet.ToString("#,#");

        heartEmpty.enabled = !player.extraLife;
        heartfull.enabled = player.extraLife;
    }
}
