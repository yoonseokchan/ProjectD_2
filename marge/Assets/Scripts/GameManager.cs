using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Transform> pointsGroup1 = new List<Transform>();  // �÷��̾�1�� ���� ����Ʈ ����Ʈ
    public List<Transform> pointsGroup2 = new List<Transform>();  // �÷��̾�2�� ���� ����Ʈ ����Ʈ
    public List<GameObject> spawnedMarges = new List<GameObject>();

    private List<GameObject> margeList = new List<GameObject>(); // Marge ������Ʈ�� ������ ����Ʈ
    private List<Transform> currentPoints; // ���� ��� ���� ���� ����Ʈ �׷�

    // Start is called before the first frame update
    void Start()
    {
        // �ڽ� ����Ʈ���� �ڵ����� ���
        Transform spawnPointGroup1 = GameObject.Find("SpawnPointGroup1")?.transform;
        Transform spawnPointGroup2 = GameObject.Find("SpawnPointGroup2")?.transform;

        if (spawnPointGroup1 != null && spawnPointGroup2 != null)
        {
            RegisterSpawnPoints(spawnPointGroup1, pointsGroup1);
            RegisterSpawnPoints(spawnPointGroup2, pointsGroup2);

            // �ʱ⿡�� Player1�� ���� ����Ʈ �׷��� ����
            currentPoints = pointsGroup1;
        }
        else
        {
            Debug.LogError("���� ����Ʈ �׷��� ã�� �� �����!");
        }
    }

    private void RegisterSpawnPoints(Transform spawnPointGroup, List<Transform> spawnPointsList)
    {
        if (spawnPointGroup != null)
        {
            foreach (Transform point in spawnPointGroup)
            {
                spawnPointsList.Add(point);
            }
        }
        else
        {
            Debug.LogError("���� ����Ʈ �׷��� �����!");
        }
    }

    public void AddSpawnedMargeToList(GameObject marge)
    {
        spawnedMarges.Add(marge);
    }

    // Marge ������Ʈ�� ����Ʈ�� �߰��ϴ� �Լ�
    public void AddMargeToList(GameObject marge)
    {
        margeList.Add(marge);
    }

    public Vector3 GetNextSpawnPoint(List<Transform> spawnPoints)
    {
        int currentIndex = spawnedMarges.Count % spawnPoints.Count;
        return spawnPoints[currentIndex].position;
    }

    
}