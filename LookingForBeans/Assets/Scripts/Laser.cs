using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    #region Fields

    private Stack<GameObject> lasers = new Stack<GameObject>();

    [SerializeField]
    private Material material;

    private Func<bool> castRaysFunc;

    #endregion

    private void Start()
    {
        // Using this rather than update to make sure the stack is not modified across 
        // multiple frames. This would cause garbage/old lasers to stick around.
        castRaysFunc = () =>
        {
            while (lasers.Count > 0)
            {
                Destroy(lasers.Pop());
            }

            Ray laserRay = CreateLaserObject(transform.position, transform.forward);
            Cast(laserRay);

            return true;
        };

        StartCoroutine(CastRays());
    }

    private IEnumerator CastRays()
    {
        yield return new WaitUntil(castRaysFunc);
        StartCoroutine(CastRays());
    }

    private void Cast(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // Change the laser to end at the hit position
            lasers.Peek()
                .GetComponent<LineRenderer>()
                .SetPositions(new[] { lasers.Peek().transform.position, hit.point });

            Vector3 reflectDirection = Vector3.Reflect(ray.direction, hit.normal);

            Ray nextRay = CreateLaserObject(hit.point, reflectDirection);
            Cast(nextRay);
        }
    }

    private Ray CreateLaserObject(Vector3 startingPoint, Vector3 dir)
    {
        // Init a new gameobject to house the laser
        lasers.Push(new GameObject());
        lasers.Peek().name = "Laser";
        lasers.Peek().transform.position = startingPoint;
        lasers.Peek().transform.forward = dir;

        // Make the line renderer and point it in the right direction
        LineRenderer laser = MakeLineRenderer(lasers.Peek());
        Ray laserRay = GetRayFromObject(lasers.Peek());
        SetLineRendererVerticesByRay(laser, laserRay);

        return laserRay;
    }

    private LineRenderer MakeLineRenderer(GameObject obj)
    {
        LineRenderer laser;
        laser = obj.AddComponent<LineRenderer>();
        laser.startWidth = 0.05f;
        laser.endWidth = 0.05f;
        laser.numCapVertices = 10;
        laser.startColor = Color.red;
        laser.endColor = Color.red;
        laser.material = material;

        return laser;
    }

    private void SetLineRendererVerticesByRay(LineRenderer laser, Ray laserRay)
    {
        var linePositions = new[] { laserRay.origin, laserRay.GetPoint(30) };
        laser.SetPositions(linePositions);
    }

    private Ray GetRayFromObject(GameObject obj)
    {
        return new Ray(obj.transform.position, obj.transform.forward);
    }
}
