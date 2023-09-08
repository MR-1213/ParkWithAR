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


    // ゲーム開始時に初期位置を設定する
    void Start()
    {
        initialPosition = objectToRespawn.transform.position;
    }

    public void HideObject()
    {
        //オブジェクトを非表示にする
        objectToRespawn.SetActive(false);
    }

    public void ObjectRespawn()
    {
        //オブジェクトを初期位置にリスポーンさせる
        objectToRespawn.transform.position = initialPosition;
        
        //オブジェクトを表示する
        objectToRespawn.SetActive(true);
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
