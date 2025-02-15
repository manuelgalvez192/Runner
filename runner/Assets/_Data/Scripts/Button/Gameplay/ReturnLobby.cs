using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnLobby : MonoBehaviour
{
    [SerializeField]
    private int numEscena;
    [SerializeField]
    private GameObject gameOverPanel;

    public void ClickReturnLobby()//boton para regresar al lobby inicial
    {
        gameOverPanel.SetActive(false);//oculto el panel game over
        menuOptions.Instance.panelOpciones.SetActive(false);//y el menu de opciones, para que si vuelve a jugar no empiecen visibles
        Time.timeScale = 1;//devuelvo velocidad al juego
        SceneManager.LoadScene(numEscena);//cargo escena del lobby
    }
}
