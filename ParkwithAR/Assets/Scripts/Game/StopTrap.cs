using UnityEngine;
using System.Collections;

public class StopTrap : MonoBehaviour
{
    private bool isTrapActive = true;
    public float stopDuration = 3f; // 逃走者を停止させる秒数

    private void OnTriggerEnter(Collider other)
    {
        if (isTrapActive)
        {
            StartCoroutine(StopRunner(other.GetComponent<PlayerManager>()));
        }
    }

    private IEnumerator StopRunner(PlayerManager playerManager)
    {
        isTrapActive = false;
        Debug.Log("逃走者を停止");

        // 逃走者の速度をゼロに設定
        playerManager.enabled = false;

        // 指定秒数待機
        yield return new WaitForSeconds(stopDuration);

        Debug.Log("逃走者を再開");
        playerManager.enabled = true;

        // 待機後、逃走者を再開
        isTrapActive = true;
    }
}
