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

    public float throwForce = 50.0f;
    public float maxDragDistance = 30.0f;
    public GameObject draggablePrefab;
    public GameObject arrowPrefab;

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
                clone.transform.rotation = Quaternion.Euler(0f, 0f, 0f); // 누워 있는 상태로 나오지 않도록 수정

                Collider cloneCollider = clone.GetComponent<Collider>();
                if (cloneCollider != null)
                {
                    cloneCollider.enabled = false;
                }

                // 드래그 시작할 때만 정면을 보도록 수정
                clone.transform.forward = mainCamera.transform.forward;

                // 드래그하는 방향의 반대 방향을 계산
                Vector3 dragDirection = (dragStartPosition - transform.position).normalized;

                // 화살의 방향을 설정하고 나서 x축과 z축의 회전값을 고정
                arrowInstance = Instantiate(arrowPrefab, clone.transform.position, Quaternion.LookRotation(Vector3.down, dragDirection));
                arrowInstance.transform.rotation = Quaternion.Euler(-90f, 0f, -90f);

            }
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

            // 클론 오브젝트를 드래그 시작 위치에 고정
            if (clone != null)
            {
                clone.transform.position = dragStartPosition;

                // 클론 오브젝트가 마우스 방향을 따라 같이 회전
                Vector3 dragDirection = (dragStartPosition - transform.position).normalized;

                // y축 회전값을 기준으로 돌도록 수정
                float angle = Mathf.Atan2(dragDirection.x, dragDirection.z) * Mathf.Rad2Deg;
                Quaternion newRotation = Quaternion.Euler(0, angle, 0);

                // 현재 클론 오브젝트의 회전값을 유지한 채로 새로운 회전값으로 Lerp
                clone.transform.rotation = Quaternion.Lerp(clone.transform.rotation, newRotation, Time.deltaTime * 10f);

                // 원래 오브젝트도 같이 회전
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

        // 바로 보는 방향을 유지
        transform.forward = throwDirection;

        transform.position = dragStartPosition;

        Collider cylinderCollider = GetComponent<Collider>();
        if (cylinderCollider != null)
        {
            cylinderCollider.enabled = true;
        }
        Destroy(arrowInstance);

        Destroy(clone);
    }
}