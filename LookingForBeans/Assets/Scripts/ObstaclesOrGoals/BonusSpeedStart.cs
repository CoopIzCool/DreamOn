using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpeedStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<FridgeCollision>().Reset();
    }


}
