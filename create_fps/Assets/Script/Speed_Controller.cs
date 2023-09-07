using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed_Controller : MonoBehaviour
{
    //false�̎������Atrue�̎�����
    public static bool SpeedFlag = false;

    //�@�������X�s�[�h�Ƒ���X�s�[�h�͂����Őݒ肷��I
    //�����X�s�[�h
    public static float walkingSpeed = 5f;
    //����X�s�[�h
    public static float runningSpeed = 8f;

    //�X�s�[�h�R���g���[���[��ݒ�.Player.cs�ł��Q�Ƃ����ϐ�.
    public static float SpeedController = walkingSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SpeedFlag)
        {
            SpeedController = runningSpeed;
            //Debug.Log("SpeedController>>>" + SpeedController);
        }
        else
        {
            SpeedController = walkingSpeed;
            //Debug.Log("SpeedController>>>" + SpeedController);
        }
    }

    //EventTrigger��PointerDown�ɃA�^�b�`
    public void OnButtonDown()
    {
        SpeedFlag = true;
        //Debug.Log("SpeedFlag>>>" + SpeedFlag);
    }

    //EventTrigger��PointerUp�ɃA�^�b�`
    public void OnButtonUP()
    {
        SpeedFlag = false;
        //Debug.Log("SpeedFlag>>>" + SpeedFlag);
    }

}

/*
 * [�����F�{�^���������ĕ����Ƒ����؂�ւ��邽�߂̃R�[�h]
 * 
 // Update is called once per frame
    void Update()
    {
        //Debug.Log("If BEFORE>>" + SpeedFlag);
        if (SpeedFlag)
        {
            SpeedController = runningSpeed;
            //Debug.Log("�X�s�[�h��" + SpeedController + "�ɐ؂�ւ���1>>") ;
        }
        else
        {
            SpeedController = walkingSpeed;
            //Debug.Log("�X�s�[�h��" + SpeedController + "�ɐ؂�ւ����I>>");
        }
        //Debug.Log("If AFTER>>" + SpeedFlag);
    }

    public void OnClickSpeed()
    {
        //Debug.Log("Clicked! BEFORE>>" + SpeedFlag);
        SpeedFlag = !SpeedFlag;
        //Debug.Log("Clicked! AFTER>>" + SpeedFlag);
    }
 */

