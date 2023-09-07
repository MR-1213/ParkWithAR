using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed_Controller : MonoBehaviour
{
    //falseの時歩き、trueの時走る
    public static bool SpeedFlag = false;

    //　※歩くスピードと走るスピードはここで設定する！
    //歩くスピード
    public static float walkingSpeed = 5f;
    //走るスピード
    public static float runningSpeed = 8f;

    //スピードコントローラーを設定.Player.csでも参照される変数.
    public static float SpeedController = walkingSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("If BEFORE>>" + SpeedFlag);
        if (SpeedFlag)
        {
            SpeedController = runningSpeed;
            //Debug.Log("スピードを" + SpeedController + "に切り替えた1>>") ;
        }
        else
        {
            SpeedController = walkingSpeed;
            //Debug.Log("スピードを" + SpeedController + "に切り替えた！>>");
        }
        //Debug.Log("If AFTER>>" + SpeedFlag);
    }

    public void OnClickSpeed()
    {
        //Debug.Log("Clicked! BEFORE>>" + SpeedFlag);
        SpeedFlag = !SpeedFlag;
        //Debug.Log("Clicked! AFTER>>" + SpeedFlag);
    }

}
