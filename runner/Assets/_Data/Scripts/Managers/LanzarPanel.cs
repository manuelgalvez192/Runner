using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzarPanel : MonoBehaviour
{
    private void Start()//lanzo el panel de countdown al iniciar
    {//para que cuando entre el jugador salga la cuenta atras
        Countdown.Instance.Start();
    }
}
