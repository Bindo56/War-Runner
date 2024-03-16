using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ledgeDetection : MonoBehaviour
{
    [SerializeField] private float radius; 
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private bool canDetected = true;
    

    [SerializeField] private Player player;

    private BoxCollider2D boxCd => GetComponent<BoxCollider2D>();

    private void Start()
    {
        canDetected = true;
        //boxCd = GetComponent<BoxCollider2D>();
    }

    //player cannot grab the ledge if candetected is true 
    private void Update()
    {
        if (canDetected)
        player.ledgeDetected = Physics2D.OverlapCircle(transform.position, radius,whatIsGround); //candetected = false so the player can grab //player.ledgedetected bool from player script

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))  
        {
            canDetected = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(boxCd.bounds.center, boxCd.size,0);

        foreach (var hit in colliders)
        {
            if(hit.gameObject.GetComponent<PlatformColor>() != null) 
            { return; }
        }


        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            canDetected = true; //can detected true player cannot se ledge
        }
    }




    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
