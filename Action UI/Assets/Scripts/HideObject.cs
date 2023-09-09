using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObject : MonoBehaviour
{
    private void Start()
    {
        // 1分ごとにShowPlaneCoroutineを呼び出すコルーチンを開始します。
        // Planeを非表示にする
        SetPlaneVisibility(false);

        Debug.Log("コルーチンを呼び出し");

        // 1分ごとにShowPlaneCoroutineを呼び出すコルーチンを開始します。
        StartCoroutine(ShowPlaneCoroutine());
    }

    private IEnumerator ShowPlaneCoroutine()
    {
        while (true)
        {
            Debug.Log("コルーチンを開始");
            // 1分待機
            yield return new WaitForSeconds(60f);

            Debug.Log("1分経過");

            // Planeを表示する
            SetPlaneVisibility(true);

            // 5秒待機
            yield return new WaitForSeconds(10f);

            // Planeを非表示にする
            SetPlaneVisibility(false);
        }
    }

    private void SetPlaneVisibility(bool isVisible)
    {
        // PlaneのMesh RendererコンポーネントのEnabledプロパティを設定して表示/非表示を切り替える
        MeshRenderer planeRenderer = GetComponent<MeshRenderer>();
        if (planeRenderer != null)
        {
            planeRenderer.enabled = isVisible;
        }
    }
}
