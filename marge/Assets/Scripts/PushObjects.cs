using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObjects : MonoBehaviour
{
    public float pushForce = 10f;
    private Vector3 appliedPushForce;

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

    // 다른 스크립트에서 호출하여 힘을 적용하는 메서드
    public void ApplyPushForce(Vector3 force)
    {
        appliedPushForce = force * pushForce;
    }
}
