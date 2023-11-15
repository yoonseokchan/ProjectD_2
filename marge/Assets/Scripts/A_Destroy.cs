using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Destroy : MonoBehaviour
{
    public string Marge;
    //public string Marge1;
    public GameObject Marged;
    private Vector3 MargedCPosition;


    public void OnCollisionEnter(Collision col)
    {  
        Debug.Log("부딛힘");
        //if (col.gameObject.name == Marge)
       // {
         //   col.gameObject.name = Marge += "1";
        
            if (col.gameObject.name == Marge)
            {  
                 Debug.Log("본인과 부딛힘");
                Vector3 MargedCPosition = col.contacts[0].point;
                Destroy(gameObject); // �ڱ��ڽ� ����
                Destroy(col.gameObject);
                
                GameObject newObject = (GameObject)Instantiate(Marged,MargedCPosition,Quaternion.identity);
                //지우는것만해놓고 만드는건 매니저만듬 그럼 해결가능함
            }
       // }
    }
}
