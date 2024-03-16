using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class endui : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI distance;
    [SerializeField] private TextMeshProUGUI coins;
    [SerializeField] private TextMeshProUGUI score;
    void Start()
    {
        GameManager manager = GameManager.Instance;

        Time.timeScale = 0;

        if (manager.Distance <= 0)
            return;

        if(manager.coins <= 0)
               return;


        distance.text = "Distance " + manager.Distance.ToString("#,#") +" m ";
        coins.text = "Coins " + manager.coins.ToString("#,#");
        score.text = "Score " + manager.score.ToString("#,#");



    }

   
}
