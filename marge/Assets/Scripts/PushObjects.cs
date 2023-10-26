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
            // ���� ���
            Vector3 pushDirection = (otherRb.transform.position - transform.position).normalized;

            // ���� ��ü�� ���� ����
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(-pushDirection * pushForce, ForceMode.Impulse);
            }

            // �浹�� ��ü���� ���� ����
            otherRb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        }
    }

    // �ٸ� ��ũ��Ʈ���� ȣ���Ͽ� ���� �����ϴ� �޼���
    public void ApplyPushForce(Vector3 force)
    {
        appliedPushForce = force * pushForce;
    }
}
