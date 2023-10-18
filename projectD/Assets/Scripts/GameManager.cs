using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Transform> points = new List<Transform>();  // 스폰 포인트 리스트
    public List<GameObject> spawnedMarges = new List<GameObject>();

    // Marge 오브젝트를 관리할 리스트
    public List<GameObject> margeList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // SpawnPointGroup 게임오브젝트의 Transform 컴포넌트 추출
        Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;

        // SpawnPointGroup 하위에 있는 모든 차일드 게임오브젝트의 Transform 컴포넌트 추출
        foreach (Transform point in spawnPointGroup)
        {
            points.Add(point);
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

    public Vector3 GetNextSpawnPoint()
    {
        int currentIndex = spawnedMarges.Count % points.Count;
        return points[currentIndex].position;
    }

    // 새로운 스폰 메서드 추가
    public void SpawnMarge(GameObject margePrefab)
    {
        // 스폰 포인트 그룹에서 다음 스폰 위치 가져오기
        Vector3 spawnPosition = GetNextSpawnPoint();

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
