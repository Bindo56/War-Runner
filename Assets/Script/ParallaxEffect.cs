using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private GameObject cam;

    [SerializeField] private float parallaxEffect;

    private float lenght;
    private float xPosition;
    void Start()
    {
        cam = GameObject.Find("Player");

        lenght = GetComponent<SpriteRenderer>().bounds.size.x;

        xPosition = transform.position.x;

    }

    // Update is called once per frame
    void Update()
    {
        float distanceMoved = cam.transform.position.x * (1 - parallaxEffect);

        float distanceToMove = cam.transform.position.x * parallaxEffect;

        transform.position = new Vector3(xPosition + distanceMoved, transform.position.y);

        if(distanceMoved > xPosition + lenght)
        {
            xPosition = xPosition + lenght;
        }
    }
}
