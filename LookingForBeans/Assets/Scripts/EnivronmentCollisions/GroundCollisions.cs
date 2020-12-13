using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollisions : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerMovement>().FreeFall == true)
            {
                collision.gameObject.GetComponent<PlayerMovement>().FreeFall = false;
                collision.gameObject.GetComponent<PlayerMovement>().Continue = true;
            }
        }
    }
}
