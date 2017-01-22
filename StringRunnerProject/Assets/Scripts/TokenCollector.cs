using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TokenCollector : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;

    public void Start()
    {
        UpdateUI();
    }

    public void AddToken()
    {
        ++score;

        GameObject.Find("Main Camera/StarSound").GetComponent<StarSounds>().PlayStarSound();

        UpdateUI();
    }

    public void UpdateUI()
    {
        Application.ExternalCall("OnTokenCollected", new object[] { score });
    }
}
