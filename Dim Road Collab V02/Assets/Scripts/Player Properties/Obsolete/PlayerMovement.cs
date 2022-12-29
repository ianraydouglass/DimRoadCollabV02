using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public float defaultMoveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public CharacterController controller;

    public float mouseSensitivity = 100f;
    float MouseX;
    float MouseY;

    Vector3 velocity;
    Vector2 inputVector;
    public bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = defaultMoveSpeed;
        Vector3 finalVector = new Vector3();
        finalVector.x = inputVector.x;
        finalVector.z = inputVector.y;
        //controller.Move(finalVector * Time.deltaTime * speed);
        finalVector.y = velocity.y;
        controller.Move(finalVector * Time.deltaTime * speed);
        

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        float mouseX = MouseX * mouseSensitivity * Time.deltaTime;
        Vector3 rotationvalue = new Vector3(0, mouseX, 0);

        controller.transform.Rotate(rotationvalue);
    }

    public void OnMove(InputValue input)
    {
        inputVector = input.Get<Vector2>();
    }

    public void OnLook(InputValue input)
    {

        MouseX = input.Get<Vector2>().x;
        MouseY = input.Get<Vector2>().y;
    }
}
