using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletEnemy : MonoBehaviour
{

   Player  player;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.GetComponent<Player>();
            if (player == null)
            {
                Debug.LogError("Lund LElE");
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") && player.playerUnlocked)
        {
            Damage();
            
        }
       

    }
    public void Damage()
    {
        


        player.BloodFX.Play();
        if (player.extraLife == true)
        {
            Debug.Log("dead");
            player.knockback();

        }

        else
            StartCoroutine(Die());
    }

    private IEnumerator Die() //Courotine 
    {
        AudioManager.instance.PlaySFX(4);

        player.isDead = true;
        player.canBeKnock = false;
        player.rb.velocity = player.knockBackDir;
        player.anim.SetBool("isDead", true);

        Time.timeScale = .6f;

        yield return new WaitForSeconds(1);
        player.rb.velocity = new Vector2(0, 0);
        GameManager.Instance.GameEnded();


    }
}
