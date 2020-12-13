using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HazardCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("GameOver");
        }
        else if(collision.gameObject.tag == "Interact")
        {
            Destroy(collision.gameObject);
        }
    }
}
