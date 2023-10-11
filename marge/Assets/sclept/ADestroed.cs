using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADestroed : MonoBehaviour {
    public GameObject B;
    public Vector3 BCPosition;
    void Start()
        {
            Vector3 BCPosition = transform.position;
        }
    public void OnCollisionEnter(Collision col) 
    {
        if (col.gameObject.name == "A") 
        {
            Destroy(gameObject); // 자기자신 삭제
            Destroy(col.gameObject); // 상대방을 삭제
            Instantiate(B,BCPosition,Quaternion.identity);
        }
    }
    
}