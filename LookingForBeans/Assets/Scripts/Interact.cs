using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    #region Fields
    //Variables

    //Interact Types
    public bool move;
    public bool slide;
    public bool rotate;

    bool selected;

    //Materials
    public Material interactDefaultMat;
    public Material hoverMat;
    public Material selectMat;

    //Raycast
    Ray ray;
    RaycastHit hit;
    public bool hitByRay;

    //Other Game Objects
    public GameObject ground;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material = interactDefaultMat;
        hitByRay = false;
        selected = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Test if the cursor is hovering over an interactable objects
        UpdateMaterial();

        UpdateSelect();

        if(selected)
            ChooseMovementType();
    }

    void UpdateMaterial()
    {
        if (hitByRay)
            gameObject.GetComponent<MeshRenderer>().material = hoverMat;
        else
            gameObject.GetComponent<MeshRenderer>().material = interactDefaultMat;
    }

    void UpdateSelect()
    {
        if (hitByRay && Input.GetMouseButton(0))
            selected = true;

        if (selected && Input.GetMouseButtonUp(0))
            selected = false;
    }
    void ChooseMovementType()
    {
        //Player selects the object
        gameObject.GetComponent<MeshRenderer>().material = selectMat;

        //Determine how object moves based on what type it is
        if (move)
        {
            gameObject.layer = 2;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Ground")
                {
                    Vector3 mousePos = hit.point;
                    mousePos.y += (gameObject.transform.localScale.y / 2);
                    gameObject.transform.position = mousePos;
                }

            }

            gameObject.layer = 0;
        }
    }
}
