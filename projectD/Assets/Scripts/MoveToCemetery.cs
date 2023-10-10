using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToCemetery : MonoBehaviour
{
    public Transform cemeteryPlane; // Cemetery Plane의 Transform 참조
    private bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered!");

        // 출력하여 디버깅 정보 확인
        Debug.Log("Collided object name: " + other.gameObject.name);
        Debug.Log("Collided object tag: " + other.gameObject.tag);

        if (other.CompareTag("GameSceneObject"))
        {
            Debug.Log("GameSceneObject entered!");

            // "Cemetery" Plane으로 이동
            other.transform.position = cemeteryPlane.position;

            // 트리거의 Collider를 비활성화
            GetComponent<Collider>().enabled = false;

            // 기타 이동 관련 처리 추가 가능
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isTriggered && other.CompareTag("GameSceneObject"))
        {
            // 플레이어가 트리거를 빠져나가면 트리거를 다시 비활성화
            isTriggered = false;

            // 트리거의 Collider를 활성화
            GetComponent<Collider>().enabled = true;
        }
    }
}