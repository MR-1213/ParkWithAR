using Photon.Pun;
using UnityEngine;

public class Paint : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _inkPrefab;
    [SerializeField] private Transform _inkParent;

    /// <summary>
    /// 原点を定めるコンポーネント
    /// </summary>
    private OriginDecideFromImageMaker _originDecideFromImageMaker;

    private void Start()
    {
        _originDecideFromImageMaker = FindObjectOfType<OriginDecideFromImageMaker>();
    }

    private void Update()
    {
        if (!photonView.IsMine) return;

        if (0 < Input.touchCount)
        {
            var touch = Input.GetTouch(0);
            var inputPosition = Input.GetTouch(0).position;
            var paintPosZ = 0.5f;
            var tmpTouchPos = new Vector3(inputPosition.x, inputPosition.y, paintPosZ);
            var touchWorldPos = _originDecideFromImageMaker.WorldToOriginLocal(Camera.main.ScreenToWorldPoint(tmpTouchPos));

            if (touch.phase == TouchPhase.Began)
            {
                photonView.RPC(nameof(PaintStartRPC), RpcTarget.All, touchWorldPos);
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                photonView.RPC(nameof(PaintingRPC), RpcTarget.All, touchWorldPos);
            }
        }
    }

    /// <summary>
    /// RPCで生成
    /// </summary>
    [PunRPC]
    private void PaintStartRPC(Vector3 inkPosition)
    {
        Instantiate(_inkPrefab, inkPosition, Quaternion.identity, _inkParent);
    }

    /// <summary>
    /// RPCで動かす
    /// </summary>
    [PunRPC]
    private void PaintingRPC(Vector3 inkPosition)
    {
        if (_inkParent.childCount > 0)
        {
            _inkParent.transform.GetChild(_inkParent.childCount - 1).transform.position = inkPosition;
        }
    }
} 
