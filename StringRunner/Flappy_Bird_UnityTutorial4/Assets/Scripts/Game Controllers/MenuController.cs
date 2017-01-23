using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;

    [SerializeField]
    private GameObject[] birds;

    private bool isGreenBirdUnloacked, isRedBirdUnloacked;

    void Awake()
    {
        MakeInstance();
    }

	// Use this for initialization
	void Start()
    {
        birds[GameController.instance.GetSelectedBird()].SetActive(true);
        CheckIfBirdsAreUnlocked();

    }

    void MakeInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void CheckIfBirdsAreUnlocked()
    {
        if(GameController.instance.IsGreenBirdUnlocked() == 1)
        {
            isGreenBirdUnloacked = true;
        }

        if (GameController.instance.IsRedBirdUnlocked() == 1)
        {
            isRedBirdUnloacked = true;
        }
    }

    public void PlayGame()
    {
        SceneFader.instance.FadeIn("GamePlay");
    }

    public void ChangeBird()
    {
        //If blue bird selected
        if(GameController.instance.GetSelectedBird() == 0)
        {
            //If green bird is unlocked
            if(isGreenBirdUnloacked)
            {
                ActivateDeactivateBird(0, 1);
            }
        }
        else if(GameController.instance.GetSelectedBird() == 1)
        {
            //If green bird is unlocked
            if (isRedBirdUnloacked)
            {
                ActivateDeactivateBird(1, 2);
            }
            else
            {
                ActivateDeactivateBird(1, 0);
            }
        }
        else if(GameController.instance.GetSelectedBird() == 2)
        {
            ActivateDeactivateBird(2, 0);
        }
    }

    void ActivateDeactivateBird(int birdDeactivate, int birdActivate)
    {
        birds[birdDeactivate].SetActive(false); //Deactivate unwanted bird
        GameController.instance.SetSelectedBird(birdActivate); //Index of wanted bird
        birds[GameController.instance.GetSelectedBird()].SetActive(true); //Activate bird
    }
}
