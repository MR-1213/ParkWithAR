using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using ExitGames.Client.Photon.StructWrapping;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject myAvator;
    private float speed = 5f; 

    private void Update()
    {
        if(photonView.IsMine)
        {
            //プレイヤーの移動
            this.transform.position += new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical"))*speed*Time.deltaTime; 
        }

        //クリックした場所にレイを飛ばす
        if(photonView.IsMine && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit))
            {
                //地面に当たったらそこにAvatorを移動させる
                if(hit.collider.gameObject.CompareTag("Ground"))
                {
                    myAvator.transform.position = hit.point;
                }
            }
        }
    }
}
