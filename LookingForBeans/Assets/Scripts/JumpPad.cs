using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    #region Fields
    private int count;
    [SerializeField]
    private Vector3 endPoint;
    [SerializeField]
    private float height;
    #endregion Fields
    private void Start()
    {
        count = 30;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            count--;
            if (count == 0)
            {
                other.gameObject.GetComponent<PlayerMovement>().Height = height;
                other.gameObject.GetComponent<PlayerMovement>().SetPoints(endPoint);
                other.gameObject.GetComponent<PlayerMovement>().Continue = false;
            }
        }
    }
}
