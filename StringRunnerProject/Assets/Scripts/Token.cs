using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour {
    private Vector3 basePosition;

    private void Start()
    {
        basePosition = transform.position;
    }

    private void Update()
    {
        transform.Rotate(0, 0, 25 * Time.deltaTime);
        transform.position = basePosition + Vector3.up * Mathf.Sin(Time.time * 2) * 0.125f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            other.SendMessage("AddToken");
            Destroy(gameObject);
        }
    }
}
