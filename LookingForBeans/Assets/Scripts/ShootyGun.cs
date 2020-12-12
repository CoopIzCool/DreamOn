using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootyGun : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private GameObject gunBody;

    [SerializeField]
    private GameObject target;

    [SerializeField]
    [Range(0.1f, 1.0f)]
    private float fireInterval;

    [SerializeField]
    [Range(0.1f, 2.0f)]
    private float bulletDeathTime;

    [SerializeField]
    [Range(1.0f, 10.0f)]
    private float bulletSpeed;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnBullet());
    }

    private void Update()
    {
        if (target != null)
        {
            gunBody.transform.forward = gunBody.transform.position - target.transform.position;
        }
    }

    IEnumerator SpawnBullet()
    {
        yield return new WaitForSeconds(fireInterval);
        var newBullet = Instantiate(bullet);
        newBullet.transform.forward = -gunBody.transform.forward;
        newBullet.AddComponent<Bullet>().SetParams(bulletSpeed, bulletDeathTime);
        StartCoroutine(SpawnBullet());
    }
}
