using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Fields
    #region
    public Animator cameraAnimator;
    public GameObject howToMenu;
    public GameObject tutorialMenu;

    public GameObject tutorialCircle;
    public Text objectTag;

    float currentTime;
    public float toHowTransitionTime;
    public float tutorialTransitionTime;

    bool howReadyToTrans;
    bool tutorialToTrans;

    float currentRotation;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        cameraAnimator.SetBool("ToOption", false);
        cameraAnimator.SetBool("ToMain", false);
        cameraAnimator.SetBool("ToTutorial", false);
        howToMenu.SetActive(false);

        currentTime = 0.0f;

        howReadyToTrans = false;

        currentRotation = tutorialCircle.transform.rotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        //Makes sure that the menu pops up before the transition is over
        if(howReadyToTrans)
        {
            currentTime += Time.deltaTime;

            if(currentTime > toHowTransitionTime)
            {
                currentTime = 0;
                howToMenu.SetActive(true);
                howReadyToTrans = false;
            }
        }

        if(tutorialToTrans)
        {
            currentTime += Time.deltaTime;
            if(currentTime > tutorialTransitionTime)
            {
                currentTime = 0;
                tutorialMenu.SetActive(true);
                tutorialToTrans = false;
            }
        }

        //Updates the the rotation of the tutorial circle
        if(tutorialCircle.transform.rotation.eulerAngles.y != currentRotation)
        {
            Quaternion target = Quaternion.Euler(0, currentRotation, 0);
            tutorialCircle.transform.rotation = Quaternion.Slerp(tutorialCircle.transform.rotation, target, Time.deltaTime * 5.0f);
        }

        UpdateObjectTags();
    }

    /// <summary>
    /// Starts the player on the first level
    /// </summary>
    public void Play()
    {
        SceneManager.LoadScene("Level1");
    }

    /// <summary>
    /// Closes the application
    /// </summary>
    public void Exit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Sends the player to the How To Play Menu
    /// </summary>
    public void ToHowToPlay()
    {
        cameraAnimator.SetBool("ToOption", true);
        cameraAnimator.SetBool("ToMain", false);
        cameraAnimator.SetBool("ToTutorial", false);
        howReadyToTrans = true;
        tutorialMenu.SetActive(false);

    }

    /// <summary>
    /// Sends the player to the Main Menu
    /// </summary>
    public void ToMain()
    {
        cameraAnimator.SetBool("ToOption", false);
        cameraAnimator.SetBool("ToMain", true);
        cameraAnimator.SetBool("ToTutorial", false);
        howToMenu.SetActive(false);
        tutorialMenu.SetActive(false);
    }

    public void ToTutorial()
    {
        cameraAnimator.SetBool("ToOption", false);
        cameraAnimator.SetBool("ToMain", false);
        cameraAnimator.SetBool("ToTutorial", true);
        howToMenu.SetActive(false);
        tutorialToTrans = true;
    }

    public void ReturnToOptions()
    {
        cameraAnimator.SetBool("ToOption", true);
        cameraAnimator.SetBool("ToMain", false);
        cameraAnimator.SetBool("ToTutorial", false);
        howReadyToTrans = true;
        tutorialMenu.SetActive(false);
    }

    public void RotateLeft()
    {
        currentRotation -= 90;
    }

    public void RotateRight()
    {
        currentRotation += 90;
    }

    public void UpdateObjectTags()
    {
        //Ensure the current rotation is within a detectable range
        float detectRotate = currentRotation;
        while(detectRotate > 360)
        {
            detectRotate -= 360;
        }

        while(detectRotate < 0)
        {
            detectRotate += 360;
        }

        //Move Objects
        if (detectRotate == 135)
            objectTag.text = "Move Objects";
        //Slide Objects
        else if(detectRotate == 225)
            objectTag.text = "Slide Objects";
        //Rotate Objects
        else if(detectRotate == 45)
            objectTag.text = "Rotate Objects";
        //Press Objects
        else if(detectRotate == 315)
            objectTag.text = "Press Objects";
    }

}
