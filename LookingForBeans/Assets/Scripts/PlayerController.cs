using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private GameObject Fridge;
    #endregion Fields

    // Update is called once per frame
    void Update()
    {
        GetComponent<NavMeshAgent>().SetDestination(Fridge.transform.position);
    }
}
