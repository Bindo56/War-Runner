using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private  Transform[] Levelpart;
    [SerializeField] private Vector3 nextPartPosition;

    [SerializeField] private float distanceToSpawn;
    [SerializeField] private float distanceToDelete;
    [SerializeField] private Transform player;

    

    // Update is called once per frame
    void Update()
    {
        DeletePlatform();
        GeneratePlatform();

    }

    private void GeneratePlatform()
    {
        while (Vector2.Distance(player.transform.position,nextPartPosition) < distanceToSpawn)
        {

            Transform part = Levelpart[Random.Range(0, Levelpart.Length)];


            Vector2 newPosition = new Vector2(nextPartPosition.x - part.Find("Startpoint").position.x, 0);

            Transform newPart = Instantiate(part, newPosition, transform.rotation, transform);

            nextPartPosition = newPart.Find("EndPoint").position;
        }
    }

    private void DeletePlatform()
    {
        if(transform.childCount > 0)
        {
            Transform partToDelete = transform.GetChild(0);

            if (Vector2.Distance(player.transform.position, partToDelete.transform.position) > distanceToDelete)
            {
                Destroy(partToDelete.gameObject);
            }
        }
    }

}
