using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCoinSpawner : MonoBehaviour
{
    public GameObject[] myObjects;
    public Vector3 playerPosition;
    float timer = 0f;


    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 2)
        {
                int randomIndex = Random.Range(0, myObjects.Length);

                playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
                Vector3 randomSpawnPosition = new Vector3(Random.Range(0, 2), 0, playerPosition.z + 200);

                Instantiate(myObjects[randomIndex], randomSpawnPosition, Quaternion.identity);
            timer = 0;
        }
        
            if (timer < 2)
            {
                Destroy(myObjects[1]);
            }
        
        
    }
}