using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeCollision : MonoBehaviour
{
    [SerializeField]
    private SceneTransition sceneManager;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            sceneManager.NextScene();
        }
    }
}
