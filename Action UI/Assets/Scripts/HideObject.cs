using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCoroutine : MonoBehaviour
{
    public GameObject hideObject; // 隠すオブジェクトをアタッチ
    private bool isHidden = true; // オブジェクトが非表示状態かどうかを追跡するフラグ


    private void Start(IEnumerator coroutine)
    {
        GlobalCoroutine component = hideObject.AddComponent <GlobalCoroutine> ();
        if (component != null) {
            // 1分ごとにShowObjectメソッドを呼び出す
            hideObject.SetActive(false);
            Debug.Log("コルーチン呼び出し");
            component.StartCoroutine (component.ShowObject (coroutine));
        }
    }
    IEnumerator ShowObject(IEnumerator coroutine)
    {
        while (true)
        {
            Debug.Log("待機開始");
            yield return new WaitForSeconds(5f);
            Debug.Log("待機終了");
            hideObject.SetActive(true);
        }
    }
}
