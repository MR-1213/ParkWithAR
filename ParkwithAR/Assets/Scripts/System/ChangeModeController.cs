using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Photon.Pun;

public class ChangeModeController : MonoBehaviourPunCallbacks
{
    private Camera gameCamera;
    [SerializeField] private Camera arCamera;
    private Image sceneTransitionImage;

    private void Start()
    {
        sceneTransitionImage = GameObject.Find("SceneTransitionPanel").GetComponent<Image>();
    }

    public void ChangeToAR()
    {
        gameCamera = Camera.main;
        gameCamera.gameObject.SetActive(false);

        sceneTransitionImage.DOFade(1f, 0f).OnComplete(() =>
        {
            //自身のプレイヤーオブジェクトを非アクティブにする
            //GamePlayManager.Instance.joinedPlayer.SetActive(false);
            //RPC経由で全員に非アクティブにするように伝える
            //photonView.RPC("InActivePlayer", RpcTarget.Others);

            sceneTransitionImage.DOFade(0f, 1f);
        });
        GamePlayManager.Instance.isAvator = false;
        arCamera.gameObject.SetActive(true);
    }

    [PunRPC]
    private void InActivePlayer()
    {
        Debug.Log(GamePlayManager.Instance.joinedPlayer.name);
        GamePlayManager.Instance.joinedPlayer.SetActive(false);
    }

    public void ChangeToGame()
    {
        arCamera.gameObject.SetActive(false);

        sceneTransitionImage.DOFade(1f, 0f).OnComplete(() =>
        {
            //ルームに入っているプレイヤー全員を取得して、アクティブにする
            //GamePlayManager.Instance.joinedPlayer.SetActive(true);
            //RPC経由で全員にアクティブにするように伝える
            //photonView.RPC("ActivePlayer", RpcTarget.Others);

            sceneTransitionImage.DOFade(0f, 1f);
        });
        GamePlayManager.Instance.isAvator = true;
        gameCamera.gameObject.SetActive(true); 
    }

    [PunRPC]
    private void ActivePlayer()
    {
        GamePlayManager.Instance.joinedPlayer.SetActive(true);
    }
}
