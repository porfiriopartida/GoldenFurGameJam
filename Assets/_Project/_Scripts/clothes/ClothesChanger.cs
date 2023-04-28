using System.Collections.Generic;
using UnityEngine;

namespace GoldenFur.clothes
{
    public class ClothesChanger : MonoBehaviour
    {
        public List<GameObject> clothingList;

        public int indice;

        void Update()
        {

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
            for (int i = 0; i < clothingList.Count; i++)
            {
                if (i == indice)
                {
                    clothingList[i].SetActive(true);
                }
                else
                {
                    clothingList[i].SetActive(false);
                }
            }
        }
    }
}

