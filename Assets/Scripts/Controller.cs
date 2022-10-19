using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Decisions only.
// A controller should not know anything except what is contained in the model, and only make decisions when asked to by the view based on input.
public class Controller 
{
    public Model model;

    // The constructor sets the model because the controller cannot use GetComponent()
    public Controller(Model newModel)
    {
        model = newModel;
    }

    // move direction directly from axes
    public void CalcMoveDirection()
    {
        model.MoveDirection = model.XMove + model.YMove;
        model.MoveDirection *= model.Shift? model.speed * 1.5f : model.speed ;
    }

    //Calculates the direction the camera is facing.
    public void CalcRotation()
    {
        model.horizontalRotation += model.MouseX * model.sensitivity;
        model.verticalRotation -= model.MouseY * model.sensitivity;
        model.verticalRotation = Mathf.Clamp(model.verticalRotation, -80f, 85f);
    }

    // Jump if jump button pressed.
    public void Jump()
    {
        if (model.Jump)
        {
            model.MoveDirection = new Vector3(model.MoveDirection.x, model.jumpSpeed, model.MoveDirection.z);
        }
    }

    // Gravity constantly pushes down.
    public void ApplyGravity(float deltaTime)
    {
        model.MoveDirection = new Vector3(model.MoveDirection.x, model.MoveDirection.y - model.gravity * deltaTime, model.MoveDirection.z); 
    }

    // Just return what is passed; placeholder for more complicated future logic.
    public bool IsGrounded(bool grounded)
    {
        return grounded;
    }
}
