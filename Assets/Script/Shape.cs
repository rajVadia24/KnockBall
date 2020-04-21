using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public bool hasFallan = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            hasFallan = true;
            GameManager.instance.CheckAllObjectIsGrounded();
        }
    }
}
