using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int direction = 1;
    public float speed = 1.5f;
    public ParticleSystem onDeathParticleSystem;

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * direction, GetComponent<Rigidbody2D>().velocity.y);
        //GetComponent<Rigidbody2D>().AddForce(Vector2.right * 5.0f * Time.deltaTime);
       // transform.Translate(Vector2.right * 1.5f * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            direction *= -1;
        }
    }

    public void Kill()
    {
        ParticleSystem pS = Instantiate(onDeathParticleSystem, transform.position, Quaternion.identity);
        pS.Play();
        Destroy(pS, pS.main.duration);
        Destroy(gameObject);
    }
}
