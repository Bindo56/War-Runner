using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    private int amountOfCoin;
    [SerializeField] private GameObject coinprefab;
    [SerializeField] private int minCoin;
    [SerializeField] private int maxCoin;


    void Start()
    {
        amountOfCoin = Random.Range(minCoin, maxCoin);
        int additionaloffSet = amountOfCoin / 2; //deviding coins by 2 for getting in center of the coinGen
        
        for (int i = 0; i < amountOfCoin; i++) //looping between number 
        {
            Vector3 offset = new Vector2(i - additionaloffSet, 0); // 

            Instantiate(coinprefab, transform.position + offset, Quaternion.identity, transform);

        }

    }

}
