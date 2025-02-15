using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    [SerializeField]
    private int numEscena;

    public void CambiarEscena()//empezar juego desde el lobby
    {
        SceneManager.LoadScene(numEscena);
    }
}
