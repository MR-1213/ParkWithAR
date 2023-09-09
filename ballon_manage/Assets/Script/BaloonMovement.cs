using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float liftForce = 10f; // 風船の浮力を調整する力
    public float dragCoefficient = 0.1f; // 空気抵抗の係数
    public Vector3 windDirection = Vector3.up; // 風の方向
    public float windStrength = 5f; // 風の強さ

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // 風の力を計算し、風船に適用
        Vector3 windForce = windDirection.normalized * windStrength;
        rb.AddForce(windForce);

        // 空気抵抗を計算し、風船に適用
        Vector3 relativeVelocity = rb.velocity - windForce;
        Vector3 dragForce = -relativeVelocity.normalized * dragCoefficient * relativeVelocity.sqrMagnitude;
        rb.AddForce(dragForce);

        // 風船の浮力を適用
        Vector3 lift = Vector3.up * liftForce;
        rb.AddForce(lift);
    }
}

