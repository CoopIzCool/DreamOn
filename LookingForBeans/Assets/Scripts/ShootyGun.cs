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
    private float fireInterval = 1.0f;

    [SerializeField]
    [Range(0.1f, 2.0f)]
    private float bulletDeathTime = 2.0f;

    [SerializeField]
    [Range(1.0f, 10.0f)]
    private float bulletSpeed = 10.0f;

    [SerializeField]
    private Material absorberMaterial;
    [SerializeField]
    private GameObject absorbObject;
    private Color absorbDefaultColor;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnBullet());
        absorbDefaultColor = new Color(221 / 255.0f, 130 / 255.0f, 120 / 255.0f, 1);
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
        newBullet.transform.position = bullet.transform.position;
        newBullet.transform.forward = -gunBody.transform.forward;
        newBullet.AddComponent<Bullet>().SetParams(bulletSpeed, bulletDeathTime, BulletAbsorbAction, BulletNotAbsorbAction);
        StartCoroutine(SpawnBullet());
    }

    private void BulletAbsorbAction()
    {
        absorbObject.SetActive(false);
        absorberMaterial.color = Color.green;
    }

    private void BulletNotAbsorbAction()
    {
        absorbObject.SetActive(true);
        absorberMaterial.color = absorbDefaultColor;
    }

    private void OnApplicationQuit()
    {
        absorberMaterial.color = absorbDefaultColor;
    }
}
