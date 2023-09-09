using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Respawn : MonoBehaviour
{
    //オブジェクトの初期位置を設定する変数
    private Vector3 initialPosition;

    //PlayerをobjectRespawnに設定
    public GameObject objectToRespawn;

    //ボタンが押された回数を格納する変数
    private static int pressedCounter = 0;


    //ARカメラ
    private static Camera arCamera;


    // ゲーム開始時に初期位置を設定する
    void Start()
    {
        arCamera = Camera.main;
    }

    public void HideObject()
    {
        //オブジェクトを非表示にする
        objectToRespawn.SetActive(false);
    }

    public void ObjectRespawn()
    {
        //ARカメラの位置と向きからRayを作成
        Ray ray = new Ray(arCamera.transform.position, -arCamera.transform.up);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit)){
            
            //Rayが地面に当たった場合
            if(hit.transform.CompareTag("floor"))
            {
                //オブジェクトをスポーンさせる位置
                Vector3 spawnPosition = hit.point;
                //地面に配置するためY座標を0に設定（後で要調整！！！）
                spawnPosition.y = 0;
                //オブジェクトをスポーンさせる
                Instantiate(objectToRespawn, spawnPosition,Quaternion.identity);
                //オブジェクトを表示する
                objectToRespawn.SetActive(true);
                Debug.Log("オブジェクトを表示！");
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (pressedCounter == 1)
        {
            //オブジェクトを非表示にする関数の呼び出し
            HideObject();
            //Debug.Log("pressedCounter[1] >>>" + pressedCounter);
        }

        if(pressedCounter == 2)
        {
            //リスポーンさせる関数の呼び出し
            ObjectRespawn();
            //Debug.Log("pressedCounter[2] >>>" + pressedCounter);
            //カウンターをリセット
            pressedCounter = 0;
        }


    }

    //Respawn-Button-AreaオブジェクトのButtonコンポーネントのOnClick()にアタッチ
    public void OnClickRespawn()
    {
        pressedCounter++;
        //Debug.Log("pressedCounter[NOW] >>>" + pressedCounter);
    }

}
