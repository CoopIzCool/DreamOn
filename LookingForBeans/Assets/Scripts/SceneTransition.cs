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
    private GameObject music;
    public AudioClip mainMusic;
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
        music = GameObject.Find("MusicManager");
        music.GetComponent<AudioSource>().Play();
        currentScene = currentScene;
        DontDestroyOnLoad(music);
    }

    private void Update()
    {
        
    }
    //go to the next level
    public void NextScene()
    {
        // if (target == "FINAL_SCENE" && currentScene == "MainMenu")
        //StartCoroutine(TransitionMusic(target));
        if (target == "MainMenu")
            music.GetComponent<AudioSource>().clip = mainMusic;
        SceneManager.LoadScene(target);
    }

    public void GoToScene(string scene)
    {
        Debug.Log("Scene: " + scene);
        Debug.Log("Current Scene: " + currentScene);
        if (scene == "FINAL_SCENE" && currentScene == "MainMenu")
            StartCoroutine(TransitionMusic(scene));
        else
        {
            if (scene == "MainMenu")
                music.GetComponent<AudioSource>().clip = mainMusic;
            SceneManager.LoadScene(scene);
        }       
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

    #endregion Level Directory
    #endregion Methods
}
