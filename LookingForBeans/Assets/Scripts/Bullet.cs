using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Fields

    private float speed;
    private float deathTime;

    #endregion

    public void SetParams(float speed, float deathTime)
    {
        this.speed = speed;
        this.deathTime = deathTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathTimer());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }
}
