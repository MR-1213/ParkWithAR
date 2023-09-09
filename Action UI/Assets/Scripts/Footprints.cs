using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footprints : MonoBehaviour
{
    private Renderer renderer;
    private Color saveColor;
    private bool isInvokeScheduled = false; // フラグを追加

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        saveColor = renderer.material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトの情報を取得
        GameObject collidedObject = collision.gameObject;

        // 衝突したオブジェクトのRendererコンポーネントを取得
        Renderer renderer2 = collidedObject.GetComponent<Renderer>();

        if (renderer2 != null)
        {
            // 衝突したオブジェクトの色情報を取得
            Color objectColor = renderer2.material.color;

            // 色情報をコンソールに出力
            Debug.Log("Collided Object Color: " + objectColor);
            if (collision.gameObject.CompareTag("逃走者"))
            {
                renderer.material.color = objectColor;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Invoke("ChangeSaveColor", 3.0f); // 3秒後にChangeSaveColorを実行する
    }

    private void ChangeSaveColor()
    {
        // objectColorをsaveColorに変更する
        renderer.material.color = saveColor;
        isInvokeScheduled = false; // Invokeが実行されたらフラグをリセット
    }
}
