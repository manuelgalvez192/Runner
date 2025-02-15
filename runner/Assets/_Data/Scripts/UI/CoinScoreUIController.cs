using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinScoreUIController : MonoBehaviour {
    public IntValue score;

    private TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start() {
        text = GetComponent<TextMeshProUGUI>();
        UpdateCoinScore();
    }

    public void UpdateCoinScore() {
        text.text = score.runtimeValue.ToString("000000");
    }
}
