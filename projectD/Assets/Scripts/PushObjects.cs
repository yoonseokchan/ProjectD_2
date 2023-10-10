using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObjects : MonoBehaviour
{
    public float pushForce = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody otherRb = collision.collider.GetComponent<Rigidbody>();

        if (otherRb != null)
        {
            // 방향 계산
            Vector3 pushDirection = (otherRb.transform.position - transform.position).normalized;

            // 현재 물체에 힘을 가함
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(-pushDirection * pushForce, ForceMode.Impulse);
            }

            // 충돌한 물체에도 힘을 가함
            otherRb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        }
    }
}
