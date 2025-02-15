using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public static GameOver Instance;

    public GameObject panelGO;

    [SerializeField]
    private GameObject lobby;
    [SerializeField]
    private GameObject retry;

    [SerializeField]
    private TMP_Text monedas;
    [SerializeField]
    private TMP_Text distancia;

    [SerializeField]
    private IntValue scoreValue;
    [SerializeField]
    private IntValue distanceValue;
    
    public int valorRevivir;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }else
        {
            Destroy(this);
        }
        
    }

    private void Start()
    {
        retry.SetActive(false);//inicio poniendo el boton de revivir desactivado, por si no consigue las monedas suficientes
    }

    public void ShowGameOver()//se llama cuando no tienes mas vidas
    {
        panelGO.SetActive(true);//muestra el panel del Game Over
        
        //muestra valores de monedas y distancia conseguidas esa partida
        monedas.text = "Monedas: " + scoreValue.runtimeValue;
        distancia.text = "Distancia: " + distanceValue.runtimeValue;

       // print("* " + ResetValues.Instance.canRevive);
        if(ResetValues.Instance.canRevive)//si el booleano que permite mostrar el boton de revivir
        {
            //print("a " + ResetValues.Instance.canRevive);
            if (scoreValue.runtimeValue >= valorRevivir)//si las monedas conseguidas son mas que las necesarias
            {
                retry.SetActive(true);//se activa el boton
            }
            //print(ResetValues.Instance.canRevive);
        }
    }
}
