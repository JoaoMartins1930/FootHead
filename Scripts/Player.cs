using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour {

    private Rigidbody myBody;
    private float speed = 5;
    public float jumpPower;
    private AudioSource som; // som de maneira correta
    private AudioSource som2;

    public Animator animator;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private bool grounded;
    private float spawnPosY;

    Vector3 originalPos;

    // Kick
    public bool kicking = false;

    [System.Serializable]
    public class myBoolEvent : UnityEvent<bool> {}

    [SerializeField]
    public myBoolEvent onLandEvent;



    [SerializeField]
    private string InputMov;

    [SerializeField]
    private string InputJump;

    [SerializeField]
    private string InputKick;

    //private bool golo = false;

    private float timeLeft = 2f;
    private float timeVelocity = 5f;


    Vector3 vel;

    

    public enum playerStates
    {
        normal = 0,
        stun = 1,
        win = 2,
        bonus = 3
       
    }
    private playerStates currentState;

    // Use this for initialization
    void Start() {
        myBody = gameObject.GetComponent<Rigidbody>();

        spawnPosY = gameObject.transform.position.y;

        originalPos = gameObject.transform.position;

        currentState = playerStates.normal;

        som = gameObject.GetComponent<AudioSource>();

        som2 = gameObject.GetComponent<AudioSource>();

        gameObject.SetActive(true);

        

    }



    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case playerStates.normal:
                animator.SetBool("isStun", false);
                animator.SetBool("isStun2", false);
                manageMove();
                break;
            case playerStates.stun:
                animator.SetBool("isStun", true);
                animator.SetBool("isStun2", true);
                som2.Play();
                timeLeft -= Time.deltaTime;
                
                if (timeLeft <= 0)
                {
                    ChangeState(playerStates.normal);
                }
                break;
            case playerStates.bonus:
                animator.SetBool("isStun", false);
                animator.SetBool("isStun2", false);
                timeVelocity -= Time.deltaTime;
                if (timeVelocity <= 0)
                {
                    ChangeState(playerStates.normal);
                }
                else
                {
                    ChangeVelocity();
                    manageMove();
                }
                break;
            case playerStates.win:

                break;
            
        }

        vel = myBody.velocity;
       
        
        if (vel.y < 0)
        {
           
            myBody.velocity += myBody.velocity * 10f * Time.deltaTime;
            myBody.velocity = new Vector3(Mathf.Clamp(myBody.velocity.x, -10, 10), Mathf.Max(myBody.velocity.y, -10), myBody.velocity.z);

          
        }
        else
        {
            
        }
    }

    // Update is called once per frame
    private void manageMove() {


        

        
        float move = Input.GetAxis(InputMov);
        myBody.velocity = new Vector2(move * speed, myBody.velocity.y);

        animator.SetFloat("speed", Mathf.Abs(move)); // codigo para sprite
        animator.SetFloat("speed2", Mathf.Abs(move));


        if (gameObject.transform.position.y == spawnPosY)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        if (Input.GetButton(InputJump) && grounded == true)
        {
            myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
           // animator.SetBool("isJumping", true);
            
            som.Play();

        }

      

        if (myBody.velocity.y < 0)
        {
            myBody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (myBody.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            myBody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (Input.GetButton(InputKick))
        {
            Kick();
        }
        else
        {
            StopKicking();
        }

        //initialPos();
    }




    public void OnLanding()
    {
       animator.SetBool("isJumping", false);
    }

    // Sets the kicking state to true
    private void Kick()
    {
        if (!kicking)
        {
            kicking = true;
            animator.SetBool("isShooting", true);
            animator.SetBool("isShooting2", true);

        }
    }

    // Sets the kicking state to false
    private void StopKicking()
    {
        kicking = false;
        animator.SetBool("isShooting", false);
        animator.SetBool("isShooting2", false);


    }

    public void InitialPos(bool goal)
    {
        if (goal)
        {
            
            gameObject.transform.position = originalPos;
        }
        else
        {
            print("nada");
        }
    }

    public void ChangeState(playerStates _newState)
    {
        print(_newState);

        if (currentState == _newState) return;

        if (currentState == playerStates.normal)
        {
            if (_newState == playerStates.stun)
            {
               

                myBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ |
                RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

                currentState = playerStates.stun;
                

                timeLeft = 2f;
            }
            else if (_newState == playerStates.bonus)
            {
                myBody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ |
                RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

                currentState = playerStates.bonus;
                timeVelocity = 5f;
            }

        }
        else if (currentState == playerStates.bonus)
        {
            myBody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ |
                RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
            if (_newState == playerStates.normal)
            {
                myBody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ |
                RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

                currentState = playerStates.normal;
                speed = 5;
            }
            else if (_newState == playerStates.stun)
            {
               
                myBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ |
                RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

                currentState = playerStates.stun;
                
                timeLeft = 2f;
            }
        }
        else if (currentState == playerStates.stun)
        {
           
            if (_newState == playerStates.normal)
            {
                myBody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ |
                RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

                currentState = playerStates.normal;
                speed = 5;
            }
            else if (_newState == playerStates.bonus)
            {
                myBody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ |
                RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

                currentState = playerStates.bonus;
                timeVelocity = 5f;
            }
        }
    }

    public void ChangeVelocity()
    {
        speed = 10;
    }


    void OnTriggerEnter(Collider other)
    {
        
        som2.Play();
    }

}
