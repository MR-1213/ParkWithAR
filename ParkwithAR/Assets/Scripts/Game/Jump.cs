using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Jump : MonoBehaviour
{
    public Rigidbody rb;
    public float jumpPower = 10;

    private Button jumpButton;
    private  bool JumpFlag = true;
    private bool ButtonFlag = false;

    DateTime dt = DateTime.Now;

    private void Start() 
    {
        jumpButton = GameObject.Find("JumpButton").GetComponent<Button>();
        jumpButton.onClick.AddListener(OnClick);
    }

    void Update()
    {
        if (ButtonFlag)
        {
            ButtonFlag = false;
            if (JumpFlag)
            {
                rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            }
            JumpFlag = false;
        }
    }

    public void OnClick()
    {
        ButtonFlag = true;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            JumpFlag = true;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            JumpFlag = false;
        }
    }

    //void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.tag == "floor")
    //    {
    //        JumpFlag = true;
    //        Debug.Log("‹ó’†:" + JumpFlag);
    //    }
    //}

}


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
