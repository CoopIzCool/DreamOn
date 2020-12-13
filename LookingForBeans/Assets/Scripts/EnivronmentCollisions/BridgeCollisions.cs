using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeCollisions : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().FreeFall = true;
        }
    }
}
