using System.Collections;
using System.Collections.Generic;
using GoldenFur.Character;
using GoldenFur.Manager;
using UnityEngine;

namespace GoldenFur
{
    public class Tutorial : MonoBehaviour
    {
        public GameObject[] popUps;
        private int popUpIndex;
        public GameObject SpawnStars;
        public float waitTime = 2f;
        //public Player player;

        void Start(){
          //  player.zSpeed=0f;
            ScoreManager.Instance.active= false;
            SpawnStars.SetActive(false);
        }

        void Update(){
            // Debug.Log("Tutorial.Update");
            if(popUpIndex == 0){
                popUps[0].SetActive(true);
                if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)){
                    popUpIndex++;
                    popUps[0].SetActive(false);
                    popUps[1].SetActive(true);
                }
            } else if(popUpIndex == 1){
                if(Input.GetKeyDown(KeyCode.S)){
                    popUpIndex++;
                    popUps[1].SetActive(false);
                    popUps[2].SetActive(true);
                }
            }else if(popUpIndex == 2){
                if(Input.GetKeyDown(KeyCode.W)){
                    popUpIndex++;
                    ScoreManager.Instance.active= true;
                    popUps[2].SetActive(false);
                    popUps[3].SetActive(true);
                    SpawnStars.SetActive(true);
                    
                }
            }else if(popUpIndex == 3){
                if(waitTime<= 0){
                    popUps[3].SetActive(false);
                    gameObject.SetActive(false);
                    return;
                }else{
                    waitTime -= Time.deltaTime;
                }
            }
    
        }
    }
}
