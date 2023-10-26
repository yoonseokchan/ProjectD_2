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
        gameManager = FindObjectOfType<GameManager>(); // GameManager 참조 변수 초기화
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            Debug.Log("플레이어가 트리거에 닿았어요!");

            player = other.transform;
            DragAndShooting dragAndShootingScript = player.GetComponent<DragAndShooting>();

            if (dragAndShootingScript != null)
            {
                GameObject margePrefab = dragAndShootingScript.MargePrefab;

                if (margePrefab != null)
                {
                    // Marge 프리팹 생성
                    SpawnMarge(margePrefab, "Player1");
                }
                else
                {
                    Debug.LogError("DragAndShooting 스크립트에서 Marge 프리팹이 설정되지 않았어요!");
                }
            }
            else
            {
                Debug.LogError("DragAndShooting 스크립트를 찾을 수 없습니다! " + player.name);
            }
        }
        else if (other.CompareTag("Player2"))
        {
            Debug.Log("플레이어2가 트리거에 닿았어요!");

            player = other.transform;
            DragAndShooting dragAndShootingScript = player.GetComponent<DragAndShooting>();

            if (dragAndShootingScript != null)
            {
                GameObject margePrefab = dragAndShootingScript.MargePrefab;

                if (margePrefab != null)
                {
                    // Marge 프리팹 생성
                    SpawnMarge(margePrefab, "Player2");
                }
                else
                {
                    Debug.LogError("DragAndShooting 스크립트에서 Marge 프리팹이 설정되지 않았어요!");
                }
            }
            else
            {
                Debug.LogError("DragAndShooting 스크립트를 찾을 수 없습니다! " + player.name);
            }
        }
    }


    private void SpawnMarge(GameObject margePrefab, string playerTag)
    {
        List<Transform> spawnPoints = (playerTag == "Player1") ? gameManager.pointsGroup2 : gameManager.pointsGroup1;

        // 스폰 포인트 그룹에서 다음 스폰 위치 가져오기
        Vector3 spawnPosition = gameManager.GetNextSpawnPoint(spawnPoints);

        // Marge 프리팹 생성 및 위치 설정
        GameObject marge = Instantiate(margePrefab, spawnPosition, Quaternion.identity);

        // Rigidbody 컴포넌트 활성화
        Rigidbody margeRigidbody = marge.GetComponent<Rigidbody>();
        if (margeRigidbody != null)
        {
            margeRigidbody.isKinematic = true;
        }

        // 게임 매니저에 Marge 추가
        gameManager.AddSpawnedMargeToList(marge);
    }
}
