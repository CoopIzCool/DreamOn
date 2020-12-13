using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private string target;
    public string currentScene;
    public float waitTime;
    public GameObject music;
    public bool isLevel;
    public static bool isPaused;
    [SerializeField]
    private GameObject pauseMenuUI;
    #endregion Fields

    #region Properties
    public string Target
    {
        get
        {
            return target;
        }
        set
        {
            target = value;
        }
    }
    #endregion Properties

    #region Methods

    //methods to go directly to other scenes TBA
    #region Level Directory
    private void Start()
    {
        //This actually sets the current scene because
        //clicking the button will somehow skip the assignment of currentscene IDK
        currentScene = currentScene;

        //If this is a level, enable pause implementation and set the player preferences to this level
        if (isLevel)
        {
            isPaused = false;
            pauseMenuUI.SetActive(false);
            PlayerPrefs.SetString("PrevLevel", currentScene);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && isLevel)
        {
            if(isPaused)
            {
                Resume();
            }
            else
            {
                pauseGame();
            }
        }
    }
    //go to the next level
    public void NextScene()
    {
       // if (target == "FINAL_SCENE" && currentScene == "MainMenu")
            //StartCoroutine(TransitionMusic(target));
        SceneManager.LoadScene(target);
    }

    public void GoToScene(string scene)
    {
        Debug.Log("Scene: " + scene);
        Debug.Log("Current Scene: " + currentScene);
        if (scene == "FINAL_SCENE" && currentScene == "MainMenu")
            StartCoroutine(TransitionMusic(scene));
        else
            SceneManager.LoadScene(scene);
    }

    //go to menu
    public void mainMenu()
    {

    }

    public void quitGame()
    {
        Application.Quit();
        //GameObject musicPlayer = GameObject.FindGameObjectWithTag("Music");
        //musicPlayer.GetComponent<MusicPlayer>().StopMusic();
        //
        //SceneManager.LoadScene("Main Menu");
    }

    IEnumerator TransitionMusic(string scene)
    {
        Debug.Log("HIIIIII");
        music.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(scene);
    }

    public void retryLevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("PrevLevel"));
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
    }

    public void pauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        isPaused = true;
    }
    #endregion Level Directory
    #endregion Methods
}
