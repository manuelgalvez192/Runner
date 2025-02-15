using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthUIController : MonoBehaviour {
    public IntValue playerHealth;


    private Image radialImage;
    private TextMeshProUGUI lifesText;
    private int initHealth;
    private void Start() {
        lifesText = GetComponent<TextMeshProUGUI>();
        UpdatePlayerLifes();
    }

    public void UpdatePlayerLifes() {
        lifesText.text = playerHealth.runtimeValue.ToString();
    }
}
