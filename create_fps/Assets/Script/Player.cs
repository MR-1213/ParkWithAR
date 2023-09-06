using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    

    //���x�̕ϐ�
    public float speed = 5f;

    // Update is called once per frame
    void Update()
    {
        //����������ϐ��Ɋi�[
        Vector3 moveDire = ((transform.forward * JoyStick_Move.joyStickPosY) + (transform.right * JoyStick_Move.joyStickPosX)).normalized;

        //�|�W�V�������X�V
        transform.position += moveDire * speed * Time.deltaTime;

        //��]���X�V����
        transform.rotation = Quaternion.Euler(JoyStickCam.rotY, JoyStickCam.rotX, 0);
    }



}
