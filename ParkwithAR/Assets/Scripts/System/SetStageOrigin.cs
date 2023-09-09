using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStageOrigin : MonoBehaviour
{
    public Transform stageOrigin;
    private void Start() 
    {
        
    }

    public Vector3 CalculateAvatorDistance(Vector3 currentPosition)
    {
        return stageOrigin.position - currentPosition;
    }

    public Vector3 CalculatePlayerDistance(Vector3 currentPosition)
    {
        return stageOrigin.position - currentPosition;
    }
}
