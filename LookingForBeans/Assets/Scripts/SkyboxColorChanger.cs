using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxColorChanger : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private Color[] colors;

    private Camera camera;
    private int currentColorIndex = 0;
    // Used for the color lerp
    private float t = 0;

    [SerializeField]
    private float cycleSpeed = 0.1f;

    [SerializeField]
    private Material fogMat;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        camera = this.GetComponent<Camera>();
        camera.backgroundColor = colors[currentColorIndex];
        fogMat.color = new Color(camera.backgroundColor.r, camera.backgroundColor.g, camera.backgroundColor.b, 1);
        if (colors != null) StartCoroutine(CycleColors());
    }

    IEnumerator CycleColors()
    {
        yield return new WaitForSeconds(cycleSpeed);
        t += cycleSpeed / 10;

        int nextColorIndex = (currentColorIndex + 1) % colors.Length;
        camera.backgroundColor = Color.Lerp(colors[currentColorIndex], colors[nextColorIndex], t);

        // if t is satisfactorily close to 1
        if (t > .95)
        {
            t = 0;
            currentColorIndex++;
        }

        fogMat.color = new Color(camera.backgroundColor.r, camera.backgroundColor.g, camera.backgroundColor.b, 1);

        // Cycle some more
        StartCoroutine(CycleColors());
    }
}
