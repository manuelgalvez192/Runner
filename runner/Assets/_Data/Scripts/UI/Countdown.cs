using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public static Countdown Instance;

    public GameObject panel;

    [SerializeField]
    private TMP_Text numeros;

    private int vueltas;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }else if(Instance != null)
        {
            Destroy(this.gameObject);
        }
        
    }

    // Start is called before the first frame update
    public void Start()
    {
        LanzarPanel();
    }

    public void LanzarPanel()
    {
        vueltas = 3;//el numero de vueltas que dara el panel son 3, una por segundo
        panel.SetActive(true);//muestro el panel de la cuenta atras
        StartCoroutine(CuentaAtras());

    }

    private IEnumerator CuentaAtras()
    {
        while (vueltas > 0)//mientras las vueltas sean mayores que 0
        {
            numeros.text = vueltas.ToString();//el numero que se muestra en pantalla es el de la vuelta
            switch (vueltas)//dependiendo de el numero en el que este
            {
                case 3:
                    numeros.color = Color.red;//lo pone rojo
                    break;
                case 2:
                    numeros.color = Color.yellow;//amarillo
                    break;
                case 1:
                    numeros.color = Color.green;//verde
                    break;
                default://por si acaso
                    print("vuelta no valida");
                    break;
            }

            yield return new WaitForSeconds(1f);//espero un segundo por cada vuelta
            vueltas--;//resto uno al numero para que acabe saliendo del switch
        }

        panel.SetActive(false);//al acabar de hacer todos los numeros lo quito
        
    }
}
