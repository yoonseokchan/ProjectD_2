using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
        // Ʈ���Ÿ� ����� ������Ʈ�� "Player" �ױ׸� �������� Ȯ��
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player triggered!");

            // Ʈ������ Collider�� ��Ȱ��ȭ
            GetComponent<Collider>().enabled = false;

            // �÷��̾� ����
            SpawnPlayer();
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

    void SpawnPlayer()
    {
        if (spawnPoints.Count == 0)
        {
            Debug.LogError("Spawn points list is empty!");
            return;
        }

        // �÷��̾� ����
        GameObject player = Instantiate(Resources.Load<GameObject>("Prefabs/PlayerPrefab"), GetNextSpawnPoint(), Quaternion.identity);

        // �÷��̾��� Marge ������Ʈ ���� �� ����
        GameObject marge = Instantiate(Resources.Load<GameObject>("Prefabs/MargePrefab"), player.transform.position, player.transform.rotation);
        marge.transform.parent = player.transform;
    }

    Vector3 GetNextSpawnPoint()
    {
        int currentIndex = spawnIndex % spawnPoints.Count;
        spawnIndex++;
        return spawnPoints[currentIndex].position;
    }
}
