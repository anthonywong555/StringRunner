using UnityEngine;
using System.Collections;

public class BGCollector : MonoBehaviour
{
    private GameObject[] backgrounds; //An array that holds all of our backgrounds
    private GameObject[] grounds; //An array that holds all of our grounds
    private float lastBGX; //Last background x position
    private float lastGroundX; //Last ground x position

    void Awake()
    {
        backgrounds = GameObject.FindGameObjectsWithTag("Background");
        grounds = GameObject.FindGameObjectsWithTag("Ground");

        lastBGX = backgrounds[0].transform.position.x; //equel the first in our array
        lastGroundX = grounds[0].transform.position.x; //equel the first in our array

        for (int i = 1; i < backgrounds.Length; i++)
        {
            if(lastBGX < backgrounds[i].transform.position.x)
            {
                lastBGX = backgrounds[i].transform.position.x;
            }

            if (lastGroundX < grounds[i].transform.position.x)
            {
                lastGroundX = grounds[i].transform.position.x;
            }
        }
    }
	
	void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Background")
        {
            Vector3 temp = target.transform.position;
            float width = ((BoxCollider2D)target).size.x; //Get size x of the collider
            temp.x = lastBGX + width;
            target.transform.position = temp;
            lastBGX = temp.x;
        }
        else if (target.tag == "Ground")
        {
            Vector3 temp = target.transform.position;
            float width = ((BoxCollider2D)target).size.x; //Get size x of the collider
            temp.x = lastGroundX + width;
            target.transform.position = temp;
            lastGroundX = temp.x;
        }
    }
}
