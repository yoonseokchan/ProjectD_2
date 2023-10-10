using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToCemetery : MonoBehaviour
{
    public Transform cemeteryPlane; // Cemetery Plane�� Transform ����
    private bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered!");

        // ����Ͽ� ����� ���� Ȯ��
        Debug.Log("Collided object name: " + other.gameObject.name);
        Debug.Log("Collided object tag: " + other.gameObject.tag);

        if (other.CompareTag("GameSceneObject"))
        {
            Debug.Log("GameSceneObject entered!");

            // "Cemetery" Plane���� �̵�
            other.transform.position = cemeteryPlane.position;

            // Ʈ������ Collider�� ��Ȱ��ȭ
            GetComponent<Collider>().enabled = false;

            // ��Ÿ �̵� ���� ó�� �߰� ����
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isTriggered && other.CompareTag("GameSceneObject"))
        {
            // �÷��̾ Ʈ���Ÿ� ���������� Ʈ���Ÿ� �ٽ� ��Ȱ��ȭ
            isTriggered = false;

            // Ʈ������ Collider�� Ȱ��ȭ
            GetComponent<Collider>().enabled = true;
        }
    }
}