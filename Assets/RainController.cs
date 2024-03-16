using DigitalRuby.RainMaker;
using UnityEngine;

public class RainController : MonoBehaviour
{

    private RainScript2D rainController => GetComponent<RainScript2D>();

    [Range(0.0f, 1.0f)]
    [SerializeField] private float instensity;
    [SerializeField] private float targetInstensity;


    [SerializeField] private float chngerate = 0.5f;
    [SerializeField] private float minValue = .2f;
    [SerializeField] private float maxValue = .49f;


    [SerializeField] private float chanceToRain = 40;
    [SerializeField] private float rainChekCoolDown;
    private float rainCheckTimer;

    bool canChangeInstensity;



    // Update is called once per frame
    void Update()
    {
        rainCheckTimer -= Time.deltaTime; 
        rainController.RainIntensity = instensity;

        CheckForRain();


        if (Input.GetKeyDown(KeyCode.R))
        {
            canChangeInstensity = true;
        }


        if (canChangeInstensity)
        {
            ChangeInstensity();
        }

    }



    private void CheckForRain()
    {
        if (rainCheckTimer < 0)
        {
            rainCheckTimer = rainChekCoolDown;
            canChangeInstensity = true;

            if (Random.Range(0, 100) < chanceToRain)
            {
                targetInstensity = Random.Range(minValue, maxValue);
            }


            else
            {
                targetInstensity = 0;

            }
        }
    }


    private void ChangeInstensity()
    {
        if (instensity < targetInstensity)
        {
            instensity += chngerate * Time.deltaTime;

            if (instensity >= targetInstensity)
            {
                instensity = targetInstensity;
                canChangeInstensity = false;

            }

        }

        if (instensity > targetInstensity)
        {
            instensity -= chngerate * Time.deltaTime;

            if (instensity <= targetInstensity)
            {
                instensity = targetInstensity;
                canChangeInstensity = false;
            }
        }
    }




}
