using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    #region Fields

    private float speed;
    private float deathTime;

    private Action absorbAction;
    private Action notAbsorbAction;

    #endregion

    public void SetParams(float speed, float deathTime, Action absorbAction = null, Action notAbsorbAction = null)
    {
        this.speed = speed;
        this.deathTime = deathTime;
        this.absorbAction = absorbAction;
        this.notAbsorbAction = notAbsorbAction;
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
        if (other.CompareTag("Player")) SceneManager.LoadScene("GameOver");

        if (other.CompareTag("BulletAbsorber"))
        {
            absorbAction();
        }
        else
        {
            notAbsorbAction();
        }

        Destroy(gameObject);
    }

    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }
}
