using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeGame : MonoBehaviour
{
    public GameObject primitivePrefab; // 미리 만들어진 primitive 프리팹을 연결할 변수
    public GameObject knightPrefab; // 미리 만들어진 knight 프리팹을 연결할 변수
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("primitive")) // 만약 충돌한 오브젝트가 "primitive" 태그를 가지면
        {
            MergeObjects(collision.gameObject); // 머지 함수 호출
        }
    }

    void MergeObjects(GameObject primitive)
    {
        Destroy(primitive); // "primitive" 오브젝트 삭제
        
        GameObject newknight = Instantiate(knightPrefab, collisionPoint, Quaternion.identity);
    }

}
