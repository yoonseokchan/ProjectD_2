using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Transform> pointsGroup1 = new List<Transform>();  // 플레이어1의 스폰 포인트 리스트
    public List<Transform> pointsGroup2 = new List<Transform>();  // 플레이어2의 스폰 포인트 리스트
    public List<GameObject> spawnedMarges = new List<GameObject>();

    private List<GameObject> margeList = new List<GameObject>(); // Marge 오브젝트를 관리할 리스트
    private List<Transform> currentPoints; // 현재 사용 중인 스폰 포인트 그룹

    // Start is called before the first frame update
    void Start()
    {
        // 자식 포인트들을 자동으로 등록
        Transform spawnPointGroup1 = GameObject.Find("SpawnPointGroup1")?.transform;
        Transform spawnPointGroup2 = GameObject.Find("SpawnPointGroup2")?.transform;

        if (spawnPointGroup1 != null && spawnPointGroup2 != null)
        {
            RegisterSpawnPoints(spawnPointGroup1, pointsGroup1);
            RegisterSpawnPoints(spawnPointGroup2, pointsGroup2);

            // 초기에는 Player1의 스폰 포인트 그룹을 선택
            currentPoints = pointsGroup1;
        }
        else
        {
            Debug.LogError("스폰 포인트 그룹을 찾을 수 없어요!");
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
            Debug.LogError("스폰 포인트 그룹이 없어요!");
        }
    }

    public void AddSpawnedMargeToList(GameObject marge)
    {
        spawnedMarges.Add(marge);
    }

    // Marge 오브젝트를 리스트에 추가하는 함수
    public void AddMargeToList(GameObject marge)
    {
        margeList.Add(marge);
    }

    public Vector3 GetNextSpawnPoint(List<Transform> spawnPoints)
    {
        int currentIndex = spawnedMarges.Count % spawnPoints.Count;
        return spawnPoints[currentIndex].position;
    }

    // 새로운 스폰 메서드 추가
    public void SpawnMarge(GameObject margePrefab, string playerTag)
    {
        // 스폰 포인트 그룹 선택
        List<Transform> spawnPoints = (playerTag == "Player1") ? pointsGroup1 : pointsGroup2;

        // 스폰 포인트 그룹에서 다음 스폰 위치 가져오기
        Vector3 spawnPosition = GetNextSpawnPoint(spawnPoints);

        // Marge 프리팹 생성 및 위치 설정
        GameObject marge = Instantiate(margePrefab, spawnPosition, Quaternion.identity);

        // Rigidbody 컴포넌트 활성화
        Rigidbody margeRigidbody = marge.GetComponent<Rigidbody>();
        if (margeRigidbody != null)
        {
            margeRigidbody.isKinematic = true;
        }

        // 게임 매니저에 Marge 추가
        AddMargeToList(marge);
        AddSpawnedMargeToList(marge);
    }
}