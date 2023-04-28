using System.Collections.Generic;
using GoldenFur.Manager;
using UnityEngine;

namespace GoldenFur.Clothing
{
    public class ClothesChanger : MonoBehaviour
    {
        public List<GameObject> clothingList;
        public string prefix;

        public int indice;

        private void Start()
        {
            ParsePlayerPrefs();
        }

        private void ParsePlayerPrefs()
        {
            for (var index = 0; index < clothingList.Count; index++)
            {
                var key = prefix + PlayerPrefKeys.Clothing + index;
                var currentClothing = clothingList[index];
                var storedValue = PlayerPrefs.GetInt(key, -1);
                Debug.Log($"Parsing {key} | {storedValue}");
                switch (storedValue)
                {
                    case 0:
                        currentClothing.SetActive(false);
                        break;
                    case 1:
                        currentClothing.SetActive(true);
                        break;
                }
            }
        }

        public void OnPressNext()
        {
            //Se presiona el boton se incremente el indice
            indice = indice + 1;
            ChangeClothing(indice);

            if (indice > clothingList.Count - 1)
            {
                indice = 0;
                ChangeClothing(indice);
            }
        }

        public void OnPressPrevious()
        {
            //Se presiona el boton se incremente el indice
            indice = indice - 1;
            ChangeClothing(indice);

            if (indice < 0)
            {
                indice = clothingList.Count - 1;
                ChangeClothing(indice);
            }
        }


        public void ChangeClothing(int indice)
        {
            string key = prefix + PlayerPrefKeys.Clothing;
            for (int i = 0; i < clothingList.Count; i++)
            {
                if (i == indice)
                {
                    clothingList[i].SetActive(true);
                    PlayerPrefs.SetInt(key + i, 1);
                }
                else
                {
                    clothingList[i].SetActive(false);
                    PlayerPrefs.SetInt(key + i, 0);
                }
            }
        }
    }
}

