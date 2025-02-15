using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resume : MonoBehaviour
{

    public void ResumeGame()//metodo para volver al juego desde el menu pausa
    {
        Countdown.Instance.LanzarPanel();//inicio la cuenta atras
        PlayerMovement.Instance.ResumePlayerMovement();//reseteo el movimiento del player
        
        menuOptions.Instance.panelOpciones.SetActive(false);//oculto el menu de pausa
        Time.timeScale = 1;//devuelvo el tiempo al juego
        
    }
}
