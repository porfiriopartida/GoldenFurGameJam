// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class cambiarZapatos : MonoBehaviour
// {
//     public List<GameObject> pantalones;
//
//     public int indice;
//
//     void Update()
//     {
//
//     }
//
//     public void OnPressPantalon()
//     {
//         //Se presiona el boton se incremente el indice
//         indice = indice + 1;
//         CambiarPantalones(indice);
//
//         if (indice > 4)
//         {
//             indice = 0;
//             CambiarPantalones(indice);
//         }
//     }
//
//     public void OnPressPantalonsMenos()
//     {
//         //Se presiona el boton se incremente el indice
//         indice = indice - 1;
//         CambiarPantalones(indice);
//
//         if (indice < 0)
//         {
//             indice = 4;
//             CambiarPantalones(indice);
//
//
//         }
//     }
//
//
//     public void CambiarPantalones(int indice)
//     {
//         for (int i = 0; i < pantalones.Count; i++)
//         {
//             if (i == indice)
//             {
//                 pantalones[i].SetActive(true);
//             }
//             else
//             {
//                 pantalones[i].SetActive(false);
//             }
//         }
//     }
// }
//
