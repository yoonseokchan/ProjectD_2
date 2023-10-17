using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrigger : MonoBehaviour
{
    public GameObject MargePrefab;  // 인스펙터에서 할당할 변수

    private List<Transform> spawnPoints = new List<Transform>();
    private int spawnIndex = 0;

    void Start()
    {
        // SpawnPointGroup 게임오브젝트의 Transform 컴포넌트 추출
        Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;

        if (spawnPointGroup != null)
        {
            // SpawnPointGroup 하위에 있는 모든 차일드 게임오브젝트의 Transform 컴포넌트 추출
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
        // 플레이어가 트리거를 통과한 경우
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player triggered!");

            // 트리거의 Collider를 비활성화
            GetComponent<Collider>().enabled = false;

            // 플레이어의 Marge 오브젝트 생성 및 연결
            SpawnMarge(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 플레이어가 트리거를 빠져나가면 트리거를 다시 활성화
        if (other.CompareTag("Player"))
        {
            // 트리거의 Collider를 활성화
            GetComponent<Collider>().enabled = true;
        }
    }

    void SpawnMarge(Transform playerTransform)
    {
        // 플레이어 위치에 Marge 오브젝트 생성 및 연결
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
