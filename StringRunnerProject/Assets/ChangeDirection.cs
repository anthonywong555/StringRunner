using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDirection : MonoBehaviour
{
    public int direction = -1;
    public float speed = 1.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player;
        if (player = other.GetComponent<Player>())
        {
            player.direction = direction;
            player.speed = speed;
        }
    }
}