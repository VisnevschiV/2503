using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class ThirdPersonMovement : MonoBehaviour
{
 
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Vector3 movedir;
//gravity
    Vector3 velocity;
    public float gravity=-9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
//ACTIONS
    public float jumpHeight;
    public float dashSpeed = 200;
    public float dashTime = 2f;
    bool inDash;
    void Start(){

    }
    // Update is called once per frame
    void Update()
    {
       
    //mooving basis  WASD
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(Horizontal,0f,Vertical).normalized;
    //gravity
        isGrounded = Physics.CheckSphere(groundCheck.position,groundDistance,groundMask);
        if(isGrounded && velocity.y<0){
            velocity.y = -2f;
        }
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y +=gravity *Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    //dash
       
            if (Input.GetKeyDown("left shift"))
                {
                Debug.Log("dash");
                StartCoroutine(Dash());
                }
    //moving based on camera code
        if(direction.magnitude >= 0.1f){
            float targetAngle = Mathf.Atan2(direction.x, direction.z)* Mathf.Rad2Deg+cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle,ref turnSmoothVelocity,turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f,angle,0f);
            movedir = Quaternion.Euler(0f,targetAngle,0f)*Vector3.forward;
            
        //movement  
            controller.Move(movedir.normalized *speed * Time.deltaTime);
        }
    
    }

    IEnumerator Dash(){
        float startTime = Time.time;

        while(Time.time<startTime+dashTime){
            controller.Move(movedir*dashSpeed*Time.deltaTime);
            yield return null;
        }
    }
}
