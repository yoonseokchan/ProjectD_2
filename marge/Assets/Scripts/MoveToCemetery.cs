using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToCemetery : MonoBehaviour
{
    public Transform cemeteryPlane;
    private bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered!");

        Debug.Log("Collided object name: " + other.gameObject.name);
        Debug.Log("Collided object tag: " + other.gameObject.tag);

        if (other.CompareTag("GameSceneObject"))
        {
            Debug.Log("GameSceneObject entered!");

            other.transform.position = cemeteryPlane.position;

            GetComponent<Collider>().enabled = false;
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (isTriggered && other.CompareTag("GameSceneObject"))
        {
            isTriggered = false;

            GetComponent<Collider>().enabled = true;
        }
    }
}