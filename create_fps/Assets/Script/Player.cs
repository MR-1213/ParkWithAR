using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    //���x�̕ϐ���Speed_Controller.cs����擾
    public static float speed = Speed_Controller.SpeedController;

    // Update is called once per frame
    void Update()
    {
        speed = Speed_Controller.SpeedController;

        //�J���p
        //if (speed == Speed_Controller.runningSpeed)
        //{
        //    Debug.Log("speed��" + speed + "�ɂȂ����I");
        //}
        //else if (speed == Speed_Controller.walkingSpeed)
        //{
        //    Debug.Log("speed��" + speed + "�ɂȂ����I");
        //}

        //����������ϐ��Ɋi�[
        Vector3 moveDire = ((transform.forward * JoyStick_Move.joyStickPosY) + (transform.right * JoyStick_Move.joyStickPosX)).normalized;

        //�|�W�V�������X�V
        transform.position += moveDire * speed * Time.deltaTime;

        //��]���X�V����
        transform.rotation = Quaternion.Euler(JoyStickCam.rotY, JoyStickCam.rotX, 0);
    }



}
