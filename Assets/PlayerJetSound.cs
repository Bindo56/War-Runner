using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJetSound : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Sound");
            AudioManager.instance.PlaySFX(7);
            
        }
    }
}
