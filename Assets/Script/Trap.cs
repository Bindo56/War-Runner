using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class Trap : MonoBehaviour
{
    [SerializeField] protected float chanceToSpawn;

    protected virtual void Start()
    {
        bool canSpawn = chanceToSpawn >= Random.Range(0, 100);
        

        if (canSpawn)
        {
            Destroy(gameObject);
        }
    }

     

    protected virtual void OnTriggerEnter2D(Collider2D collision) //using protected to give asses to another script
    {
        if (collision.GetComponent<Player>() != null) if (collision.GetComponent<Player>() != null)
            collision.GetComponent<Player>().Damage();
    }
}
