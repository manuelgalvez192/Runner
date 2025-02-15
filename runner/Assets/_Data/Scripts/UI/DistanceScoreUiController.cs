using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistanceScoreUiController : MonoBehaviour
{
    public IntValue score;

    private TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        UpdateDistanceScore();
    }


    // Update is called once per frame
    public void UpdateDistanceScore()
    {
        
        text.text = score.runtimeValue.ToString("000000");
    }
}
