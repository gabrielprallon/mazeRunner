using UnityEngine;
using System.Collections;

public class blockMovement : MonoBehaviour
{

    public gameManager gm;


    /////////////////////////////////////////////
    /////////      Variaveis Bloco     //////////
    /////////////////////////////////////////////

    public bool hasMovement = false;
    public bool hasRotation = false;
    public bool startPosition = true;
    public bool isCheckPoint = false;
    public Rigidbody rb;

    [Header("Posicao")]
    //movimento
    public Vector3 startPos;
    public Vector3 moveDirection;
    public Vector3 minPos;
    public Vector3 maxPos;

    [Header("rotação")]
    public Vector3 currentRotation;
    public Vector3 rotationDirection;
    public Vector3 minRotation;
    public Vector3 maxRotation;
    public Vector3 rotationAmount = Vector3.zero;
    private Vector3 currentRotationCount = Vector3.zero;

    [Header("Tipo de movimento")]
    public bool singleMove;
    public bool singleSpin;
    //public bool positiveFirst;

    [Header("atributos de movimento")]

    //tempo
    [Range(0f, 60f)]
    public float warmUpTimePos = 0.1f;
    [Range(0f, 60f)]
    public float warmUpTimeRot = 0.1f;
    private bool move = false;
    private bool rot = false;
    private float startTimePos = 0f;
    private float startTimeRot = 0f;

    //velocidades
    public Vector3 blockPosSpeed;
    public Vector3 blockRotSpeed;

    float stop = 0f;

    [Header("Random")]
    public bool randomMovement = false;
    [Range(0f, 60f)]
    public float movRange = 0.1f;
    public bool randomRotation = false;
    [Range(0f, 360f)]
    public float rotRange = 0.1f;

    //////////////////////////////////////////////
    //////////////////////////////////////////////
    //////////////////////////////////////////////




    // Use this for initialization   
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        if (startPosition)
            startPos = gameObject.transform.position;
        currentRotation = gameObject.transform.rotation.eulerAngles;
        if (randomMovement)
        {
            blockPosSpeed = new Vector3(Random.Range(0f, movRange), Random.Range(0f, movRange), Random.Range(0f, movRange));
            moveDirection = new Vector3(Random.Range((int)-1, (int)1), Random.Range((int)-1, (int)1), Random.Range((int)-1, (int)1));
            minPos = new Vector3(Random.Range(startPos.x - movRange, startPos.x), Random.Range(startPos.y - movRange, startPos.y), Random.Range(startPos.z - movRange, startPos.z));
            maxPos = new Vector3(Random.Range(startPos.x, startPos.x + movRange), Random.Range(startPos.y, startPos.y + movRange), Random.Range(startPos.z, startPos.z + movRange));
        }
        StartCoroutine(waitActivationMove());
        StartCoroutine(waitActivationRot());

    }
    void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<gameManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (hasMovement)
        {
            if (move)
            {
                if (singleMove == true)
                    blockSingleMovement();
                else
                    pingPongMovement();
            }
        }
        if (hasRotation)
        {
            if (rot)
            {
                if (singleSpin == true)
                    singleRotation();
                else
                    pingPongRotation();
            }
        }


    }

    IEnumerator waitActivationMove()
    {
        yield return new WaitForSeconds(warmUpTimePos);
        move = true;
    }

    IEnumerator waitActivationRot()
    {
        yield return new WaitForSeconds(warmUpTimeRot);
        rot = true;
    }

    ///////////////////////////////////////
    ///////// Funcoes de movimento ////////
    ///////////////////////////////////////


    //funçao de movimento unico
    public void blockSingleMovement()
    {

        Vector3 currentPos = gameObject.transform.position;
        Vector3 movement = moveDirection;

        // movimento em x
        if (moveDirection.x < 0)
        {
            if (currentPos.x > minPos.x)
            {
                rb.AddForce(-transform.right * blockPosSpeed.x, ForceMode.VelocityChange);

                ///    rb.velocity.x.Equals(-blockPosSpeed.x);
            }

            else
                rb.velocity.x.Equals(stop);

        }
        else
        {
            if (currentPos.x < maxPos.x)
                rb.AddForce(transform.right * blockPosSpeed.x, ForceMode.VelocityChange);
            //rb.velocity.x.Equals(blockPosSpeed.x);
            else
                rb.velocity.x.Equals(stop);
        }

        // movimento em y
        if (moveDirection.y < 0)
        {
            if (currentPos.y > minPos.y)
                rb.AddForce(-transform.up * blockPosSpeed.x, ForceMode.VelocityChange);
            else
                rb.velocity.y.Equals(stop);

        }
        else
        {
            if (currentPos.y < maxPos.y)
                rb.AddForce(transform.up * blockPosSpeed.y, ForceMode.VelocityChange);
            else
                rb.velocity.y.Equals(stop);
        }

        // movimento em z
        if (moveDirection.z < 0)
        {
            if (currentPos.z > minPos.z)
                rb.AddForce(-transform.forward * blockPosSpeed.z, ForceMode.VelocityChange);
            else
                rb.velocity.z.Equals(stop);

        }
        else
        {
            if (currentPos.z < maxPos.z)
                rb.AddForce(transform.forward * blockPosSpeed.z, ForceMode.VelocityChange);
            else
                rb.velocity.z.Equals(stop);
        }

        
    }

    //movimento ping pong
    public void pingPongMovement()
    {
        Vector3 currentPos = gameObject.transform.position;
        //Vector3 movement = moveDirection;
        Vector3 currentDir = moveDirection;

        // movimento em x

        //if (moveDirection.x < 0)
        // {

        //  if (currentPos.x <= minPos.x)
        // {
        //     moveDirection.x *= -1;
        //     movement.x *= -1;

        //   }

        //}
        //  else
        //  {
        //   
        //  if (currentPos.x >= maxPos.x)
        //  {
        //      moveDirection.x *= -1;
        //    movement.x *= -1;
        //   }
        // }

        // movimento em y
        // if (moveDirection.y < 0)
        //{
        // if (currentPos.y <= minPos.y)
        //  {
        //      moveDirection.y *= -1;
        //      movement.y *= -1;
        //   }

        // }
        //   else
        //{
        //     if (currentPos.y >= maxPos.y)
        //     {
        //         moveDirection.y *= -1;
        //        movement.y *= -1;
        //     }
        // }

        // movimento em z
        // if (moveDirection.z < 0)
        // {
        //    if (currentPos.z <= minPos.z)
        //    {
        //       moveDirection.z *= -1;
        //        movement.z *= -1;
        //  }

        //}
        //else
        //{
        //    if (currentPos.z >= maxPos.z)
        //    {
        //        moveDirection.z *= -1;
        //       movement.z *= -1;
        //    }
        // }


        // movement.x *= blockPosSpeed.x;
        //movement.y *= blockPosSpeed.y;
        //movement.z *= blockPosSpeed.z;

        // transform.Translate(movement * Time.deltaTime, Space.World);

        //NEW SCRIPT <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        // X
        if (currentDir.x < 0)
        {
            if (currentPos.x <= minPos.x)
            {
                currentDir *= -1;
            }
            else
            {
                blockSingleMovement();
            }
        }
        else
        {
            if (currentPos.x >= maxPos.x)
            {
                currentDir *= -1;
            }
            else
            {
                blockSingleMovement();
            }
        }
        // Y
        if (currentDir.y < 0)
        {
            if (currentPos.y <= minPos.y)
            {
                currentDir *= -1;
            }
            else
            {
                blockSingleMovement();
            }
        }
        else
        {
            if (currentPos.y >= maxPos.y)
            {
                currentDir *= -1;
            }
            else
            {
                blockSingleMovement();
            }
        }
        // Z
        if (currentDir.z < 0)
        {
            if (currentPos.z <= minPos.z)
            {
                currentDir *= -1;
            }
            else
            {
                blockSingleMovement();
            }
        }
        else
        {
            if (currentPos.z >= maxPos.z)
            {
                currentDir *= -1;
            }
            else
            {
                blockSingleMovement();
            }
        }


        ajustPosition();


    }

    public void ajustPosition()
    {
        Vector3 newPosition = transform.position;
        if (moveDirection.x > 0) {
            if (newPosition.x > maxPos.x) { 
                newPosition.x = maxPos.x;
                moveDirection.x *= -1 ;
            }
        } else {
            if (newPosition.x < minPos.x) { 
                newPosition.x = minPos.x;
                moveDirection.x *= -1;
            }

        }
        if (moveDirection.y > 0) {
            if (newPosition.y > maxPos.y){
                newPosition.y = maxPos.y;
                moveDirection.y *= -1;
            }
        } else {
            if (newPosition.y < minPos.y){
                newPosition.y = minPos.y;
                moveDirection.y *= -1;
            }

        }
        if (moveDirection.z > 0){
            if (newPosition.z > maxPos.z)
            {
                newPosition.z = maxPos.z;
                moveDirection.z *= -1;
            }
        }
        else
        {
            if (newPosition.z < minPos.z)
            {
                newPosition.z = minPos.z;
                moveDirection.z *= -1;
            }

        }
        transform.position = newPosition;
    }

    // Rotacao

    public void singleRotation()
    {
        Vector3 currentPos = currentRotation;
        Vector3 movement = rotationDirection;
        
        // movimento em x
        if (rotationDirection.x < 0)
        {
            if (currentPos.x > minRotation.x - 360 * (rotationAmount.x -1))
            {
                movement.x *= blockRotSpeed.x;
            }
            else
                movement.x *= 0;
            if ((currentPos.x + movement.x * Time.deltaTime < minRotation.x - 360 * (rotationAmount.x - 1)) && movement.x != 0)
            {
                movement.x = ((minRotation.x - 360 * (rotationAmount.x - 1)) - currentPos.x) / Time.deltaTime;
            }

        }
        else
        {
            
            if (currentPos.x < maxRotation.x + 360 * (rotationAmount.x -1))
                movement.x *= blockRotSpeed.x;
            else
                movement.x *= 0;
            
            if ((currentPos.x + movement.x * Time.deltaTime > maxRotation.x + 360 * (rotationAmount.x - 1)) && movement.x != 0)
            {
               movement.x = ((maxRotation.x + 360 * (rotationAmount.x - 1)) - currentPos.x) / Time.deltaTime;               
            }
        }

        // movimento em y
        if (rotationDirection.y < 0)
        {
            if (currentPos.y > minRotation.y - 360 * (rotationAmount.y - 1))
            {
                movement.y *= blockRotSpeed.y;
            }
            else
                movement.y *= 0;
            if ((currentPos.y + movement.y * Time.deltaTime < minRotation.y - 360 * (rotationAmount.y - 1)) && movement.y != 0)
            {
                movement.y = ((minRotation.y - 360 * (rotationAmount.y - 1)) - currentPos.y) / Time.deltaTime;
            }

        }
        else
        {

            if (currentPos.y < maxRotation.y + 360 * (rotationAmount.y - 1))
                movement.y *= blockRotSpeed.y;
            else
                movement.y *= 0;

            if ((currentPos.y + movement.y * Time.deltaTime > maxRotation.y + 360 * (rotationAmount.y - 1)) && movement.y != 0)
            {
                movement.y = ((maxRotation.y + 360 * (rotationAmount.y - 1)) - currentPos.y) / Time.deltaTime;
            }
        }

        // movimento em z
        if (rotationDirection.z < 0)
        {
            if (currentPos.z > minRotation.z - 360 * (rotationAmount.z - 1))
            {
                movement.z *= blockRotSpeed.z;
            }
            else
                movement.z *= 0;
            if ((currentPos.z + movement.z * Time.deltaTime < minRotation.z - 360 * (rotationAmount.z - 1)) && movement.z != 0)
            {
                movement.z = ((minRotation.z - 360 * (rotationAmount.z - 1)) - currentPos.z) / Time.deltaTime;
            }

        }
        else
        {

            if (currentPos.z < maxRotation.z + 360 * (rotationAmount.z - 1))
                movement.z *= blockRotSpeed.z;
            else
                movement.z *= 0;

            if ((currentPos.z + movement.z * Time.deltaTime > maxRotation.z + 360 * (rotationAmount.z - 1)) && movement.z != 0)
            {
                movement.z = ((maxRotation.z + 360 * (rotationAmount.z - 1)) - currentPos.z) / Time.deltaTime;
            }
        }

        
        currentRotationCount.x += movement.x * Time.deltaTime;
        currentRotationCount.y += movement.y * Time.deltaTime;
        currentRotationCount.z += movement.z * Time.deltaTime;

        currentRotation.x += movement.x * Time.deltaTime;
        currentRotation.y += movement.y * Time.deltaTime;
        currentRotation.z += movement.z * Time.deltaTime;

        transform.Rotate(Vector3.right, movement.x * Time.deltaTime, Space.Self);
        transform.Rotate(Vector3.up, movement.y * Time.deltaTime, Space.Self);
        transform.Rotate(Vector3.forward, movement.z * Time.deltaTime, Space.Self);

    } 

    public void pingPongRotation()
    {
        Vector3 currentPos = currentRotation;
        Vector3 movement = rotationDirection;

        // movimento em x
        if (rotationDirection.x < 0)
        {
            if (currentPos.x > minRotation.x - 360 * (rotationAmount.x - 1))
            {
                movement.x *= blockRotSpeed.x;
            }
            else
            {
                movement.x *= -1;
                rotationDirection.x *= -1;
            }
            if ((currentPos.x + movement.x * Time.deltaTime < minRotation.x - 360 * (rotationAmount.x - 1)) && movement.x != 0)
            {
                movement.x = ((minRotation.x - 360 * (rotationAmount.x - 1)) - currentPos.x) / Time.deltaTime;
            }

        }
        else
        {

            if (currentPos.x < maxRotation.x + 360 * (rotationAmount.x - 1))
                movement.x *= blockRotSpeed.x;
            else {
                movement.x *= -1;
                rotationDirection.x *= -1;
            }
                

            if ((currentPos.x + movement.x * Time.deltaTime > maxRotation.x + 360 * (rotationAmount.x - 1)) && movement.x != 0)
            {
                movement.x = ((maxRotation.x + 360 * (rotationAmount.x - 1)) - currentPos.x) / Time.deltaTime;
            }
        }

        // movimento em y
        if (rotationDirection.y < 0)
        {
            if (currentPos.y > minRotation.y - 360 * (rotationAmount.y - 1))
            {
                movement.y *= blockRotSpeed.y;
            }
            else {
                movement.y *= -1;
                rotationDirection.y *= -1;
            }
                
            if ((currentPos.y + movement.y * Time.deltaTime < minRotation.y - 360 * (rotationAmount.y - 1)) && movement.y != 0)
            {
                movement.y = ((minRotation.y - 360 * (rotationAmount.y - 1)) - currentPos.y) / Time.deltaTime;
            }

        }
        else
        {

            if (currentPos.y < maxRotation.y + 360 * (rotationAmount.y - 1))
                movement.y *= blockRotSpeed.y;
            else
            {
                movement.y *= -1;
                rotationDirection.y *= -1;
            }

            if ((currentPos.y + movement.y * Time.deltaTime > maxRotation.y + 360 * (rotationAmount.y - 1)) && movement.y != 0)
            {
                movement.y = ((maxRotation.y + 360 * (rotationAmount.y - 1)) - currentPos.y) / Time.deltaTime;
            }
        }

        // movimento em z
        if (rotationDirection.z < 0)
        {
            if (currentPos.z > minRotation.z - 360 * (rotationAmount.z - 1))
            {
                movement.z *= blockRotSpeed.z;
            }
            else
            {
                movement.z *= -1;
                rotationDirection.z *= -1;
            }
            if ((currentPos.z + movement.z * Time.deltaTime < minRotation.z - 360 * (rotationAmount.z - 1)) && movement.z != 0)
            {
                movement.z = ((minRotation.z - 360 * (rotationAmount.z - 1)) - currentPos.z) / Time.deltaTime;
            }

        }
        else
        {

            if (currentPos.z < maxRotation.z + 360 * (rotationAmount.z - 1))
                movement.z *= blockRotSpeed.z;
            else
            {
                movement.z *= -1;
                rotationDirection.z *= -1;
            }

            if ((currentPos.z + movement.z * Time.deltaTime > maxRotation.z + 360 * (rotationAmount.z - 1)) && movement.z != 0)
            {
                movement.z = ((maxRotation.z + 360 * (rotationAmount.z - 1)) - currentPos.z) / Time.deltaTime;
            }
        }

        
        currentRotationCount.x += movement.x * Time.deltaTime;
        currentRotationCount.y += movement.y * Time.deltaTime;
        currentRotationCount.z += movement.z * Time.deltaTime;

        currentRotation.x += movement.x * Time.deltaTime;
        currentRotation.y += movement.y * Time.deltaTime;
        currentRotation.z += movement.z * Time.deltaTime;

        transform.Rotate(Vector3.right, movement.x * Time.deltaTime, Space.Self);
        transform.Rotate(Vector3.up, movement.y * Time.deltaTime, Space.Self);
        transform.Rotate(Vector3.forward, movement.z * Time.deltaTime, Space.Self);
    }

    void OnCollisionEnter(Collision col)
    {
        if (isCheckPoint == true)
        {
            gm.saveCheckPoint();
        }
    }
}

