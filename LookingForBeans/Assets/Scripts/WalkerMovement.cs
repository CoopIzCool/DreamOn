using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerMovement : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private Vector3 direction;
    public float yaw;
    public float pitch;
    public float roll;
    public float x;
    public float y;
    public float z;
    #endregion Fields
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetFromTransform(transform);
        direction = Vector3.forward * Time.deltaTime;
    }

    public void SetFromTransform(Transform t)
    {
        pitch = t.eulerAngles.x;
        yaw = t.eulerAngles.y;
        roll = t.eulerAngles.z;
        x = t.position.x;
        y = t.position.y;
        z = t.position.z;
    }

    public void Translate(Vector3 translation)
    {
        Vector3 rotatedTranslation = Quaternion.Euler(pitch, yaw, roll) * translation;

        x += rotatedTranslation.x;
        y += rotatedTranslation.y;
        z += rotatedTranslation.z;
    }



    public void UpdateTransform(Transform t)
    {
        t.eulerAngles = new Vector3(pitch, yaw, roll);
        t.position = new Vector3(x, y, z);
    }
}
