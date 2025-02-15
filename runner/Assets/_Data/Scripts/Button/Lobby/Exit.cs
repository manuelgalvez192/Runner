using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public void ExitGame()//salir del juego
    {
        print("sale del juego");
        Application.Quit();
    }
}
