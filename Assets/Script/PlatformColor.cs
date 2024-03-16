using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformColor : MonoBehaviour
{

    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private SpriteRenderer headerSr;


    private void Start()
    {
        headerSr.transform.parent = transform.parent; // to assig the parent of gameobject to main parent of gameobject 
        headerSr.transform.localScale = new Vector2(sr.bounds.size.x, .2f); //size of the  colour changer of headersr
        headerSr.transform.position = new Vector2(transform.position.x , sr.bounds.max.y); //to assign it to top of the platform 

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (GameManager.Instance.colorEntirePlatform)
            {

              headerSr.color = GameManager.Instance.platformColor;
                sr.color = GameManager.Instance.platformColor;
            }

                 headerSr.color = GameManager.Instance.platformColor; //platform color from gamemanager 

        }
                
                
    }
}
