// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class cambiarRopaTorso : MonoBehaviour
// {
//     public List<GameObject> camisas;
//
//     public int indice;
//
//     void Update()
//     {
//         
//     }
//
//     public void OnPressCamisa()
//     {
//         //Se presiona el boton se incremente el indice
//          indice = indice + 1;
//         CambiarPrendaCamisa(indice);
//
//         if (indice > 5)
//         {
//             indice = 0;
//             CambiarPrendaCamisa(indice);
//
//         }
//     }
//
//     public void OnPressCamisaMenos()
//     {
//         //Se presiona el boton se incremente el indice
//         indice = indice - 1;
//         CambiarPrendaCamisa(indice);
//
//         if (indice < 0)
//         {
//             indice = 5;
//             CambiarPrendaCamisa(indice);
//
//         }
//     }
//
//
//     public void CambiarPrendaCamisa(int indice)
//     {
//         for (int i = 0; i < camisas.Count; i++)
//         {
//             if (i == indice)
//             {
//                 camisas[i].SetActive(true);
//             }
//             else
//             {
//                 camisas[i].SetActive(false);
//             }
//         }
//     }
// }
//        
