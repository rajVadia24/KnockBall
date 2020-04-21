using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector3 spawnPosition;
    Quaternion spawnRotation;
    private GameManager gmInstance;
    private void Start()
    {
        gmInstance = GameManager.instance;
        spawnPosition = gmInstance.PointToShoot.position;
        spawnRotation = gmInstance.PointToShoot.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            RepositionBall();
        }
    }

    private void RepositionBall()
    {
        this.gameObject.SetActive(false);
        transform.position = spawnPosition;
        transform.rotation = spawnRotation;
        this.gameObject.SetActive(true);
        StartCoroutine(readyToShoot());
    }

    IEnumerator readyToShoot()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        gmInstance.ReadyToShoot = true;
    }


}

