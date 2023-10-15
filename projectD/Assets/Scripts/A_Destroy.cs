using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Destroy : MonoBehaviour
{
    public GameObject B;
    private Vector3 BCPosition;

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "GameObject A")
        {
            Vector3 BCPosition = col.contacts[0].point;
            Destroy(gameObject); // �ڱ��ڽ� ����
            Destroy(col.gameObject); // ������ ����
            Instantiate(B, BCPosition, Quaternion.identity);
        }
    }
}
