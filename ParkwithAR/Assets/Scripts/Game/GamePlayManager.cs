using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class GamePlayManager : MonoBehaviourPunCallbacks
{
    public static GamePlayManager Instance;

    public float timeLimit = 180f;
    public float preparationTime = 10f;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private Sprite seekImage;
    [SerializeField] private Sprite hideImage;

    private Dictionary<int, Sprite> playerRoleImages = new Dictionary<int, Sprite>();
    private Dictionary<int, string> playerRoleMessages = new Dictionary<int, string>();
    private TMP_Text debugText;

    private void Awake()
    {
        // シングルトン
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // シリアライズの登録
        SpriteSerializer.Register();
    }

    private void Start()
    {
        PhotonNetwork.IsMessageQueueRunning = true;

        debugText = GameObject.Find("DebugText").GetComponent<TMP_Text>();

        // プレイヤーを生成
        var v = new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(-3f, 3f));
        PhotonNetwork.Instantiate("Player", v, Quaternion.identity);

        //ルームを作成したプレイヤーのみの処理
        if(PhotonNetwork.IsMasterClient)
        {
            //現在のサーバー時刻をゲームの開始時刻に設定
            PhotonNetwork.CurrentRoom.SetStartTime(PhotonNetwork.ServerTimestamp);
            //ルーム内の全員に役割を割り当てる
            AssignRoles();
        }
    }

    private void AssignRoles()
    {
        // ルーム内のプレイヤーのリストを取得
        Player[] players = PhotonNetwork.PlayerList;

        // プレイヤーの数が2人未満の場合は役割を割り当てない
        if (players.Length < 2) return;

        // 一人のプレイヤーに"Seek"の役割、他のプレイヤーに"Hide"の役割を割り当てる
        int seekPlayerIndex = Random.Range(0, players.Length);
        for (int i = 0; i < players.Length; i++)
        {
            int actorNumber = players[i].ActorNumber;
            Sprite roleImage = i == seekPlayerIndex ? seekImage : hideImage;
            string roleMessage = i == seekPlayerIndex ? "あなたは<color=red>Seek</color>です" : "あなたは<color=blue>Hide</color>です";
            
            // プレイヤーと役割を紐付ける
            AssignRoleToPlayer(actorNumber, roleImage);
            //プレイヤーと役割メッセージを紐づける
            AssignRoleMessageToPlayer(actorNumber, roleMessage);

            // 自分のプレイヤーにだけ役割を表示する
            if (players[i].IsLocal)
            {
                UpdateRoleTextBasedOnRole();
                UpdateRoleMessageTextBasedOnRole();
            }
        }
    }

    private void AssignRoleToPlayer(int actorNumber, Sprite roleImage)
    {
        // プレイヤーに役割を割り当てる
        // ローカルプレイヤー以外のプレイヤーにはRPC経由で割り当てる
        if (!playerRoleImages.ContainsKey(actorNumber))
        {
            playerRoleImages.Add(actorNumber, roleImage);

            // 他のクライアントにも役割を通知する
            photonView.RPC("SyncPlayerRole", RpcTarget.Others, actorNumber, roleImage);
        }
    }

    [PunRPC]
    private void SyncPlayerRole(int actorNumber, Sprite roleImage)
    {
        // プレイヤーに役割を割り当てる
        // ローカルプレイヤー以外のプレイヤーにはRPC経由で割り当てる
        if (!playerRoleImages.ContainsKey(actorNumber))
        {
            playerRoleImages.Add(actorNumber, roleImage);
        }
    }
    
    private void AssignRoleMessageToPlayer(int actorNumber, string roleMessage)
    {
        if(!playerRoleMessages.ContainsKey(actorNumber))
        {
            playerRoleMessages.Add(actorNumber, roleMessage);

            //他のクライアントにも役割メッセージを通知する
            photonView.RPC("SyncPlayerRoleMessage", RpcTarget.Others, actorNumber, roleMessage);
        }
        
    }

    [PunRPC]
    private void SyncPlayerRoleMessage(int actorNumber, string roleMessage)
    {
        if(!playerRoleMessages.ContainsKey(actorNumber))
        {
            playerRoleMessages.Add(actorNumber, roleMessage);
        }
    }

    private void Update() 
    {
        // ルームに入っていない場合は何もしない
        if(!PhotonNetwork.InRoom) { return; }
        //まだゲームの開始時刻が設定されていない場合は更新しない
        if(!PhotonNetwork.CurrentRoom.TryGetStartTime(out int timestamp)) { return; }

        float elapsedTime = Mathf.Max(0f, unchecked(PhotonNetwork.ServerTimestamp - timestamp) / 1000f);
        if(elapsedTime <= preparationTime)
        {
            //準備時間中は何もしない
            return;
        }
        else
        {
            //準備時間の分、経過時間を引く
            elapsedTime -= preparationTime;
            timerText.text = timeLimit - elapsedTime > 0 ? (timeLimit - elapsedTime).ToString("F0") : "0";
        }
    }

    public Sprite GetPlayerRoleImage()
    {
        // 自分のプレイヤーの役割を取得
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        if (playerRoleImages.ContainsKey(actorNumber))
        {
            return playerRoleImages[actorNumber];
        }
        return null;
    }

    public string GetPlayerRoleMessage()
    {
        // 自分のプレイヤーの役割メッセージを取得
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        if (playerRoleMessages.ContainsKey(actorNumber))
        {
            return playerRoleMessages[actorNumber];
        }
        return "";
    }

    private void UpdateRoleTextBasedOnRole()
    {
        // 自分のプレイヤーの子オブジェクトのTextを更新
        GameObject localPlayerImageObject = GameObject.Find("RoleImage");
        if (localPlayerImageObject != null)
        {
            Image localPlayerImage = localPlayerImageObject.GetComponent<Image>();
            localPlayerImage.sprite = GetPlayerRoleImage();
        }
    }

    private void UpdateRoleMessageTextBasedOnRole()
    {
        // 自分のプレイヤーの子オブジェクトのTextを更新
        GameObject localPlayerTextObject = GameObject.Find("MessageText");
        if (localPlayerTextObject != null)
        {
            TMP_Text localPlayerText = localPlayerTextObject.GetComponent<TMP_Text>();
            localPlayerText.text = GetPlayerRoleMessage();
        }
    }
}
