using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickCam : MonoBehaviour
{
    //カメラの感度
    public float aimSensitivity = 10;
    //スティックの動く量
    private int stickMovement = 0;
    //向くべき方向X,Y
    public float positionX, positionY;
    //角度制限
    public float viewPointValue = 45;
    //比較用に一時的に数値を格納するX,Y
    private float tempPosX = 0, tempPosY = 0;
    //プレイヤーに向いてほしい回転を格納するX,Y
    public static float rotX = 0, rotY = 0;

    private void Start()
    {
        stickMovement = 3 * (Screen.width + Screen.height) / 100;
    }


    //右画面をドラックしているときに呼ぶ関数
    public void Move(BaseEventData data)
    {
        PointerEventData pointer = data as PointerEventData;

        //ドラッグされた数値を変数に格納する
        positionX = pointer.position.x / stickMovement;
        positionY = pointer.position.y / stickMovement;

        //感度調整
        positionX *= aimSensitivity;
        positionY *= aimSensitivity;

        Rotation();

    }

    public void Rotation()
    {
        if (positionX != tempPosX)
        {
            if (tempPosX == 0)
            {
                tempPosX = positionX;
            }

            if (positionX == 0)
            {
                tempPosX = 0;
            }

            rotX -= (tempPosX - positionX);

            if (rotX > 360)
            {
                rotX -= 360;
            }

            if (rotX < -360)
            {
                rotX += 360;
            }

            tempPosX = positionX;

        }

        if (positionY != tempPosY)
        {
            if (tempPosY == 0)
            {
                tempPosY = positionY;
            }

            if (positionY == 0)
            {
                tempPosY = 0;
            }

            rotY += (tempPosY - positionY);

            if (rotY > viewPointValue)
            {
                rotY = viewPointValue;
            }

            if (rotY < -viewPointValue)
            {
                rotY = -viewPointValue;
            }

            tempPosY = positionY;

        }
    }



    //右画面から指を話したときに呼ぶ関数
    public void PointerUP(BaseEventData data)
    {
        //ポジション初期化関数
        PositionInitialization();
        //回転
        Rotation();
    }

    //ポジション初期化関数
    public void PositionInitialization()
    {
        positionX = 0;
        positionY = 0;
    }


}