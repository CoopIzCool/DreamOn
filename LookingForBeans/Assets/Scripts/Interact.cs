using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Interact : MonoBehaviour
{
    #region Fields
    //Variables

    //Interact Types
    public bool move;
    public bool slide;
    public bool rotate;

    bool selected;

    //Rotation
    float angle = 0.0f;
    public float angleStep;
    float smooth = 5.0f;
 
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

        gameObject.layer = 2;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "Ground")
            {
                Vector3 mousePos = hit.point;

                if (move)
                {
                    mousePos.y += (gameObject.transform.localScale.y / 2);
                    gameObject.transform.position = mousePos;
                }

                else if (rotate)
                {

                    //Player Input
                    if (Input.GetKey(KeyCode.A))
                        angle += angleStep;

                    if (Input.GetKey(KeyCode.D))

                        angle -= angleStep;

                    Debug.Log(angle);

                    Quaternion target = Quaternion.Euler(0, angle, 0);

                    transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
                }
            }
            
            

        }

        gameObject.layer = 0;
        //Determine how object moves based on what type it is
        
    }
}
