using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private List<GameObject> wayPoints;
    [SerializeField]
    private List<float> speeds;
    private int currentWayPoint;
    private bool continueMovement;
    private Vector3[] launchPoints = new Vector3[3];
    [SerializeField]
    private float launchHeight;
    private float launchCount;
    private bool freeFall;
    #endregion Fields
    #region Properites
    public bool Continue
    {
        set { continueMovement = value; }
    }
    public float Height
    {
        set { launchHeight = value; }
    }
    public bool FreeFall
    {
        get { return freeFall; }
        set { freeFall = value; }
    }
    #endregion Properties
    private void Start()
    {
        currentWayPoint = 0;
        continueMovement = true;
        launchCount = 0;
        freeFall = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (continueMovement)
        {
            float step = speeds[currentWayPoint] * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, wayPoints[currentWayPoint].transform.position, step);
            Vector3 relativePos = wayPoints[currentWayPoint].transform.position - transform.position;
            Quaternion rotate = Quaternion.LookRotation(relativePos, Vector3.up);
            rotate.eulerAngles = new Vector3(-90, rotate.eulerAngles.y + 90, rotate.eulerAngles.z);
            transform.rotation = rotate;
        }
        else if(!freeFall)
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
                continueMovement = true;
            }
        }
    }

    public void IncrementPoint()
    {
        Debug.Log("Next Waypoint");
        currentWayPoint++;
    }

    public void SetPoints(Vector3 endPoint)
    {
        launchPoints[0] = transform.position;
        launchPoints[2] = endPoint;
        launchPoints[1] = launchPoints[0] + ((launchPoints[2] - launchPoints[0]) / 2) + (Vector3.up * launchHeight);
        launchCount = 0.0f;
    }
}
