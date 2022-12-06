using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public CharacterController controller;
    public float walkingSpeed;
    public float runningSpeed;
    public float dashSpeed;
    public Rigidbody rb;
    public float jumpHeight;
    public float gravity = -9.81f;
    public float lastClickTime;
    Vector3 velocity;
    private float speed;
    public static bool dash;
    public Transform gorundCheck;
    public float groundDistance;
    public LayerMask mask;
    bool grounded;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dash = false;
    }

    // Update is called once per frame
    void Update()
    {

        grounded = Physics.CheckSphere(gorundCheck.position, groundDistance,mask);
        if(grounded && velocity.y<0){
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 move = transform.right * -y + transform.forward * x;
        velocity.y +=gravity *Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
        controller.Move(velocity * Time.deltaTime);
        
        if(!dash){
            if (Input.GetKey("left shift"))
            {
               speed = runningSpeed;
            }else{
                speed = walkingSpeed;
            }
        }
        if (Input.GetKeyDown("left shift"))
        {
            if(Time.time-lastClickTime<0.8){
                Debug.Log("Dash");
                speed = dashSpeed;
                dash= true;
                Invoke("stopDash", 1);
            }
            lastClickTime = Time.time;
        }
        controller.Move(move * speed * Time.deltaTime);
    }

    void stopDash(){
        dash = false;
    }
}

