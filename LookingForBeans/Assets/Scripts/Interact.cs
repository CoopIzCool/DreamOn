﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;

public class Interact : MonoBehaviour
{
    #region Fields
    //Variables

    //Main Menu only
    [Header("Main Menu Only")]
    public bool onMainMenu;

    //Interact Types
    [Header("Interaction Type")]
    public bool move;
    public bool slide;
    public bool rotate;
    public bool pressed;

    bool selected;

    //Rotation
    [Header("Rotation Settings")]
    public float angleStep;
    float angle = 0.0f;   
    float smooth = 15.0f;

    //Slide
    [Header("Slide Settings")]
    public string axis;
    public float range;

    //Pressed
    [Header("Pressed Settings")]
    public Animator pressAnimator;

    //Materials
    [Header("Materials")]
    public Material interactDefaultMat;
    public Material hoverMat;
    public Material selectMat;

    //Raycast
    Ray ray;
    RaycastHit hit;
    public bool hitByRay;

    //Other Game Objects
    public GameObject ground;

    //launching fields
    public bool beingLaunched;
    private Vector3[] launchPoints = new Vector3[3];
    [SerializeField]
    private float launchHeight;
    private float launchCount;
    #endregion
    #region Properties
    public float Height
    {
        set { launchHeight = value; }
    }
    #endregion Properties

    // Start is called before the first frame update
    void Start()
    {
        if(!onMainMenu)
            gameObject.GetComponent<MeshRenderer>().material = interactDefaultMat;

        hitByRay = false;
        selected = false;

        angle = gameObject.transform.rotation.eulerAngles.y;
        beingLaunched = false;
    }

    // Update is called once per frame
    void Update()
    {

        //Test if the cursor is hovering over an interactable objects if the object is not being launched
        if (!beingLaunched)
        {
            if (!onMainMenu)
                UpdateMaterial();
            else
                gameObject.GetComponent<MeshRenderer>().material = interactDefaultMat;

            if (!onMainMenu)
                UpdateSelect();

            if (selected || onMainMenu)
                ChooseMovementType();
        }
        else
        {
            if (launchCount < 1.0f)
            {
                launchCount += 1.0f * Time.deltaTime;
                Vector3 m1 = Vector3.Lerp(launchPoints[0], launchPoints[1], launchCount);
                Vector3 m2 = Vector3.Lerp(launchPoints[1], launchPoints[2], launchCount);
                transform.position = Vector3.Lerp(m1, m2, launchCount);
            }
            else
            {
                beingLaunched = false;
            }
        }
    }

    /// <summary>
    /// Updates material based how an objected is being interacted
    /// </summary>
    void UpdateMaterial()
    {
        if (hitByRay)
            gameObject.GetComponent<MeshRenderer>().material = hoverMat;
        else
            gameObject.GetComponent<MeshRenderer>().material = interactDefaultMat;
    }

    /// <summary>
    /// Ensures that a selected object remains selected even if the cursor leaves the hitbox
    /// </summary>
    void UpdateSelect()
    {
        if (hitByRay && Input.GetMouseButton(0))
            selected = true;

        if (selected && Input.GetMouseButtonUp(0))
            selected = false;

        //Updates the button animator
        if(pressed)
        {
            if(pressAnimator != null)
                pressAnimator.SetBool("IsPressed", selected);
        }
    }
    void ChooseMovementType()
    {
        //Player selects the object
        if(!onMainMenu)
            gameObject.GetComponent<MeshRenderer>().material = selectMat;

        //Creates a raycast that will ignore the object
        gameObject.layer = 2;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "Ground" && hit.normal == hit.transform.up)
            {
                Vector3 mousePos = hit.point;

                //Object freely moves and rotates on the ground
                if (move)
                {
                    //Freeform movement
                    mousePos.y = gameObject.transform.position.y;
                    gameObject.transform.position = mousePos;

                    //Rotation
                    RotateObject();
                }

                else if (slide)
                {
                    //Determine which axis is being used
                    if(axis == "x")
                    {
                        float yPos = gameObject.transform.position.y;
                        float zPos = gameObject.transform.position.z;

                        mousePos.x = Mathf.Clamp(mousePos.x, -range, range);
                        mousePos.y = yPos;
                        mousePos.z = zPos;

                        gameObject.transform.position = mousePos;

                    }
                    else if(axis == "z")
                    {
                        float xPos = gameObject.transform.position.x;
                        float yPos = gameObject.transform.position.y;
                        
                        mousePos.x = xPos;
                        mousePos.y = yPos;
                        mousePos.z = Mathf.Clamp(mousePos.z, -range, range);

                        gameObject.transform.position = mousePos;
                    }

                }
            }                     
        }

        gameObject.layer = 0;
        //Determine how object moves based on what type it is

            //Object will only rotate around its center
        if(rotate)
            RotateObject();


    }

    void RotateObject()
    {
        //Player Input
        if (Input.GetKey(KeyCode.Q))
            angle += angleStep;

        if (Input.GetKey(KeyCode.E))
            angle -= angleStep;

        Quaternion target = Quaternion.Euler(0, angle, 0);

        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
    }

    public void SetPoints(Vector3 endPoint)
    {
        launchPoints[0] = transform.position;
        launchPoints[2] = endPoint;
        launchPoints[1] = launchPoints[0] + ((launchPoints[2] - launchPoints[0]) / 2) + (Vector3.up * launchHeight);
    }
}
