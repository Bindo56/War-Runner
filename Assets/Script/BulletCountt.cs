using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCountt : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            
            AudioManager.instance.PlaySFX(6);
            GameManager.Instance.Bullet = 25;
            Destroy(gameObject);
        }

    }
}
