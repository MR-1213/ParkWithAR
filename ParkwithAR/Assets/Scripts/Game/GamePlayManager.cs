using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class GamePlayManager : MonoBehaviourPunCallbacks
{
    public static GamePlayManager Instance;

    private ChangeModeController changeModeController;

    public float timeLimit = 180f;
    public float preparationTime = 10f;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text avatorTimerText;
    [SerializeField] private Sprite seekImage;
    [SerializeField] private Sprite hideImage;
    [SerializeField] private Button changeToARButton;

    public GameObject joinedPlayer;
    public Dictionary<int, string> playerRoles = new Dictionary<int, string>();
    private Dictionary<int, Sprite> playerRoleImages = new Dictionary<int, Sprite>();
    private Dictionary<int, string> playerRoleMessages = new Dictionary<int, string>();
    private Dictionary<int, string> playerSeekWinMessages = new Dictionary<int, string>();
    private Dictionary<int, string> playerHideWinMessages = new Dictionary<int, string>();
    public bool isGameOver = false;

    //アバターを使用できる制限時間
    public float avatarLimitTime = 30f;

    //アバターを使用している時間
    public float avatarTime = 0f;

    //アバターを使用しているかどうか
    public bool isAvatar = false;

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
        changeModeController = GetComponent<ChangeModeController>();

        // プレイヤーを生成
        var v = new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(-3f, 3f));
        joinedPlayer = PhotonNetwork.Instantiate("Player", v, Quaternion.identity);

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

            // プレイヤーの役割を設定
            string role = i == seekPlayerIndex ? "Seek" : "Hide";
            Sprite roleImage = i == seekPlayerIndex ? seekImage : hideImage;
            string roleMessage = i == seekPlayerIndex ? "あなたは<color=red>Seek</color>です" : "あなたは<color=blue>Hide</color>です";
            string seekWinMessage = i == seekPlayerIndex ? "全員捕まえた！" : "全員捕まってしまった...";
            string hideWinMessage = i == seekPlayerIndex ? "逃げ切られた..." : "逃げ切った！";
            
            // プレイヤーと役割を紐付ける
            AssignRoleStringToPlayer(actorNumber, role);
            AssignRoleImageToPlayer(actorNumber, roleImage);
            //プレイヤーと役割メッセージを紐づける
            AssignRoleMessageToPlayer(actorNumber, roleMessage);
            //プレイヤーとSeek勝利メッセージを紐づける
            AssignSeekWinMessageToPlayer(actorNumber, seekWinMessage);
            //プレイヤーとHide勝利メッセージを紐づける
            AssignHideWinMessageToPlayer(actorNumber, hideWinMessage);

            /*
            // 自分のプレイヤーにだけ役割を表示する
            if (players[i].IsLocal)
            {
                UpdateRoleTextBasedOnRole();
                UpdateRoleMessageTextBasedOnRole();
            }
            */
        }
    }

    private void AssignRoleStringToPlayer(int actorNumber, string role)
    {
        // プレイヤーに役割を割り当てる
        // ローカルプレイヤー以外のプレイヤーにはRPC経由で割り当てる
        if (!playerRoles.ContainsKey(actorNumber))
        {
            playerRoles.Add(actorNumber, role);

            // 他のクライアントにも役割を通知する
            photonView.RPC("SyncPlayerRoleString", RpcTarget.Others, actorNumber, role);
        }
    }

    [PunRPC]
    private void SyncPlayerRoleString(int actorNumber, string role)
    {
        // プレイヤーに役割を割り当てる
        // ローカルプレイヤー以外のプレイヤーにはRPC経由で割り当てる
        if (!playerRoles.ContainsKey(actorNumber))
        {
            playerRoles.Add(actorNumber, role);
        }
    }

    private void AssignRoleImageToPlayer(int actorNumber, Sprite roleImage)
    {
        // プレイヤーに役割を割り当てる
        // ローカルプレイヤー以外のプレイヤーにはRPC経由で割り当てる
        if (!playerRoleImages.ContainsKey(actorNumber))
        {
            playerRoleImages.Add(actorNumber, roleImage);

            // 他のクライアントにも役割を通知する
            photonView.RPC("SyncPlayerRoleImage", RpcTarget.Others, actorNumber, roleImage);
        }
    }

    [PunRPC]
    private void SyncPlayerRoleImage(int actorNumber, Sprite roleImage)
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

    private void AssignSeekWinMessageToPlayer(int actorNumber, string seekWinMessage)
    {
        if(!playerSeekWinMessages.ContainsKey(actorNumber))
        {
            playerSeekWinMessages.Add(actorNumber, seekWinMessage);

            //他のクライアントにもSeek勝利メッセージを通知する
            photonView.RPC("SyncPlayerSeekWinMessage", RpcTarget.Others, actorNumber, seekWinMessage);
        }
    }

    [PunRPC]
    private void SyncPlayerSeekWinMessage(int actorNumber, string seekWinMessage)
    {
        if(!playerSeekWinMessages.ContainsKey(actorNumber))
        {
            playerSeekWinMessages.Add(actorNumber, seekWinMessage);
        }
    }

    private void AssignHideWinMessageToPlayer(int actorNumber, string hideWinMessage)
    {
        if(!playerHideWinMessages.ContainsKey(actorNumber))
        {
            playerHideWinMessages.Add(actorNumber, hideWinMessage);

            //他のクライアントにもHide勝利メッセージを通知する
            photonView.RPC("SyncPlayerHideWinMessage", RpcTarget.Others, actorNumber, hideWinMessage);
        }
    }

    [PunRPC]
    private void SyncPlayerHideWinMessage(int actorNumber, string hideWinMessage)
    {
        if(!playerHideWinMessages.ContainsKey(actorNumber))
        {
            playerHideWinMessages.Add(actorNumber, hideWinMessage);
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
            if(timeLimit - elapsedTime <= 0 && !isGameOver)
            {
                isGameOver = true;
            }
        }

        if(isAvatar)
        {
            //アバターを使用している場合は、使用時間を更新
            avatarTime += Time.deltaTime;
            if(avatarTime >= avatarLimitTime)
            {
                //アバターを使用できる制限時間を超えたら、アバターを使用できないようにする
                changeToARButton.onClick.Invoke();
            }
            else
            {
                //アバターを使用できる制限時間を超えていない場合は、残り時間を表示
                avatorTimerText.text = (avatarLimitTime - avatarTime).ToString("F0");
            }
        }
        else
        {
            //アバターを使用していない場合は、使用時間をリセット
            avatarTime = 0f;
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

    public string GetSeekWinMessage()
    {
        // 自分のプレイヤーのSeek勝利メッセージを取得
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        if (playerSeekWinMessages.ContainsKey(actorNumber))
        {
            return playerSeekWinMessages[actorNumber];
        }
        return "";
    }

    public string GetHideWinMessage()
    {
        // 自分のプレイヤーのHide勝利メッセージを取得
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        if (playerHideWinMessages.ContainsKey(actorNumber))
        {
            return playerHideWinMessages[actorNumber];
        }
        return "";
    }

    /*
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
    */
}
