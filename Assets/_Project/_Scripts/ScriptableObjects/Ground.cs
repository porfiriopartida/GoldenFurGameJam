using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public GameObject[] Floor;
    public Vector3 playerPosition;
    float timer = 0f;


    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 2)
        {
            int randomIndex = Random.Range(0, Floor.Length);

            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            Vector3 randomSpawnPosition = new Vector3(1,-1, playerPosition.z + 200);

            Instantiate(Floor[randomIndex], randomSpawnPosition, Quaternion.identity);
            timer = 0;
        }
        if (timer < 2)
        {
            Destroy(Floor[1]);
        }
    }
}