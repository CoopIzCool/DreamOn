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
    [SerializeField]
    private Material deactive;
    #endregion Fields
    private void Start()
    {
        count = 30;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Interact")
        {
            count--;
        }
        if (count == 0)
        {
            if (other.tag == "Player")
            {
                other.gameObject.GetComponent<PlayerMovement>().Height = height;
                other.gameObject.GetComponent<PlayerMovement>().SetPoints(endPoint);
                other.gameObject.GetComponent<PlayerMovement>().Continue = false;

            }
            else if (other.tag == "Interact")
            {
                other.gameObject.GetComponent<Interact>().Height = height;
                other.gameObject.GetComponent<Interact>().SetPoints(endPoint);
                other.gameObject.GetComponent<Interact>().beingLaunched = true;
            }
            GetComponent<MeshRenderer>().material = deactive;
        }
    }
}
