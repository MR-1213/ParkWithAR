using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    //移動スピードを格納する変数
    private float speed = 5f;

    //bool型の状態変数
    public bool status = true;

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

    //歩くと走るを切り替える
    public void RunOrWalk(BaseEventData data)
    {
        if(status == true)
        {
            speed = 100f;
            status = false;
        }
        else
        {
            speed = 5f;
            status = true;
        }



    }
    


}
