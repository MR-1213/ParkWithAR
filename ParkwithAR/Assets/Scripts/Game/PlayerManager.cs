using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Photon.Pun;
using ExitGames.Client.Photon.StructWrapping;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private SpeedController speedController;
    [SerializeField] private UpdateRole updateRole;
    private JoyStick_Move stickMove;
    private JoyStickCam stickCam;

    private float speed = 3f;
    private bool isGameOver = false;
    private bool status = true;

    private void Start() 
    {
        stickMove = GameObject.Find("CommonCanvas").GetComponent<JoyStick_Move>();
        stickCam = GameObject.Find("CommonCanvas").GetComponent<JoyStickCam>();

        if(!photonView.IsMine)
        {
            //自分以外のカメラは無効にする
            GetComponentInChildren<Camera>().enabled = false;
            //AudioListenerを無効にする
            GetComponentInChildren<AudioListener>().enabled = false;
        }
    }

    private void Update()
    {
        if(photonView.IsMine)
        {
            //スピードを設定
            speed = speedController.playerSpeed;

            //動く方向を変数に格納
            Vector3 moveDire = ((transform.forward * stickMove.joyStickPosY) + (transform.right * stickMove.joyStickPosX)).normalized;

            //ポジションを更新
            transform.position += moveDire * speed * Time.deltaTime;

            //回転を更新する
            transform.rotation = Quaternion.Euler(stickCam.rotY, stickCam.rotX, 0);
        }

        if(GamePlayManager.Instance.isGameOver && !this.isGameOver)
        {
            this.isGameOver = true;
            string hideWinMessage = photonView.IsMine ? GamePlayManager.Instance.GetHideWinMessage() : "";
            Debug.Log("hideWinMessage: " + hideWinMessage);

            //Hide勝利メッセージを表示
            updateRole.ShowWinMessage(hideWinMessage);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //自分のオブジェクトのActorNumberを取得
        int myActorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        if(collision.gameObject.GetComponent<PhotonView>() == null)
        {
            return;
        }
        Debug.Log("myActorNumber: " + myActorNumber);

        //当たったオブジェクトのActorNumberを取得
        int otherActorNumber = collision.gameObject.GetComponent<PhotonView>().Owner.ActorNumber;
        Debug.Log("otherActorNumber: " + otherActorNumber);

        if(!GamePlayManager.Instance.playerRoles.ContainsKey(myActorNumber) || !GamePlayManager.Instance.playerRoles.ContainsKey(otherActorNumber))
        {
            return;
        }

        //当たったオブジェクトの役割を取得して、SeekとHideの場合はゲーム終了
        if (GamePlayManager.Instance.playerRoles[otherActorNumber] != GamePlayManager.Instance.playerRoles[myActorNumber] && !this.isGameOver)
        {
            //Seek側の勝利
            this.isGameOver = true;
            string seekWinMessage = photonView.IsMine ? GamePlayManager.Instance.GetSeekWinMessage() : "";
            Debug.Log("seekWinMessage: " + seekWinMessage);

            //Seek勝利メッセージを表示
            updateRole.ShowWinMessage(seekWinMessage);
        }
    }

}
