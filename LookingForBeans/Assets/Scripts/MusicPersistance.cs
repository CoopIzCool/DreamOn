using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPersistance : MonoBehaviour
{
    private static MusicPersistance instance = null;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //if()
    }
}
