using UnityEngine;
using System.Collections;

public class gameManager : MonoBehaviour {

    public GameObject player;
    public Vector3 startPos;
    public Vector3 currentCheckPoint;
    public bool start = true;

    void Start()
    {
        if (start)
        {
            startPos = player.transform.position;
        }
        player.transform.position = startPos;
        currentCheckPoint = startPos;
    }

    public void respawnPlayer()
    {
        player.transform.position = currentCheckPoint;
    }
    public void saveCheckPoint()
    {
        currentCheckPoint = player.transform.position;
    }
}
