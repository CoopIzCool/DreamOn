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

    float currentTime;
    public float toHowTransitionTime;

    bool howReadyToTrans;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        cameraAnimator.SetBool("ToOption", false);
        cameraAnimator.SetBool("ToMain", false);
        howToMenu.SetActive(false);

        currentTime = 0.0f;

        howReadyToTrans = false;
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
        howReadyToTrans = true;
        
    }

    /// <summary>
    /// Sends the player to the Main Menu
    /// </summary>
    public void ToMain()
    {
        cameraAnimator.SetBool("ToOption", false);
        cameraAnimator.SetBool("ToMain", true);
        howToMenu.SetActive(false);
    }
}
