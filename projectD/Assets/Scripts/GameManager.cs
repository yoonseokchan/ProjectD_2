using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Transform> points = new List<Transform>();  // ���� ����Ʈ ����Ʈ
    public List<GameObject> spawnedMarges = new List<GameObject>();

    // Marge ������Ʈ�� ������ ����Ʈ
    public List<GameObject> margeList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // SpawnPointGroup ���ӿ�����Ʈ�� Transform ������Ʈ ����
        Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;

        // SpawnPointGroup ������ �ִ� ��� ���ϵ� ���ӿ�����Ʈ�� Transform ������Ʈ ����
        foreach (Transform point in spawnPointGroup)
        {
            points.Add(point);
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

    public Vector3 GetNextSpawnPoint()
    {
        int currentIndex = spawnedMarges.Count % points.Count;
        return points[currentIndex].position;
    }

    // ���ο� ���� �޼��� �߰�
    public void SpawnMarge(GameObject margePrefab)
    {
        // ���� ����Ʈ �׷쿡�� ���� ���� ��ġ ��������
        Vector3 spawnPosition = GetNextSpawnPoint();

        // Marge ������ ���� �� ��ġ ����
        GameObject marge = Instantiate(margePrefab, spawnPosition, Quaternion.identity);

        // Rigidbody ������Ʈ Ȱ��ȭ
        Rigidbody margeRigidbody = marge.GetComponent<Rigidbody>();
        if (margeRigidbody != null)
        {
            margeRigidbody.isKinematic = true;
        }

        // ���� �Ŵ����� Marge �߰�
        AddMargeToList(marge);
        AddSpawnedMargeToList(marge);
    }
}
