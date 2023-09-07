using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    //速度の変数をSpeed_Controller.csから取得
    public static float speed = Speed_Controller.SpeedController;

    // Update is called once per frame
    void Update()
    {
        speed = Speed_Controller.SpeedController;

        //開発用
        //if (speed == Speed_Controller.runningSpeed)
        //{
        //    Debug.Log("speedが" + speed + "になった！");
        //}
        //else if (speed == Speed_Controller.walkingSpeed)
        //{
        //    Debug.Log("speedが" + speed + "になった！");
        //}

        //動く方向を変数に格納
        Vector3 moveDire = ((transform.forward * JoyStick_Move.joyStickPosY) + (transform.right * JoyStick_Move.joyStickPosX)).normalized;

        //ポジションを更新
        transform.position += moveDire * speed * Time.deltaTime;

        //回転を更新する
        transform.rotation = Quaternion.Euler(JoyStickCam.rotY, JoyStickCam.rotX, 0);
    }



}
