using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //Fields
    #region
    public Animator cameraAnimator;
    public GameObject howToMenu;
    public GameObject tutorialMenu;

    public GameObject tutorialCircle;

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

        if(tutorialCircle.transform.rotation.eulerAngles.y != currentRotation)
        {
            Debug.Log(currentRotation);
            //tutorialCircle.transform.rotation = Quaternion.Euler(0, currentRotation, 0);
            Quaternion target = Quaternion.Euler(0, currentRotation, 0);
            tutorialCircle.transform.rotation = Quaternion.Slerp(tutorialCircle.transform.rotation, target, Time.deltaTime * 5.0f);
        }
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
        currentRotation += 90;
    }

    public void RotateRight()
    {
        currentRotation -= 90;
    }

}
