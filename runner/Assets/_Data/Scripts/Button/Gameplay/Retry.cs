using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retry : MonoBehaviour
{
    public static Retry Instance;

    [SerializeField]
    private GameEvent updateCoin;
    [SerializeField]
    private GameEvent updateLife;
    [SerializeField]
    private IntValue scoreValue;
    [SerializeField]
    private IntValue health;
    [SerializeField]
    private int vidasRevivir;
    [SerializeField]
    private GameObject panelGO;
    [SerializeField]
    private GameObject retryButton;



    public void Revivir()//boton para revivir
    {
       
        scoreValue.runtimeValue -= GameOver.Instance.valorRevivir;//resto las moendas necesarias del total del juegador
        health.runtimeValue = vidasRevivir;//le pongo la vida inicial
        panelGO.SetActive(false);//quito el panel de Game Over
        PlayerMovement.Instance.ResetPlayerMovement();//reseteo el movimiento del jugador
        Countdown.Instance.LanzarPanel();//lanzo la cuenta atras

        ResetValues.Instance.canRevive = false;//se pone a false el booleano que deja revivir porque solo se puede una vez por partida

        updateCoin.Raise();//aplico eventos para actualizar los valores
        updateLife.Raise();

        retryButton.SetActive(false);//oculto el boton de revivir
    }
}
