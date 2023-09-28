using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndShooting : MonoBehaviour
{
    private Rigidbody rb;
    private Camera mainCamera;
    private bool isDragging = false;
    private Vector3 dragStartPosition;

    public float throwForce = 100.0f;
    public float maxDragDistance = 15.0f;

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
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isDragging)
            {
                ThrowObject();
            }
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
                dragStartPosition = transform.position;
            }
        }
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
    }
}