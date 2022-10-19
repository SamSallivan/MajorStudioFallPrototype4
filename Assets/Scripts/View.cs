using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Input/Output.
// Makes no decisions: always asks the controller.
public class View : MonoBehaviour
{
    // Unity components
    CharacterController characterController;
    public bool isActive;
    public Transform playerHead;
    public float startRotationY;

    // MVC
    public Model model;
    private Controller controller;
    public Animator animator;

    public MiniGameTrigger trigger;

    void Awake()
    {
		Cursor.visible = false;

        Cursor.lockState = CursorLockMode.Locked;
    }
    void Start()
    {
		Cursor.visible = false;
        
        Cursor.lockState = CursorLockMode.Locked;

        characterController = GetComponent<CharacterController>();

        controller = new Controller(model);
    }


    // Game controller changes should always be applied in FixedUpdate because they are physics operations.
    void FixedUpdate()
    {
        //Debug.Log(model.horizontalRotation); 

        //Allows move and jump if on ground/

        if (isActive)
        {
            if (controller.IsGrounded(characterController.isGrounded))
            {
                controller.CalcMoveDirection();

                controller.Jump();
            }

            // Output
            characterController.Move(model.MoveDirection * Time.deltaTime);
        }

        controller.ApplyGravity(Time.deltaTime);
        /*         if(animator != null)
                    animator.SetFloat("speed", characterController.velocity.magnitude);*/
        //Debug.Log(animator.GetFloat("speed"));
    }

    // Control input should always be read in Update because it is frame-dependent.
    void Update()
    {

        // Input
        if (isActive){
            model.XMove = Input.GetAxis("Horizontal") * transform.right;
            model.YMove = Input.GetAxis("Vertical") * transform.forward;
            model.Jump = Input.GetButton("Jump");
            model.Shift = Input.GetKey(KeyCode.LeftShift);

            model.MouseX = Input.GetAxis("Mouse X") * Time.fixedDeltaTime;
            model.MouseY = Input.GetAxis("Mouse Y") * Time.fixedDeltaTime;

            controller.CalcRotation();
            transform.rotation = Quaternion.Euler(0, startRotationY + model.horizontalRotation, 0);
            playerHead.localRotation = Quaternion.Euler(model.verticalRotation, 0, 0);
        }
        else{
            model.XMove = Vector3.zero;
            model.YMove = Vector3.zero;
            model.Jump = false;

            model.MouseX = 0;
            model.MouseY = 0;
            model.Shift = false;
        }
    }
}
