using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingTrap : Trap
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform[] movePoint;

    private int i;

    

    protected override void Start()
    {
        base.Start();
        transform.position = movePoint[0].position;
    }




    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint[i].position , moveSpeed * Time.deltaTime);

        if(Vector2.Distance(transform.position, movePoint[i].position) < .25f) //if position of movepoin is less then .25f
        {
            i++;

            if(i >=movePoint.Length)
                i=0;
        }

        if(transform.position.x > movePoint[i].position.x)
            transform.Rotate(new Vector3(0,0,rotationSpeed * Time.deltaTime));

        else 
            transform.Rotate(new Vector3(0,0,-rotationSpeed * Time.deltaTime)); //for left rotation
    }



    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
