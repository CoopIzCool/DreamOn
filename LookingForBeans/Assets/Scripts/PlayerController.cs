using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
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
    #endregion Properties
    private void Start()
    {
        currentWayPoint = 0;
        continueMovement = true;
        launchCount = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (continueMovement)
        {
            Debug.Log(currentWayPoint);
            float step = speeds[currentWayPoint] * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, wayPoints[currentWayPoint].transform.position, step);

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
    }
}
