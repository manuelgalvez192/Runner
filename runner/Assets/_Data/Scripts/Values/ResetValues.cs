using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResetValues : MonoBehaviour
{
    public static ResetValues Instance;

    [SerializeField] private IntValue health;
    [SerializeField] private IntValue distanceValue;
    [SerializeField] private IntValue scoreValue;
    [SerializeField] private Image RepSlider;
    [SerializeField] private GameObject panelCorazon;

    public bool canRevive = false;

    public GameEvent updateCoin;
    public GameEvent updateDistance;
    public GameEvent updateLife;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void Start()
    {
        canRevive = true; //alse Resetea la variable a false

        // Resetea valores
        health.runtimeValue = health.intValue;
        distanceValue.runtimeValue = distanceValue.intValue;
        scoreValue.runtimeValue = scoreValue.intValue;
        RepSlider.fillAmount = 0;

        // Debugging
        Debug.Log($"Health Reset: {health.runtimeValue}");
        Debug.Log($"Distance Reset: {distanceValue.runtimeValue}");
        Debug.Log($"Score Reset: {scoreValue.runtimeValue}");

        // Dispara los eventos
        updateCoin?.Raise();
        updateDistance?.Raise();
        updateLife?.Raise();
    }
}