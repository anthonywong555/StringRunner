using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    public static float offsetX;

	// Use this for initialization
	void Start()
    {
	
	}
	
	// Update is called once per frame
	void Update()
    {
	    //If we have a bird in the current game
        if(BirdScript.instance != null)
        {
            //If our bird is alive
            if(BirdScript.instance.isAlive)
            {
                MoveTheCamera();
            }
        }
	}

    //This will be used to move our camera
    void MoveTheCamera()
    {
        Vector3 temp = transform.position; //Get current position of camera
        temp.x = BirdScript.instance.GetPositionX() + offsetX; //Set where we want the camera to be in a moment
        transform.position = temp; //Set the new location fo camera
    }
}
