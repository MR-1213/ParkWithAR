using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Respawn : MonoBehaviour
{
    //�I�u�W�F�N�g�̏����ʒu��ݒ肷��ϐ�
    private Vector3 initialPosition;

    //Player��objectRespawn�ɐݒ�
    public GameObject objectToRespawn;

    //�{�^���������ꂽ�񐔂��i�[����ϐ�
    private static int pressedCounter = 0;


    //AR�J����
    private static Camera arCamera;


    // �Q�[���J�n���ɏ����ʒu��ݒ肷��
    void Start()
    {
        arCamera = Camera.main;
    }

    public void HideObject()
    {
        //�I�u�W�F�N�g���\���ɂ���
        objectToRespawn.SetActive(false);
    }

    public void ObjectRespawn()
    {
        //AR�J�����̈ʒu�ƌ�������Ray���쐬
        Ray ray = new Ray(arCamera.transform.position, -arCamera.transform.up);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit)){
            
            //Ray���n�ʂɓ��������ꍇ
            if(hit.transform.CompareTag("floor"))
            {
                //�I�u�W�F�N�g���X�|�[��������ʒu
                Vector3 spawnPosition = hit.point;
                //�n�ʂɔz�u���邽��Y���W��0�ɐݒ�i��ŗv�����I�I�I�j
                spawnPosition.y = 0;
                //�I�u�W�F�N�g���X�|�[��������
                Instantiate(objectToRespawn, spawnPosition,Quaternion.identity);
                //�I�u�W�F�N�g��\������
                objectToRespawn.SetActive(true);
                Debug.Log("�I�u�W�F�N�g��\���I");
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (pressedCounter == 1)
        {
            //�I�u�W�F�N�g���\���ɂ���֐��̌Ăяo��
            HideObject();
            //Debug.Log("pressedCounter[1] >>>" + pressedCounter);
        }

        if(pressedCounter == 2)
        {
            //���X�|�[��������֐��̌Ăяo��
            ObjectRespawn();
            //Debug.Log("pressedCounter[2] >>>" + pressedCounter);
            //�J�E���^�[�����Z�b�g
            pressedCounter = 0;
        }


    }

    //Respawn-Button-Area�I�u�W�F�N�g��Button�R���|�[�l���g��OnClick()�ɃA�^�b�`
    public void OnClickRespawn()
    {
        pressedCounter++;
        //Debug.Log("pressedCounter[NOW] >>>" + pressedCounter);
    }

}
