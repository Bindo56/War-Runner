using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


[Serializable]
public struct ColorToSell
{
    public Color color;
    public int price;
}

public enum ColorType
{
    playerColor,
    platformColor

}

public class UiShop : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI coinstext;
    [SerializeField] private TextMeshProUGUI notifyText;

    [Space]


    [Header("Platform Color")]
    [SerializeField] private GameObject platformColorButton;
    [SerializeField] private Transform platformColorParent;
    [SerializeField] private Image platformDisplay;
    [SerializeField] public ColorToSell[] platformColors;

    [Header("PlayerColor")]
    [SerializeField]  private GameObject PlayerColorButton;
     [SerializeField] private Transform PlayerColorParent;
    [SerializeField] private Image  PlayerDisplay;
    [SerializeField] public ColorToSell[] playerColors;


    void Start()
    {
        coinstext.text = PlayerPrefs.GetInt("Coins").ToString("#,#");


        for (int i = 0; i < platformColors.Length; i++)
        {
            Color color = platformColors[i].color;
            int price = platformColors[i].price;

            GameObject newbutton = Instantiate(platformColorButton, platformColorParent);

            newbutton.transform.GetChild(0).GetComponent<Image>().color = color;
            newbutton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = price.ToString("#,#");

            newbutton.GetComponent<Button>().onClick.AddListener(() => PurchaseColor(color, price, ColorType.platformColor));

        }

        for (int i = 0; i < playerColors.Length; i++)
        {
            Color color = playerColors[i].color;
            int price = playerColors[i].price;

            GameObject newbutton = Instantiate(PlayerColorButton, PlayerColorParent);

            newbutton.transform.GetChild(0).GetComponent<Image>().color = color;
            newbutton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = price.ToString("#,#");

            newbutton.GetComponent<Button>().onClick.AddListener(() => PurchaseColor(color, price, ColorType.playerColor));

        }
    }

    public void PurchaseColor(Color color, int price, ColorType colorType)
    {
        AudioManager.instance.PlaySFX(3);   
        if (EnoughMoney(price))
        {
            if (colorType == ColorType.platformColor)
            {
                GameManager.Instance.platformColor = color;
                platformDisplay.color = color;

            }

            else if (colorType == ColorType.playerColor)
            {
                GameManager.Instance.player.GetComponent<SpriteRenderer>().color = color;
                GameManager.Instance.SaveColor(color.r, color.g, color.b);
                PlayerDisplay.color = color;


            }




            StartCoroutine(Notify("Purchase successful", 1));
        }
        else
            StartCoroutine(Notify("Not enough money", 1));
    }


    private bool EnoughMoney(int price)
    {
        int myCoins = PlayerPrefs.GetInt("Coins");

        if (myCoins > price)
        {
            int newAmountOfCoins = myCoins - price;
            PlayerPrefs.SetInt("Coins", newAmountOfCoins);
            coinstext.text = PlayerPrefs.GetInt("Coins").ToString("#,#");
            return true;


        }

        return false;
    }
    

    IEnumerator Notify(string text, float seconds)
    {
        notifyText.text =   text;

        yield   return new WaitForSeconds(seconds);

        notifyText.text = "Click to buy";
    }


















}
