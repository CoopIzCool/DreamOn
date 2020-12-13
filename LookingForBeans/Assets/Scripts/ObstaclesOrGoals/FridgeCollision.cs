using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeCollision : MonoBehaviour
{
    [SerializeField]
    private SceneTransition sceneManager;
    [SerializeField]
    private bool speedUp;
    private static int speedLevel = 1;
    [SerializeField]
    private bool speedReset;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (speedUp)
            {
                speedLevel++;
                PlayerPrefs.SetInt("Speedcrement", speedLevel);
            }
            sceneManager.NextScene();
        }
    }

    public void Reset()
    {
        PlayerPrefs.SetInt("Speedcrement", 1);
        speedLevel = 1;
    }
}
