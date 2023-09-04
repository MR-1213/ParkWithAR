using UnityEngine;
using System.Collections;

public class StopTrap : MonoBehaviour
{
    private bool isTrapActive = true;
    private float stopDuration = 3f; // 逃走者を停止させる秒数

    private void OnTriggerEnter(Collider other)
    {
        if (isTrapActive && other.CompareTag("逃走者")) // 逃走者のタグに合わせて調整
        {
            StartCoroutine(StopRunner(other.GetComponent<Rigidbody>()));
        }
    }

    private IEnumerator StopRunner(Rigidbody runnerRigidbody)
    {
        isTrapActive = false;

        // 逃走者の速度をゼロに設定
        runnerRigidbody.velocity = Vector3.zero;
        runnerRigidbody.angularVelocity = Vector3.zero;

        // 指定秒数待機
        yield return new WaitForSeconds(stopDuration);

        // 待機後、逃走者を再開
        isTrapActive = true;
    }
}
