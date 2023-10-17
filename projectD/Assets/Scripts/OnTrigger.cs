using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrigger : MonoBehaviour
{
    public GameObject MargePrefab;  // �ν����Ϳ��� �Ҵ��� ����

    private List<Transform> spawnPoints = new List<Transform>();
    private int spawnIndex = 0;

    void Start()
    {
        // SpawnPointGroup ���ӿ�����Ʈ�� Transform ������Ʈ ����
        Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;

        if (spawnPointGroup != null)
        {
            // SpawnPointGroup ������ �ִ� ��� ���ϵ� ���ӿ�����Ʈ�� Transform ������Ʈ ����
            foreach (Transform point in spawnPointGroup)
            {
                spawnPoints.Add(point);
            }
        }
        else
        {
            Debug.LogError("SpawnPointGroup not found!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾ Ʈ���Ÿ� ����� ���
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player triggered!");

            // Ʈ������ Collider�� ��Ȱ��ȭ
            GetComponent<Collider>().enabled = false;

            // �÷��̾��� Marge ������Ʈ ���� �� ����
            SpawnMarge(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �÷��̾ Ʈ���Ÿ� ���������� Ʈ���Ÿ� �ٽ� Ȱ��ȭ
        if (other.CompareTag("Player"))
        {
            // Ʈ������ Collider�� Ȱ��ȭ
            GetComponent<Collider>().enabled = true;
        }
    }

    void SpawnMarge(Transform playerTransform)
    {
        // �÷��̾� ��ġ�� Marge ������Ʈ ���� �� ����
        GameObject marge = Instantiate(MargePrefab, playerTransform.position, playerTransform.rotation);
        marge.transform.parent = playerTransform;
    }

    Vector3 GetNextSpawnPoint()
    {
        int currentIndex = spawnIndex % spawnPoints.Count;
        spawnIndex++;
        return spawnPoints[currentIndex].position;
    }
}
