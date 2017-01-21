using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TokenCollector : MonoBehaviour {
    public int score = 0;
    public Text scoreText;

    public void Start()
    {
        scoreText = GameObject.Find("Canvas/ScoreText").GetComponent<Text>();

        UpdateUI();

    }

    public void AddToken()
    {
        ++score;

        UpdateUI();
    }

    public void UpdateUI()
    {
        scoreText.text = "Score: " + score;
    }
}
