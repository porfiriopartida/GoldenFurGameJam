using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cambiarRopaCabeza : MonoBehaviour
{
    public List<GameObject> ropaCabeza;

    public int indice;

    void Update()
    {

    }

    public void OnPressCabeza()
    {
        //Se presiona el boton se incremente el indice
        indice = indice + 1;
        CambiarPrendaCabeza(indice);

        if (indice > 4)
        {
            indice = 0;
            CambiarPrendaCabeza(indice);

        }
    }

    public void OnPressCabezaMenos()
    {
        //Se presiona el boton se incremente el indice
        indice = indice - 1;
        CambiarPrendaCabeza(indice);

        if (indice < 0)
        {
            indice = 4;
            CambiarPrendaCabeza(indice);

        }
    }


    public void CambiarPrendaCabeza(int indice)
    {
        for (int i = 0; i < ropaCabeza.Count; i++)
        {
            if (i == indice)
            {
                ropaCabeza[i].SetActive(true);
            }
            else
            {
                ropaCabeza[i].SetActive(false);
            }
        }
    }
}

