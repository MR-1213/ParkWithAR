using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpeedController : MonoBehaviour
{
    //falseの時歩き、trueの時走る
    public static bool SpeedFlag = false;

    // ※歩くスピードと走るスピードはここで設定する！
    //歩くスピード
    public static float walkingSpeed = 3f;
    //走るスピード
    public static float runningSpeed = 7f;

    //スピードコントローラーを設定.Player.csでも参照される変数.
    public float playerSpeed = walkingSpeed;

    private Button speedButton;

    private void Start() 
    {
        speedButton = GameObject.Find("SpeedButton").GetComponent<Button>();

        //EventTriggerのPointerDownとPointerUpにメソッドを登録
        EventTrigger trigger = speedButton.GetComponent<EventTrigger>();
        EventTrigger.Entry entryDown = new EventTrigger.Entry();
        entryDown.eventID = EventTriggerType.PointerDown;
        entryDown.callback.AddListener((data) => { OnButtonDown(); });
        trigger.triggers.Add(entryDown);

        EventTrigger.Entry entryUp = new EventTrigger.Entry();
        entryUp.eventID = EventTriggerType.PointerUp;
        entryUp.callback.AddListener((data) => { OnButtonUP(); });
        trigger.triggers.Add(entryUp);

    }

    // Update is called once per frame
    private void Update()
    {
        if (SpeedFlag)
        {
            playerSpeed = runningSpeed;
            Debug.Log("playerSpeed>>>" + playerSpeed);
        }
        else
        {
            playerSpeed = walkingSpeed;
            Debug.Log("playerSpeed>>>" + playerSpeed);
        }
    }

    //EventTriggerのPointerDownにアタッチ
    public void OnButtonDown()
    {
        SpeedFlag = true;
        Debug.Log("SpeedFlag>>>" + SpeedFlag);
    }

    //EventTriggerのPointerUpにアタッチ
    public void OnButtonUP()
    {
        SpeedFlag = false;
        Debug.Log("SpeedFlag>>>" + SpeedFlag);
    }
}
