using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    

    //速度の変数
    public float speed = 5f;

    // Update is called once per frame
    void Update()
    {
        //動く方向を変数に格納
        Vector3 moveDire = ((transform.forward * JoyStick_Move.joyStickPosY) + (transform.right * JoyStick_Move.joyStickPosX)).normalized;

        //ポジションを更新
        transform.position += moveDire * speed * Time.deltaTime;

        //回転を更新する
        transform.rotation = Quaternion.Euler(JoyStickCam.rotY, JoyStickCam.rotX, 0);
    }



}
