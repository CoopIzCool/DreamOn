using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    #region Fields

    private GameObject laserContainer;

    [SerializeField]
    private Material material;

    private Func<bool> castRaysFunc;

    private bool solved = false;
    
    #endregion

    private void Start()
    {
        laserContainer = new GameObject();
        LineRenderer laser;
        laser = laserContainer.AddComponent<LineRenderer>();
        laser.startWidth = 0.05f;
        laser.endWidth = 0.05f;
        laser.numCapVertices = 10;
        laser.numCornerVertices = 10;
        laser.startColor = Color.red;
        laser.endColor = Color.red;
        laser.material = material;

        // Using this rather than update to make sure the stack is not modified across 
        // multiple frames. This would cause garbage/old lasers to stick around.
        castRaysFunc = () =>
        {
            solved = false;

            // Recursively cast the laser
            Ray laserRay = new Ray(transform.position, transform.forward);
            List<Ray> rays = Cast(laserRay);

            // Populate the linerenderer's vertices
            List<Vector3> vertices = new List<Vector3>();
            for (int i = 0; i < rays.Count; i++)
            {
                vertices.Add(rays[i].origin);
            }

            // Extend the laser past the final origin if the puzzle is not solved
            if (!solved) vertices.Add(rays[rays.Count - 1].GetPoint(30));

            laser.positionCount = vertices.Count;
            laser.SetPositions(vertices.ToArray());

            return true;
        };

        StartCoroutine(CastRays());
    }

    private IEnumerator CastRays()
    {
        yield return new WaitUntil(castRaysFunc);
        StartCoroutine(CastRays());
    }

    private List<Ray> Cast(Ray castedRay, List<Ray> rays = null)
    {
        if (rays == null) rays = new List<Ray>();
        rays.Add(castedRay);

        RaycastHit hit;
        if (Physics.Raycast(castedRay, out hit) && !solved)
        {
            if (hit.collider.CompareTag("LaserSolution")) solved = true;

            Vector3 reflectDirection = Vector3.Reflect(castedRay.direction, hit.normal);
            Ray nextRay = new Ray(hit.point, reflectDirection);
            return Cast(nextRay, rays);
        }
        else
        {
            return rays;
        }
    }
}
