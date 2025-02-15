using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Runner;

public class TonicosManager : MonoBehaviour
{
    public static TonicosManager Instance;

    [SerializeField]
    private Image RepSliderBackground;
    [SerializeField]
    private Image RepSlider;
    [SerializeField]
    private IntValue health;
    [SerializeField]
    private GameEvent onPlayerLifeChange;

    [SerializeField]
    private GameObject panelX2;

    [SerializeField]
    private GameObject panelX10;

    public bool isSpawneable;
    private bool shine;
    private bool isBoostx2 = false;
    private bool isBoostx10 = false;

    public List<Transform> listaMulti, listaRep, listaRaro;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
        
        isSpawneable = true;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        RepSliderBackground.enabled = false;//inicio poniendo el icono de aumentar la vida invisible
    }

    public void AumentarVida()
    {
        RepSliderBackground.enabled = true;//muestro el icono
        RepSlider.fillAmount += 0.33f;//aumento un slider un 33% cada vez que se llama
        if (RepSlider.fillAmount > 0.9f)//como el 33 nunca llega a ser uno lo compruebo sin mas decimales
        {
            health.runtimeValue++;//aumenta el valor de la vida en uno
            onPlayerLifeChange.Raise();//lo actualizo para mostrarlo
            RepSlider.fillAmount = 0;//vuelvo a iniciar el valor del slider
            RepSliderBackground.enabled = false;//lo vuelvo a ocultar
        }
        
    }

    public void LlamarCorrutina(int op)//switch para llamar corrutinas desde spawn efect
    {
        switch (op)
        {
            case 1:
                StopCoroutine(CooldownX10());//se puede coger un tonico de x10 y luego uno de x2, por lo que paro el de x10
                StartCoroutine(CooldownX2());//lanzo el de x2, para que se quede con el ultimo cogido
                break;
            case 2:
                StartCoroutine(TurnSpawneable());
                break;
            case 3:
                StopCoroutine(SwitchTextX10());
                StartCoroutine(SwitchTextX2());
                break;
            case 4:
                StopCoroutine(CooldownX2());
                StartCoroutine(CooldownX10());
                break;
            case 5:
                StopCoroutine(SwitchTextX2());
                StartCoroutine(SwitchTextX10());
                break;

            default:
                print("no llama ninguna corrutina");
                break;
        }

    }

    private IEnumerator CooldownX2()
    {//el multiplicador se cambia tras 6s que es lo que dura el efecto
        isBoostx2 = true;//se cambia los booleanos para comprobar si se tienen los efectos
        shine = true;
        yield return new WaitForSeconds(6);
        CoinController.multiplicador = 1;
        isBoostx2 = false;//se reinician booleanos
        shine = false;
        panelX2.SetActive(false);//se oculta el panel desde aqui para que el switch no pueda acabar mostrandose, y se quede siempre en la ui
    }

    private IEnumerator TurnSpawneable()
    {//durante 30s se mantiene la opcion de que no se pueda spawnear si se ha cogido otro tonico multiplicador
        isSpawneable = false;
        yield return new WaitForSeconds(30);
        isSpawneable = true;
    }

    private IEnumerator SwitchTextX2()
    {//comprueba si tiene el efecto del x2 con el primer booleando
        while(isBoostx2)//mientras tenga el efecto
        {
            if (shine)//alterna entre mostrar y no mostrar el icono de x2 en pantalla, para que tenga un efecto de loop
            {
                panelX2.SetActive(true);
                shine = false;
                yield return new WaitForSeconds(.7f);
            }
            else
            {
                panelX2.SetActive(false);
                shine = true;
                yield return new WaitForSeconds(.7f);
            }
        }
        
    }


    private IEnumerator CooldownX10()//igual que el x2
    {
        isBoostx10 = true;
        shine = true;
        PlayerMovement.Instance.TurnControll();//para invertir los controles
        
        panelX2.SetActive(false);
        yield return new WaitForSeconds(5);
        CoinController.multiplicador = 1;
        isBoostx10 = false;
        shine = false;
        panelX10.SetActive(false);
        PlayerMovement.Instance.TurnControll();//vuelve a invertir los controles, para dejarlos normal
    }

    private IEnumerator SwitchTextX10()
    {
        while (isBoostx10)
        {
            if (shine)
            {
                panelX10.SetActive(true);
                shine = false;
                yield return new WaitForSeconds(.7f);
            }
            else
            {
                panelX10.SetActive(false);
                shine = true;
                yield return new WaitForSeconds(.7f);
            }
        }

    }

}
