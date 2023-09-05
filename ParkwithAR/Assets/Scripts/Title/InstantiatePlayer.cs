using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

//PUNのコールバックを受け取れるようにする為のMonoBehaviourPunCallbacks
public class InstantiatePlayer : MonoBehaviourPunCallbacks
{
    public TMP_Text statusText;
    private const int MaxPlayerPerRoom = 2;
    
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    // Start is called before the first frame update
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    //マッチングボタンが押された時の処理
    public void OnMatchingButtonClicked()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    //Photonのコールバック
    public override void OnConnectedToMaster()
    {
        Debug.Log("マスターに繋ぎました。");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"{cause}の理由で繋げませんでした。");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("ルームを作成します。");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = MaxPlayerPerRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("ルームに参加しました");
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        if (playerCount != MaxPlayerPerRoom)
        {
            statusText.text = PhotonNetwork.CurrentRoom.PlayerCount + "/" + MaxPlayerPerRoom + "人のプレイヤーが参加中...";
        }
        else
        {
            statusText.text = "マッチング完了。ゲームを開始します。";
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == MaxPlayerPerRoom)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
                statusText.text = "マッチング完了。ゲームを開始します。";
                
                PhotonNetwork.IsMessageQueueRunning = false;

                SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
            }

        }
    }
}