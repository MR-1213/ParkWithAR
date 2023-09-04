using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI; // ゲームオーバーUIのGameObjectをアタッチ
    public Text gameOverText; // ゲームオーバー時に表示するテキスト

    private bool isGameOver = false; // ゲームオーバー状態を管理

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("鬼") && !isGameOver)
        {
            // 障害物に衝突したらゲームオーバー
            isGameOver = true;
            ShowGameOverUI();
        }
    }

    void ShowGameOverUI()
    {
        // ゲームオーバーUIを表示し、テキストを設定
        gameOverUI.SetActive(true);
        gameOverText.text = "Game Over!"; // テキストをカスタマイズ可能
    }

    public void RestartGame()
    {
        // ゲームを再起動
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
