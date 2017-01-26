using UnityEngine;
using System.Collections;

public class killZone : MonoBehaviour {

    public Vector3 respawn;
    public GameObject player;

    public gameManager gm;

    void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<gameManager>();
    }

    public void OnTriggerEnter()
    {
        gm.respawnPlayer();
    } 
}
