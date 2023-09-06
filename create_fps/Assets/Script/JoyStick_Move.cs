using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick_Move : MonoBehaviour
{
    //スティックを格納するための変数
    public GameObject joyStick;
    //ジョイスティックキャンバスのポジションを格納するための変数
    private RectTransform joyStickRectTransform;
    //ジョイスティックのバックグランドを格納する変数
    public GameObject backGround;
    //スティックが動ける範囲
    public int stickRange = 4;
    //実際に動く値
    private int stickMovement = 0;



    public static float joyStickPosX;
    public static float joyStickPosY;

    // Start is called before the first frame update
    private void Start()
    {
        //初期設定
        Initialization();
    }

    //初期設定の関数定義
    private void Initialization()
    {
        //異なる画面サイズでも同じような挙動にするための調整
        stickMovement = stickRange * (Screen.width + Screen.height) / 100;

        joyStickRectTransform = joyStick.GetComponent<RectTransform>();

    }


    //ジョイスティックの動き
    public void Move(BaseEventData data)
    {
        PointerEventData pointer = data as PointerEventData;

        //ジョイスティックと入力位置の差を格納
        float x = backGround.transform.position.x - pointer.position.x;
        float y = backGround.transform.position.y - pointer.position.y;

        float angle = Mathf.Atan2(y, x);
        
        if(Vector2.Distance(backGround.transform.position,pointer.position) > stickMovement)
        {
            y = stickMovement * Mathf.Sin(angle);
            x = stickMovement * Mathf.Cos(angle);
        }

        //プレイヤーを動かす値を格納
        joyStickPosX = -x / stickMovement;
        joyStickPosY = -y / stickMovement;

        joyStick.transform.position = new Vector2(backGround.transform.position.x - x, backGround.transform.position.y - y);

    }
    
    //入力中に呼ぶ関数
    public void PointerDown(BaseEventData data)
    {
        PointerEventData pointer = data as PointerEventData;

        //JoyStickDisplay(true);
        joyStick.transform.position = pointer.position;
    }

    //指を話した瞬間に呼ぶ関数
    public void PointerUp(BaseEventData data)
    {
        //ジョイスティックのポジションを初期化
        PositionInitialization();

    }

    //ジョイスティックのポジション初期化
    public void PositionInitialization()
    {
        joyStickRectTransform.anchoredPosition = Vector2.zero;
        //指を話たときにプレイヤーの移動をやめる
        joyStickPosX = 0;
        joyStickPosY = 0;
    }

}


/*
 * [メモ：stickをタップした場所に出現させるバージョン]
 * 
 using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick_Move : MonoBehaviour
{
    //スティックを格納するための変数
    public GameObject joyStick;
    //ジョイスティックキャンバスのポジションを格納するための変数
    private RectTransform joyStickRectTransform;
    //ジョイスティックのバックグランドを格納する変数
    public GameObject backGround;
    //スティックが動ける範囲
    public int stickRange = 3;
    //実際に動く値
    private int stickMovement = 0;

    // Start is called before the first frame update
    private void Start()
    {
        //初期設定
        Initialization();
    }

    //初期設定の関数定義
    private void Initialization()
    {
        //異なる画面サイズでも同じような挙動にするための調整
        stickMovement = stickRange * (Screen.width + Screen.height) / 100;

        joyStickRectTransform = joyStick.GetComponent<RectTransform>();

        //ジョイスティックの表示
        JoyStickDisplay(false);
    }

    

    //ジョイスティックの表示の関数定義
    private void JoyStickDisplay(bool x)
    {
        backGround.SetActive(x);
        joyStick.SetActive(x);
    }

    

    //ジョイスティックの動き
    public void Move(BaseEventData data)
    {
        PointerEventData pointer = data as PointerEventData;

        //ジョイスティックと入力位置の差を格納
        float x = backGround.transform.position.x - pointer.position.x;
        float y = backGround.transform.position.y - pointer.position.y;

        float angle = Mathf.Atan2(y, x);
        
        if(Vector2.Distance(backGround.transform.position,pointer.position) > stickMovement)
        {
            y = stickMovement * Mathf.Sin(angle);
            x = stickMovement * Mathf.Cos(angle);
        }

        joyStick.transform.position = new Vector2(backGround.transform.position.x - x, backGround.transform.position.y - y);

    }

    
    
    //入力中に呼ぶ関数
    public void PointerDown(BaseEventData data)
    {
        PointerEventData pointer = data as PointerEventData;

        JoyStickDisplay(true);
        backGround.transform.position = pointer.position;
    }

    //指を話した瞬間に呼ぶ関数
    public void PointerUp(BaseEventData data)
    {
        //ジョイスティックのポジションを初期化
        PositionInitialization();

        JoyStickDisplay(false);
        
    }

    //ジョイスティックのポジション初期化
    public void PositionInitialization()
    {
        joyStickRectTransform.anchoredPosition = Vector2.zero;
    }
    

}

 
 */