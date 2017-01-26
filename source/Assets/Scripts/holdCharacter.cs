using UnityEngine;
using System.Collections;

public class holdCharacter : MonoBehaviour {

    // Use this for initialization
    public bool isKinectic = false;
    public blockMovement block;

    void Start()
    {
        block = gameObject.GetComponent<blockMovement>();
    }



    void OnCollisionEnter(Collision cc)
    {
        if (cc.gameObject.tag == "Player")
        {
            if (gameObject.transform.position.y < cc.transform.position.y)
                cc.transform.parent = gameObject.transform;
        }
    }
    void OnCollisionExit(Collision cc)
    {
        if (cc.gameObject.tag == "Player")
        {
            cc.transform.parent = null;
            if (isKinectic)
            {
                Vector3 aux = Vector3.zero;
                aux.x = block.blockPosSpeed.x * block.moveDirection.x;
                aux.y = block.blockPosSpeed.y * block.moveDirection.y;
                aux.z = block.blockPosSpeed.z * block.moveDirection.z;
                cc.gameObject.GetComponent<Rigidbody>().AddForce((aux*cc.gameObject.GetComponent<Rigidbody>().mass*50));
            }
        }
        
    }
}
