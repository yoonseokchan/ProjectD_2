using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrigger : MonoBehaviour
{
    private Rigidbody rb;
    private Transform player;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); // GameManager ���� ���� �ʱ�ȭ
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            Debug.Log("�÷��̾ Ʈ���ſ� ��Ҿ��!");

            player = other.transform;
            DragAndShooting dragAndShootingScript = player.GetComponent<DragAndShooting>();

            if (dragAndShootingScript != null)
            {
                GameObject margePrefab = dragAndShootingScript.MargePrefab;

                if (margePrefab != null)
                {
                    // Marge ������ ����
                    SpawnMarge(margePrefab, "Player1");
                    Destroy(other.gameObject);
                }
                else
                {
                    Debug.LogError("DragAndShooting ��ũ��Ʈ���� Marge �������� �������� �ʾҾ��!");
                }
            }
            else
            {
                Debug.LogError("DragAndShooting ��ũ��Ʈ�� ã�� �� �����ϴ�! " + player.name);
            }
        }
        else if (other.CompareTag("Player2"))
        {
            Debug.Log("�÷��̾�2�� Ʈ���ſ� ��Ҿ��!");

            player = other.transform;
            DragAndShooting dragAndShootingScript = player.GetComponent<DragAndShooting>();

            if (dragAndShootingScript != null)
            {
                GameObject margePrefab = dragAndShootingScript.MargePrefab;

                if (margePrefab != null)
                {
                    // Marge ������ ����
                    SpawnMarge(margePrefab, "Player2");
                    Destroy(other.gameObject);
                }
                else
                {
                    Debug.LogError("DragAndShooting ��ũ��Ʈ���� Marge �������� �������� �ʾҾ��!");
                }
            }
            else
            {
                Debug.LogError("DragAndShooting ��ũ��Ʈ�� ã�� �� �����ϴ�! " + player.name);
            }
        }
    }


    private void SpawnMarge(GameObject margePrefab, string playerTag)
    {
        List<Transform> spawnPoints = (playerTag == "Player1") ? gameManager.pointsGroup2 : gameManager.pointsGroup1;

        // ���� ����Ʈ �׷쿡�� ���� ���� ��ġ ��������
        Vector3 spawnPosition = gameManager.GetNextSpawnPoint(spawnPoints);

        // Marge ������ ���� �� ��ġ ����
        GameObject marge = Instantiate(margePrefab, spawnPosition, Quaternion.identity);

        // Rigidbody ������Ʈ Ȱ��ȭ
        Rigidbody margeRigidbody = marge.GetComponent<Rigidbody>();
        if (margeRigidbody != null)
        {
            margeRigidbody.isKinematic = false;
        }

        // ���� �Ŵ����� Marge �߰�
        gameManager.AddSpawnedMargeToList(marge);
    }
}
