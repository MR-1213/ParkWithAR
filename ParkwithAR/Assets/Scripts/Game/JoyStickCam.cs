using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickCam : MonoBehaviour
{
    //�J�����̊��x
    public float aimSensitivity = 10;
    //�X�e�B�b�N�̓�����
    private int stickMovement = 0;
    //�����ׂ�����X,Y
    public float positionX, positionY;
    //�p�x����
    public float viewPointValue = 45;
    //��r�p�Ɉꎞ�I�ɐ��l���i�[����X,Y
    private float tempPosX = 0, tempPosY = 0;
    //�v���C���[�Ɍ����Ăق�����]���i�[����X,Y
    public float rotX = 0, rotY = 0;

    private void Start()
    {
        stickMovement = 3 * (Screen.width + Screen.height) / 100;
    }


    //�E��ʂ��h���b�N���Ă���Ƃ��ɌĂԊ֐�
    public void OnMove(BaseEventData data)
    {
        PointerEventData pointer = data as PointerEventData;

        //�h���b�O���ꂽ���l��ϐ��Ɋi�[����
        positionX = pointer.position.x / stickMovement;
        positionY = pointer.position.y / stickMovement;

        //���x����
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



    //�E��ʂ���w��b�����Ƃ��ɌĂԊ֐�
    public void PointerUP(BaseEventData data)
    {
        //�|�W�V�����������֐�
        PositionInitialization();
        //��]
        Rotation();
    }

    //�|�W�V�����������֐�
    public void PositionInitialization()
    {
        positionX = 0;
        positionY = 0;
    }


}