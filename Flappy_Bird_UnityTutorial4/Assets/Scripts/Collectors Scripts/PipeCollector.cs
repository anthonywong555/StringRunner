using UnityEngine;
using System.Collections;

public class PipeCollector : MonoBehaviour
{
    private GameObject[] pipeHolders;
    private float distance = 2.8f; //How far apart our pipes will be
    private float lastPipesX; //The last x spot of our last pipe
    private float pipeMin = -1.5f; //The lowest height of our pipe on y
    private float pipeMax = 2.4f; //The max hight of our pipe on y

    void Awake()
    {
        pipeHolders = GameObject.FindGameObjectsWithTag("PipeHolder"); //Grab all game objects in scene with this tag

        for(int i = 0; i < pipeHolders.Length; i++)
        { //Here we are setting all the pipes at random y positions so that they are a bit of a challenge to jump through
            Vector3 temp = pipeHolders[i].transform.position;
            temp.y = Random.Range(pipeMin, pipeMax);
            pipeHolders[i].transform.position = temp;
        }

        lastPipesX = pipeHolders[0].transform.position.x;

        for (int i = 1; i < pipeHolders.Length; i++)
        { //Here we are figuring out the x position of the last pipe and setting the lastPipesX varable to be that value
            if(lastPipesX < pipeHolders[i].transform.position.x)
            {
                lastPipesX = pipeHolders[i].transform.position.x;
            }
        }
    }

	void OnTriggerEnter2D(Collider2D target)
    {
        //So if target is the pipe holder
        if(target.tag == "PipeHolder")
        {
            Vector3 temp = target.transform.position;
			temp.x += lastPipesX + distance; //Here we are setting the new x position
            temp.y = Random.Range(pipeMin, pipeMax); //Here we are setting the new y position
            target.transform.position = temp; //We have set the new position of the pipes
            //lastPipesX = temp.x; //This is now the furthest x so we need to record it
        }
    }
}
