using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTurnGameManager : MonoBehaviour
{
    private float turnTimer = 30f; // �� Ÿ�̸�
    private float timer; // ��� �ð� ����

    private Camera mainCamera;
    private Animator timerAnimator; // Ÿ�̸��� �ִϸ����� ������Ʈ

    void Start()
    {
        // Start���� mainCamera �� timerAnimator �ʱ�ȭ
        mainCamera = Camera.main;
        timerAnimator = GetComponent<Animator>();

        // OnSwitchTurn �̺�Ʈ�� ResetTimerAndAnimation �޼��带 ����
        DragAndShooting.OnSwitchTurn += ResetTimerAndAnimation;
    }

    void Update()
    {
        // ��� �ð��� ����
        timer += Time.deltaTime;

        // 30�ʰ� ������ �� ��ȯ
        if (timer >= turnTimer)
        {
            SwitchTurns();
            // Ÿ�̸� �ʱ�ȭ
            timer = 0f;
        }
        Debug.Log("Animator State: " + timerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Time"));
    }

    void SwitchTurns()
    {
        // ���⿡ �� ���� ������ �߰��ϸ� �˴ϴ�.
        if (mainCamera == null) return;

        // ���� �������� �ݴ������� �̵�
        Vector3 oppositePosition = new Vector3(-mainCamera.transform.position.x, mainCamera.transform.position.y, -mainCamera.transform.position.z);

        // ���� ī�޶��� ���ʹϾ��� ��������
        Quaternion currentRotation = mainCamera.transform.rotation;

        // Y�� ȸ�� ������ ���� ���ʹϾ��� ������� ���
        float newYRotation = currentRotation.eulerAngles.y + 180f;

        // 360���� �Ѿ�� 0���� �ʱ�ȭ
        if (newYRotation >= 360f)
        {
            newYRotation -= 360f;
        }

        // X���� ���� ������, Y���� ���� ���� ȸ�� ������, Z���� ���� ������ ����
        mainCamera.transform.rotation = Quaternion.Euler(40f, newYRotation, 0f);
        mainCamera.transform.position = oppositePosition;
    }

    public void ResetTimerAndAnimation()
    {
        // Ÿ�̸� �ʱ�ȭ
        timer = 0f;

        // ������ �� �ִϸ��̼� �ʱ�ȭ
        StartCoroutine(PlayAnimationAfterDelay("Time", 0f));
    }

    private IEnumerator PlayAnimationAfterDelay(string animationName, float delay)
    {
        // ������
        yield return new WaitForSeconds(delay);

        // �ִϸ��̼� �ʱ�ȭ
        if (timerAnimator != null)
        {
            timerAnimator.Play(animationName);
        }
    }

    // OnSwitchTurn �̺�Ʈ�� ���� �� ����
    private void OnDisable()
    {
        DragAndShooting.OnSwitchTurn -= ResetTimerAndAnimation;
    }
}
