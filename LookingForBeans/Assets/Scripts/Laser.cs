using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    #region Fields

    private GameObject laserContainer;

    [SerializeField]
    private Material laserMaterial;
    [SerializeField]
    private Material laserSolutionMaterial;

    private Func<bool> castRaysFunc;

    private bool solved = false;
    private bool playerHit = false;

    private const int maxLasers = 1000;
    private int currentLasers = 0;
    private bool maxLasersReached = false;

    private Color unsolvedColor;

    #endregion

    private void Start()
    {
        laserContainer = new GameObject();
        laserContainer.name = "Laser Beam";
        LineRenderer laser = laserContainer.AddComponent<LineRenderer>();
        laser.startWidth = 0.05f;
        laser.endWidth = 0.05f;
        laser.numCapVertices = 10;
        laser.numCornerVertices = 10;
        laser.startColor = Color.red;
        laser.endColor = Color.red;
        laser.material = laserMaterial;

        unsolvedColor = laserSolutionMaterial.color;

        // Using this rather than update to make sure the stack is not modified across 
        // multiple frames. This would cause garbage/old lasers to stick around.
        castRaysFunc = () =>
        {
            solved = false;
            playerHit = false;
            laserSolutionMaterial.color = unsolvedColor;

            // Recursively cast the laser
            Ray laserRay = new Ray(transform.position, transform.forward);
            List<Ray> rays = Cast(laserRay);

            // Populate the linerenderer's vertices
            List<Vector3> vertices = new List<Vector3>();
            for (int i = 0; i < rays.Count; i++)
            {
                vertices.Add(rays[i].origin);
            }

            // Extend the laser past the final origin if the puzzle is not solved and the laser didn't just end
            if (!solved && !maxLasersReached && !playerHit) vertices.Add(rays[rays.Count - 1].GetPoint(30));

            // If the puzzle is solved, let the player know by recoloring the solution block
            if (solved) laserSolutionMaterial.color = Color.green;

            laser.positionCount = vertices.Count;
            laser.SetPositions(vertices.ToArray());

            return true;
        };

        StartCoroutine(CastRays());
    }

    private void OnApplicationQuit()
    {
        laserSolutionMaterial.color = unsolvedColor;
    }

    private IEnumerator CastRays()
    {
        currentLasers = 0;
        yield return new WaitUntil(castRaysFunc);
        StartCoroutine(CastRays());
    }

    private List<Ray> Cast(Ray castedRay, List<Ray> rays = null)
    {
        currentLasers++;
        if (rays == null) rays = new List<Ray>();
        rays.Add(castedRay);

        maxLasersReached = currentLasers > maxLasers;

        RaycastHit hit;
        if (Physics.Raycast(castedRay, out hit) && !solved && !maxLasersReached && !playerHit)
        {
            if (hit.collider.CompareTag("LaserSolution")) solved = true;
            if (hit.collider.CompareTag("Player")) playerHit = true;

            Vector3 reflectDirection = Vector3.Reflect(castedRay.direction, hit.normal);
            
            // Don't allow pointing the beam in up/down directions
            if (reflectDirection.y != 0)
            {
                reflectDirection.y = 0;
                reflectDirection.Normalize();
            }

            Ray nextRay = new Ray(hit.point, reflectDirection);
            return Cast(nextRay, rays);
        }
        else
        {
            return rays;
        }
    }
}
