using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HazardCollision : MonoBehaviour
{
    public AudioClip gameOverMusic;
    private AudioSource music;

    private void Start()
    {
        music = GameObject.Find("MusicManager").GetComponent<AudioSource>();    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            music.clip = gameOverMusic;
            music.Play();
            SceneManager.LoadScene("GameOver");
        }
        else if (collision.gameObject.tag == "Interact")
        {
            Destroy(collision.gameObject);
        }
    }
}
