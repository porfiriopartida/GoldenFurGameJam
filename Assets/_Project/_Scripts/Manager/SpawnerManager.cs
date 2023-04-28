using GoldenFur.Common;
using GoldenFur.ScriptableObjects;
using UnityEngine;

namespace GoldenFur.Manager
{
    public class SpawnerManager : Singleton<SpawnerManager>
    {
        public FloorFragmentsDatabase floorFragmentsDatabase;
        public void SpawnFragment(Transform fragmentTriggerGround, float fragmentLength)
        {
            //Find random fragment
            var fragments = floorFragmentsDatabase.floorFragmentPrefabs;
            var randomIndex = Random.Range(0, fragments.Length);
            
            //Calculate next Z Pos based on current length
            var refPos = fragmentTriggerGround.position;
            var randomSpawnPosition = new Vector3(refPos.x, refPos.y, refPos.z + fragmentLength);
            
            var newFloor = Instantiate(fragments[randomIndex], randomSpawnPosition, Quaternion.identity, LevelSceneManager.Instance.fragmentStorage);
            // newFloor.name = "Fragment";
            Debug.Log("Spawning new floor");
        }
    }
}