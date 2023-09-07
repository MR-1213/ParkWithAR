using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Jump : MonoBehaviour
{
    public Rigidbody rb;
    public float jumpPower = 10;

    private static bool JumpFlag = true;
    private static bool ButtonFlag = false;

    DateTime dt = DateTime.Now;

    void Start()
    {
       
    }

    void Update()
    {
        if (ButtonFlag)
        {
            ButtonFlag = false;
            //Debug.Log("BEFORE>>jumpFlag:" + JumpFlag);
            //Debug.Log(dt + "条件前>>>" + JumpFlag);
            if (JumpFlag)
            {
                rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            }
            JumpFlag = false;
        }
    }

    public void OnClickJump()
    {
        ButtonFlag = true;
        //Debug.Log("Clicked:" + ButtonFlag);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            JumpFlag = true;
            //Debug.Log("着地:" + JumpFlag);
            //Debug.Log(dt + "着地後>>>" + JumpFlag);
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            JumpFlag = false;
            //Debug.Log("離陸:" + JumpFlag);
            //Debug.Log(dt + "離陸後>>>" + JumpFlag);
        }
    }

}

//Spaceキーでジャンプ
//void Update()
//{
//    if (Input.GetKeyDown(KeyCode.Space))
//    {
//        Debug.Log("BEF>>JumpFlag:" + JumpFlag);
//        if (JumpFlag == true)
//        {
//            rb.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
//            Debug.Log("AFT>>JumpFlag:" + JumpFlag);
//        }
//    }
//}
