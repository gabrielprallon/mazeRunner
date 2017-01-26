using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class wallBehavior : MonoBehaviour {

    private bool isWallR = false;
    private bool isWallL = false;
    private bool isWallF = false;
    private bool isWallB = false;
    private RaycastHit hitR;
    private RaycastHit hitL;
    private RaycastHit hitF;
    private RaycastHit hitB;
    public int jumpRunCount = 0;
    public int jumpWallCount = 0;
    private RigidbodyFirstPersonController cc;
    private Rigidbody rb;
    public float runTime = 0.5f;
    public float minVelWallRun = 0.1f;


	void Start () {

        
        cc = GetComponent<RigidbodyFirstPersonController>();
        rb = GetComponent<Rigidbody>();

	}
	

	void Update () {
	    if (cc.Grounded)
        {
            jumpRunCount = 0;
            jumpWallCount = 0;
        }
        wallRun();

        wallJump();
        
	}
    void wallRun()
    {
        float magnitude = Mathf.Sqrt(Mathf.Pow(cc.Velocity.x, 2) + Mathf.Pow(cc.Velocity.z, 2));
        if (!cc.Grounded && jumpRunCount <= 1 && magnitude > minVelWallRun)
        {
            if (Physics.Raycast(transform.position, -transform.right, out hitL, 1))
            {
                if (hitL.transform.tag == "Wall")
                {
                    isWallL = true;                    
                    jumpRunCount += 1;
                    jumpWallCount = 0;
                    rb.useGravity = false;
                    StartCoroutine(afterRun());
                }
            }
            if (Physics.Raycast(transform.position, transform.right, out hitR, 1))
            {
                if (hitR.transform.tag == "Wall")
                {
                    Debug.Log("hit wall other left");
                    isWallR = true;                    
                    jumpRunCount += 1;
                    jumpWallCount = 0;
                    rb.useGravity = false;
                    StartCoroutine(afterRun());
                }
            }
            if (Physics.Raycast(transform.position, transform.forward, out hitF, 1))
            {
                if (hitF.transform.tag == "Wall")
                {
                    Debug.Log("hit wall frontal left");
                    isWallF = true;
                    jumpRunCount += 1;
                    jumpWallCount = 0;
                    rb.useGravity = false;
                    StartCoroutine(afterRun());
                }
            }
        }
    }
    private void wallJump()
    {
        Debug.DrawRay(transform.position, transform.right);
        if (!cc.Grounded && jumpWallCount <= 1 )
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (Physics.Raycast(transform.position, transform.forward, out hitF, 1))
                {
                    if (hitF.transform.tag == "Wall")
                    {
                        isWallF = true;
                        jumpWallCount += 1;
                        jumpRunCount = 0;
                        rb.useGravity = true;
                        rb.drag = 0f;
                        rb.velocity = Vector3.zero;
                        rb.AddForce((hitF.normal + transform.up) * cc.movementSettings.JumpForce, ForceMode.Impulse);
                        return;

                    }
                 }
                if (Physics.Raycast(transform.position, -transform.forward, out hitB, 1))
                {
                    if (hitB.transform.tag == "Wall")
                    {
                        isWallB = true;
                        jumpWallCount += 1;
                        jumpRunCount = 0;
                        rb.useGravity = true;
                        rb.drag = 0f;
                        rb.velocity = Vector3.zero;
                        rb.AddForce((hitB.normal + transform.up) * cc.movementSettings.JumpForce, ForceMode.Impulse);
                        return;

                    }
                }
                if (Physics.Raycast(transform.position, transform.right, out hitR, 1))
                {
                    if (hitR.transform.tag == "Wall")
                    {
                        isWallR = true;
                        jumpWallCount += 1;
                        jumpRunCount = 0;
                        rb.useGravity = true;
                        rb.drag = 0f;
                        rb.velocity = Vector3.zero;
                        rb.AddForce((hitR.normal + transform.up ) * cc.movementSettings.JumpForce, ForceMode.Impulse);
                        return;

                    }
                }
                if (Physics.Raycast(transform.position, -transform.right, out hitL, 1))
                {
                    if (hitL.transform.tag == "Wall")
                    {
                        isWallL = true;
                        jumpWallCount += 1;
                        jumpRunCount = 0;
                        rb.useGravity = true;
                        rb.drag = 0f;
                        rb.velocity = Vector3.zero;
                        rb.AddForce((hitL.normal + transform.up) * cc.movementSettings.JumpForce, ForceMode.Impulse);
                        return;

                    }
                }
            }
        }

    }

    IEnumerator afterRun()
    {
        yield return new WaitForSeconds(runTime);
        isWallL = false;
        isWallR = false;
        isWallF = false;
        rb.useGravity = true;
    }
}
