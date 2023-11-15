using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndShooting : MonoBehaviour
{
    private Rigidbody rb;
    private Camera mainCamera;
    private bool isDragging = false;
    private Vector3 dragStartPosition;
    private GameObject clone;
    private GameObject arrowInstance;
    private Vector3 throwForceDirection;

    public float throwForce = 50.0f;
    public float maxDragDistance = 30.0f;

    public GameObject draggablePrefab;
    public GameObject arrowPrefab;
    public GameObject MargePrefab;

    float currentYRotation = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDragging();
        }

        if (isDragging)
        {
            UpdateDrag();
            UpdateArrow();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isDragging)
            {
                ThrowObject();
            }
        }

        if (isDragging && Input.GetMouseButtonDown(1))
        {
            CancelDrag();
        }

    }

    void StartDragging()
{
    try
    {
        // ���� �ڵ�
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                isDragging = true;
                rb.isKinematic = true;

                Collider cylinderCollider = GetComponent<Collider>();
                if (cylinderCollider != null)
                {
                    cylinderCollider.enabled = false;
                }

                dragStartPosition = transform.position;

                clone = Instantiate(draggablePrefab, dragStartPosition, Quaternion.identity);
                clone.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

                Collider cloneCollider = clone.GetComponent<Collider>();
                if (cloneCollider != null)
                {
                    cloneCollider.enabled = false;
                }

                clone.transform.forward = mainCamera.transform.forward;

                Vector3 dragDirection = (dragStartPosition - transform.position).normalized;

                arrowInstance = Instantiate(arrowPrefab, clone.transform.position, Quaternion.LookRotation(Vector3.down, dragDirection));
                arrowInstance.transform.rotation = Quaternion.Euler(-90f, 0f, -90f);
            }
        }
    }
    catch (System.Exception ex)
    {
        Debug.LogError("Exception in StartDragging: " + ex.Message);
        throw; // ���ܸ� �ٽ� ������ �ֿܼ� �� �ڼ��� ������ ����ϵ��� ��
    }
}


    void CancelDrag()
    {
        isDragging = false;
        rb.isKinematic = false;

        Collider cylinderCollider = GetComponent<Collider>();
        if (cylinderCollider != null)
        {
            cylinderCollider.enabled = true;
        }

        transform.position = dragStartPosition;

        Destroy(arrowInstance);
        Destroy(clone);
    }

    void UpdateDrag()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);
        float distanceToPlane;

        if (plane.Raycast(ray, out distanceToPlane))
        {
            Vector3 newPosition = ray.GetPoint(distanceToPlane);
            transform.position = newPosition;

            // Ŭ�� ������Ʈ�� �巡�� ���� ��ġ�� ����
            if (clone != null)
            {
                clone.transform.position = dragStartPosition;

                // Ŭ�� ������Ʈ�� ���콺 ������ ���� ���� ȸ��
                Vector3 dragDirection = (dragStartPosition - transform.position).normalized;

                // y�� ȸ������ �������� ������ ����
                float angle = Mathf.Atan2(dragDirection.x, dragDirection.z) * Mathf.Rad2Deg;
                Quaternion newRotation = Quaternion.Euler(0, angle, 0);

                // ���� Ŭ�� ������Ʈ�� ȸ������ ������ ä�� ���ο� ȸ�������� Lerp
                clone.transform.rotation = Quaternion.Lerp(clone.transform.rotation, newRotation, Time.deltaTime * 10f);

                // ���� ������Ʈ�� ���� ȸ��
                transform.rotation = clone.transform.rotation;
            }
        }
    }

    void UpdateArrow()
    {
        if (arrowInstance != null)
        {
            Vector3 direction = (clone.transform.position - transform.position).normalized;
            arrowInstance.transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    void ThrowObject()
    {
        isDragging = false;
        rb.isKinematic = false;

        float dragDistance = Vector3.Distance(dragStartPosition, transform.position);
        float throwForceMagnitude = Mathf.Lerp(0f, throwForce, dragDistance / maxDragDistance);

        Vector3 throwDirection = (dragStartPosition - transform.position).normalized;
        rb.velocity = throwDirection * throwForceMagnitude;

        // �ٷ� ���� ������ ����
        transform.forward = throwDirection;

        transform.position = dragStartPosition;

        Collider cylinderCollider = GetComponent<Collider>();
        if (cylinderCollider != null)
        {
            cylinderCollider.enabled = true;
        }
        Destroy(arrowInstance);

        Destroy(clone);

        // ���� ����� ���� ����
        throwForceDirection = (dragStartPosition - transform.position).normalized;
        float throwDistance = Vector3.Distance(dragStartPosition, transform.position);
        float throwMagnitude = Mathf.Lerp(0f, throwForce, throwDistance / maxDragDistance);
        Vector3 throwDir = throwForceDirection * throwMagnitude;

        // �ٸ� ��ũ��Ʈ�� ���� ����
        PushObjects pushObjectsScript = GetComponent<PushObjects>();
        if (pushObjectsScript != null)
        {
            pushObjectsScript.ApplyPushForce(throwDir);
        }
        // 5�� �Ŀ� CameraRotate �޼��带 ȣ��
        Invoke("SwitchTurns", 3f);
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
        mainCamera.transform.rotation = Quaternion.Euler(61f, newYRotation, 0f);
        mainCamera.transform.position = oppositePosition;
    }
}