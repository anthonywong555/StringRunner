using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public void StartGame()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().direction = 1;
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Update is called once per frame
    void Update () {
		if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("GameScene");
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartGame();
        }
    }
}
