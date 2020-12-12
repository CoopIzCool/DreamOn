using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastInteraction : MonoBehaviour
{
    //Fields
    Ray ray;
    RaycastHit hit;

    GameObject previousInteractObject;

    // Start is called before the first frame update
    void Start()
    {
        previousInteractObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        TestRayCast();
    }

    void TestRayCast()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "Interact")
            {
                previousInteractObject = hit.collider.gameObject;
                previousInteractObject.GetComponent<Interact>().hitByRay = true;
            }
            else
            {
                if (previousInteractObject != null)
                    previousInteractObject.GetComponent<Interact>().hitByRay = false;
            }
        }
    }
}
