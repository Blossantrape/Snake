using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreWindow : MonoBehaviour
{
    private TextMeshProUGUI _scoreText;
    
    private void Awake() {
        _scoreText = transform.Find("_scoreText").GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        _scoreText.text = GameHandler.GetScore().ToString();
    }
}
