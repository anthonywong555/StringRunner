using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        player.direction = 0;

        GameObject.Find("Main Camera/Music").GetComponent<Music>().EndMusic();

        Application.ExternalCall("OnWin");
    }
}
