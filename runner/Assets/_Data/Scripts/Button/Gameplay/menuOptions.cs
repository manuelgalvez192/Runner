using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuOptions : MonoBehaviour
{
    public static menuOptions Instance;

    [SerializeField]
    private float durationScale;

    public GameObject menuOpciones;

    public GameObject panelOpciones;

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

    // Start is called before the first frame update
    void Start()
    {
        panelOpciones.SetActive(false);//lo inicio a false para que no se vea
    }

    public void ClickMenu()
    {
        panelOpciones.SetActive(true);//cuando hago click en el boton de arriba a la izquierda lo muestro
        Time.timeScale = 0;//detengo el tiempo
        panelOpciones.transform.DOScale(new Vector3(3.5f, 3.5f, 3.5f), durationScale).SetRelative(true).SetUpdate(true);//aplico la animacion pedida en el ejercicio
        if(panelOpciones.transform.localScale.x > 3.5f)
        {
            //para si se hace click varias veces en el boton de opciones no se aumente de mas la escala del boton
            //otra solucion era ocultar el boton que llama a este panel mientras se muestra
            panelOpciones.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
        }
    }
}
