using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour
{
    public GameObject catchUI; // ゲームオーバーUIのGameObjectをアタッチ
    public Text catchText; // ゲームオーバー時に表示するテキスト

    public int count = 0;
    private bool isCatch = false; // ゲームオーバー状態を管理

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("逃走者") && !isCatch)
        {
            // 逃走者に衝突した場合
            isCatch = true;
            ShowGameOverUI();
        }
    }

    void ShowGameOverUI()
    {
        //UIを表示し、テキストを設定
        catchUI.SetActive(true);
        count += 1;
        string message = string.Format("あなたは{0}人捕まえました。", count);
        catchText.text = message; // テキストをカスタマイズ可能
    }

    public void RestartGame()
    {
        // ゲームを再起動
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}

