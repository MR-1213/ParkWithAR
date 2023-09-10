using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggerbaloon : MonoBehaviour
{

    public BaloonMovement baloonMovement;

    private void Start() {
        Debug.Log("風船の動きを停止");
        baloonMovement.enabled = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Triggerにeventに入りました");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("風船の動きを開始");
            baloonMovement.enabled = true;
        }
    }

}
