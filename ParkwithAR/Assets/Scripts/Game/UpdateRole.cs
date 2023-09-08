using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using DG.Tweening;
using System.Collections;

public class UpdateRole : MonoBehaviourPunCallbacks
{
    [SerializeField] private Image roleImage;
    [SerializeField] private TMP_Text roleMessageText;

    private CanvasGroup roleCanvasGroup;
    private CanvasGroup roleMessageCanvasGroup;
    private TMP_Text preparationText;
    private Image sceneTransitionImage;
    public TMP_Text debugText;

    private void Start()
    {
        debugText = GameObject.Find("DebugText").GetComponent<TMP_Text>();

        roleCanvasGroup = roleImage.GetComponent<CanvasGroup>();
        roleMessageCanvasGroup = roleMessageText.GetComponent<CanvasGroup>();

        preparationText = GameObject.Find("GamePreparationText").GetComponent<TMP_Text>();
        sceneTransitionImage = GameObject.Find("SceneTransitionPanel").GetComponent<Image>();

        // プレイヤーの役割に基づいてTextを更新
        StartCoroutine(UpdateRoleBasedOnRole());
    }

    private IEnumerator UpdateRoleBasedOnRole()
    {
        // 1秒間画面を暗くしてからフェードイン
        yield return sceneTransitionImage.DOFade(1f, 1f).OnComplete(() =>
        {
            sceneTransitionImage.DOFade(0f, 1f);
        }).WaitForCompletion();

        // 自分のプレイヤーの役割を取得
        Sprite playerRoleImage = null;
        while(true)
        {
            playerRoleImage = photonView.IsMine ? GamePlayManager.Instance.GetPlayerRoleImage() : null;
            if(playerRoleImage != null || !photonView.IsMine)
            {
                break;
            }
            yield return null;
        }
        
        Debug.Log("playerRoleImage: " + playerRoleImage);

        roleCanvasGroup.alpha = 0f;
        roleImage.sprite =  playerRoleImage;

        //自分の役割のメッセージを取得
        string playerRoleMessage = photonView.IsMine ? GamePlayManager.Instance.GetPlayerRoleMessage() : "";
        Debug.Log("playerRoleMessage: " + playerRoleMessage);
        
        //自分の役割を伝えるメッセージを表示する
        roleMessageText.text = playerRoleMessage;
        //準備時間分待機してからメッセージを表示してフェードアウト
        if (playerRoleMessage != "")
        {
            roleMessageCanvasGroup.alpha = 0f;
            roleMessageCanvasGroup.DOFade(1f, 0f).SetDelay(GamePlayManager.Instance.preparationTime).OnComplete(() =>
            {
                preparationText.alpha = 0f;
                roleCanvasGroup.alpha = 1f;
                roleMessageCanvasGroup.DOFade(0f, 3.0f).SetDelay(5.0f);
            });
        }
    }

    public void ShowWinMessage(string message)
    {
        roleMessageCanvasGroup.alpha = 1f;
        roleMessageText.text = message;
    }
    
}
