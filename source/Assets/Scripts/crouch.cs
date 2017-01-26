using UnityEngine;
using System.Collections;

public class crouch : MonoBehaviour
{
        public float HeightMax = 1f;
        public float HeightMin = 0.8f;

        public float crouchVelocity = 3f;

        public bool getDown = false;
        public bool alreadyDown = false;
        public bool getUp = false;

        void Start()
        {

        }
        void Update()
        {

            if (Input.GetButtonDown("Crouch"))
            {
                getDown = true;
                getUp = false;

            }
            if (Input.GetButtonUp("Crouch"))
            {
                getDown = false;
                getUp = true;
            }
            if (getDown == true && alreadyDown == false)
            {
                if (gameObject.transform.localScale.y > HeightMin)
                {
                    gameObject.transform.localScale += new Vector3(0, -crouchVelocity * Time.deltaTime, 0);
                gameObject.transform.Translate(new Vector3(0, -crouchVelocity * Time.deltaTime, 0));              
                }
                else
                {
                    gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, HeightMin, gameObject.transform.localScale.z);
                    getDown = false;
                    alreadyDown = true;
                }
            }
            if (getDown == false && getUp == true)
            {
                alreadyDown = false;
                if (gameObject.transform.localScale.y < HeightMax)
            { 
                 gameObject.transform.localScale += new Vector3(0, crouchVelocity * Time.deltaTime, 0);
                gameObject.transform.Translate(new Vector3(0, crouchVelocity * Time.deltaTime, 0));
            }
                else
                {
                    gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, HeightMax, gameObject.transform.localScale.z);
                    getDown = false;
                    getUp = false;
                }
            }
        }
}

