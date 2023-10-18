using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrigger : MonoBehaviour
{
    private Rigidbody rb;
    private Transform player;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
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
                    SpawnMarge(margePrefab);

                    // �÷��̾� ������Ʈ ��Ȱ��ȭ
                    other.gameObject.SetActive(false);
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

    private void SpawnMarge(GameObject margePrefab)
    {
        // ���� �Ŵ����� ����
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            // ���� ����Ʈ �׷쿡�� ���� ���� ��ġ ��������
            Vector3 spawnPosition = gameManager.GetNextSpawnPoint();

            // Marge ������ ���� �� ��ġ ����
            GameObject marge = Instantiate(margePrefab, spawnPosition, Quaternion.identity);

            // Rigidbody ������Ʈ Ȱ��ȭ
            Rigidbody margeRigidbody = marge.GetComponent<Rigidbody>();
            if (margeRigidbody != null)
            {
                margeRigidbody.isKinematic = true;
            }

            // ���� �Ŵ����� Marge �߰�
            gameManager.AddSpawnedMargeToList(marge);
        }
        else
        {
            Debug.LogError("���� �Ŵ����� ã�� �� �����!");
        }
    }
}
