using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTurnGameManager : MonoBehaviour
{
    private float turnTimer = 30f; // 턴 타이머
    private float timer; // 경과 시간 저장

    private Camera mainCamera;
    private Animator timerAnimator; // 타이머의 애니메이터 컴포넌트

    void Start()
    {
        // Start에서 mainCamera 및 timerAnimator 초기화
        mainCamera = Camera.main;
        timerAnimator = GetComponent<Animator>();

        // OnSwitchTurn 이벤트에 ResetTimerAndAnimation 메서드를 구독
        DragAndShooting.OnSwitchTurn += ResetTimerAndAnimation;
    }

    void Update()
    {
        // 경과 시간을 누적
        timer += Time.deltaTime;

        // 30초가 지나면 턴 전환
        if (timer >= turnTimer)
        {
            SwitchTurns();
            // 타이머 초기화
            timer = 0f;
        }
        Debug.Log("Animator State: " + timerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Time"));
    }

    void SwitchTurns()
    {
        // 여기에 턴 변경 로직을 추가하면 됩니다.
        if (mainCamera == null) return;

        // 현재 포지션의 반대쪽으로 이동
        Vector3 oppositePosition = new Vector3(-mainCamera.transform.position.x, mainCamera.transform.position.y, -mainCamera.transform.position.z);

        // 현재 카메라의 쿼터니언을 가져오기
        Quaternion currentRotation = mainCamera.transform.rotation;

        // Y축 회전 각도를 현재 쿼터니언을 기반으로 계산
        float newYRotation = currentRotation.eulerAngles.y + 180f;

        // 360도를 넘어가면 0으로 초기화
        if (newYRotation >= 360f)
        {
            newYRotation -= 360f;
        }

        // X축은 현재 각도로, Y축은 새로 계산된 회전 각도로, Z축은 현재 각도로 설정
        mainCamera.transform.rotation = Quaternion.Euler(40f, newYRotation, 0f);
        mainCamera.transform.position = oppositePosition;
    }

    public void ResetTimerAndAnimation()
    {
        // 타이머 초기화
        timer = 0f;

        // 딜레이 후 애니메이션 초기화
        StartCoroutine(PlayAnimationAfterDelay("Time", 0f));
    }

    private IEnumerator PlayAnimationAfterDelay(string animationName, float delay)
    {
        // 딜레이
        yield return new WaitForSeconds(delay);

        // 애니메이션 초기화
        if (timerAnimator != null)
        {
            timerAnimator.Play(animationName);
        }
    }

    // OnSwitchTurn 이벤트에 대한 언 구독
    private void OnDisable()
    {
        DragAndShooting.OnSwitchTurn -= ResetTimerAndAnimation;
    }
}
