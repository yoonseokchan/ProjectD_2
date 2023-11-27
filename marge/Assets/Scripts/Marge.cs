using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marge : MonoBehaviour
{
    public GameObject Marged;
    private Vector3 MargedCPosition;


    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "MP1C1")
        {
            Vector3 MargedCPosition = col.contacts[0].point;
            Destroy(gameObject); // �ڱ��ڽ� ����
            Destroy(col.gameObject); // ������ ����
            Instantiate(Marged, MargedCPosition, Quaternion.identity);
        }
    }
}
