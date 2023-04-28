using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GoldenFur.ScriptableObjects
{
    public class GroundCreator : MonoBehaviour
    {
        //Floor prefabs
        public GameObject[] floorPrefabs;
        
        //Floor prefabs
        public Vector2 yRange; //.65 ~ 2.54
        public Vector3 xPositions;
        
        public GameObject coinPrefab;
        
        //Floor prefabs
        public GameObject[] obstaclesPrefab;
        
        public float length;
    
        //Parent ground.
        public Transform groundReference;
        
        //Parent Storage
        public Transform coinStorage;
        public Transform groundStorage;
        public Transform obstaclesStorage;
        
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SpawnNextFloor();
            }
        }

        public void SpawnNextFloor()
        {
            var randomIndex = Random.Range(0, floorPrefabs.Length);
            var refPos = groundReference.position;
            var randomSpawnPosition = new Vector3(refPos.x,refPos.y, refPos.z + length);

            var newFloor = Instantiate(floorPrefabs[randomIndex], randomSpawnPosition, Quaternion.identity, groundStorage);
            Debug.Log("Spawning new floor");

            SpawnCoins(newFloor);
        }

        private void SpawnCoins(GameObject newFloor)
        {
            var refPos = newFloor.transform.position;

            var randomX = xPositions[Random.Range(0, 3)]; 
            var randomY = Random.Range(yRange.x, yRange.y); 
            
            var randomSpawnPosition = new Vector3(randomX, randomY, refPos.z + length);
            Instantiate(coinPrefab, randomSpawnPosition, Quaternion.identity, coinStorage);
        }

        private void SpawnObstacle()
        {
            var randomIndex = Random.Range(0, floorPrefabs.Length);
            var refPos = groundReference.position;
            var randomSpawnPosition = new Vector3(refPos.x,refPos.y, refPos.z + length);
            Instantiate(floorPrefabs[randomIndex], randomSpawnPosition, Quaternion.identity, obstaclesStorage);
        }
    }
}